using System.Net;
using System.Net.Http.Json;
using Shouldly;

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
        response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        var createdResponse = await ReadFromJsonAsync<Guid>(response);
        var transactionalPartnerId = createdResponse!.Result;
        response.Headers.Location!.ToString().ShouldBe($"{Url}/{transactionalPartnerId}");
        createdResponse.IsSuccess.ShouldBeTrue();
        createdResponse.Errors.ShouldBeEmpty();

        await EnsureEqualAsync(transactionalPartnerId, request);

        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.ShouldNotBeEmpty();
        transactionalPartners.ShouldHaveSingleItem();
        transactionalPartners[0].Id.ShouldBe(transactionalPartnerId);
        
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
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.ShouldBeFalse();
        createdResponse.Errors.ShouldHaveSingleItem();
        createdResponse.Result.ShouldBeNull();
        
        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.ShouldBeEmpty();
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
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.ShouldBeFalse();
        createdResponse.Result.ShouldBeNull();
        createdResponse.Errors.ShouldHaveSingleItem();
        createdResponse.Errors[0].ShouldBe(TelNoOrEmailIsTaken);
        
        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.ShouldNotBeEmpty();
        transactionalPartners.ShouldHaveSingleItem();
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
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.ShouldBeFalse();
        createdResponse.Result.ShouldBeNull();
        createdResponse.Errors.ShouldHaveSingleItem();
        createdResponse.Errors[0].ShouldBe(TelNoOrEmailIsTaken);
        
        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.ShouldNotBeEmpty();
        transactionalPartners.ShouldHaveSingleItem();
    }
}