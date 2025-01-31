using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

public class CreateTests(EndpointFactory endpointFactory)
    : SharedTests(endpointFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Create_TransactionalPartner_WhenDataIsValid()
    {
        // Arrange
        var request = CreateTransactionalPartnerFaker.Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        var createdResponse = await ReadFromJsonAsync<Guid>(response);
        var transactionalPartnerId = createdResponse!.Result;
        response.Headers.Location!.ToString().Should().Be($"{Url}/{transactionalPartnerId}");
        createdResponse.IsSuccess.Should().BeTrue();
        createdResponse.Errors.Should().BeEmpty();

        await EnsureEqualAsync(transactionalPartnerId, request);

        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.Should().NotBeEmpty();
        transactionalPartners.Should().HaveCount(1);
        transactionalPartners[0].Id.Should().Be(transactionalPartnerId);
        
        CreatedIds.Add(transactionalPartnerId);
    }
    
    [Fact]
    public async Task Create_ReturnsValidationError_WhenInputIsInvalid()
    {
        //Arrange
        var request = CreateTransactionalPartnerFaker
            .Clone()
            .RuleFor(x => x.LocationTypeId, (byte)200)
            .Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.Should().BeFalse();
        createdResponse.Errors.Should().HaveCount(1);
        createdResponse.Result.Should().BeNull();
        
        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Create_ReturnsError_WhenEmailWasExisted()
    {
        //Arrange
        var request1 = CreateTransactionalPartnerFaker.Generate();
        await SetupCreatingTransactionalPartnerAsync(request1);
        var email = request1.Email;
        var request2 = CreateTransactionalPartnerFaker
            .Clone()
            .RuleFor(x => x.Email, email)
            .Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request2);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.Should().BeFalse();
        createdResponse.Result.Should().BeNull();
        createdResponse.Errors.Should().HaveCount(1);
        createdResponse.Errors[0].Should().Be(TelNoOrEmailIsTaken);
        
        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.Should().NotBeEmpty();
        transactionalPartners.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task Create_ReturnsError_WhenTelNoWasExisted()
    {
        //Arrange
        var request1 = CreateTransactionalPartnerFaker.Generate();
        await SetupCreatingTransactionalPartnerAsync(request1);
        var telno = request1.TelNo;
        var request2 = CreateTransactionalPartnerFaker
            .Clone()
            .RuleFor(x => x.TelNo, telno)
            .Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request2);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.Should().BeFalse();
        createdResponse.Result.Should().BeNull();
        createdResponse.Errors.Should().HaveCount(1);
        createdResponse.Errors[0].Should().Be(TelNoOrEmailIsTaken);
        
        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.Should().NotBeEmpty();
        transactionalPartners.Should().HaveCount(1);
    }
}