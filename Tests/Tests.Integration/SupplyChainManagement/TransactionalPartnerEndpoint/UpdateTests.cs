using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

public class UpdateTests(EndpointFactory apiFactory)
    : SharedTests(apiFactory.CreateClient()) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Update_TransactionalPartner_WhenDataIsValid()
    {
        // Arrange
        var id = await SetupCreatingTransactionalPartnerAsync();
        var request = UpdateTransactionalPartnerFaker(id).Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var transactionalPartnerUpdatedResponse = await ReadFromJsonAsync(response);
        transactionalPartnerUpdatedResponse!.IsSuccess.Should().BeTrue();
        transactionalPartnerUpdatedResponse.Errors.Should().BeEmpty();

        await EnsureEqualAsync(id, request);
    }
    
    [Fact]
    public async Task Update_ReturnsValidationError_WhenInputIsInvalid()
    {
        //Arrange
        var request = CreateTransactionalPartnerFaker.Generate();
        var id = await SetupCreatingTransactionalPartnerAsync(request);
        var request2 = UpdateTransactionalPartnerFaker(id)
            .Clone()
            .RuleFor(x => x.LocationTypeId, (byte)200)
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}", request2);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse!.IsSuccess.Should().BeFalse();
        updatedResponse.Errors.Should().HaveCount(1);
        
        await EnsureEqualAsync(id, request);
    }
    
    [Fact]
    public async Task Update_ReturnsError_WhenEmailWasExisted()
    {
        //Arrange
        var requestCreate1 = CreateTransactionalPartnerFaker.Generate();
        await SetupCreatingTransactionalPartnerAsync(requestCreate1);
        
        var requestCreate2 = CreateTransactionalPartnerFaker.Generate();
        var id = await SetupCreatingTransactionalPartnerAsync(requestCreate2);
        var email = requestCreate1.Email;
        var updateRequest = UpdateTransactionalPartnerFaker(id)
            .Clone()
            .RuleFor(x => x.Email, email)
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}", updateRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse!.IsSuccess.Should().BeFalse();
        updatedResponse.Errors.Should().HaveCount(1);
        updatedResponse.Errors[0].Should().Be(TelNoOrEmailIsTaken);
        
        await EnsureEqualAsync(id, requestCreate2);
    }
    
    [Fact]
    public async Task Update_ReturnsError_WhenTelNoWasExisted()
    {
        //Arrange
        var requestCreate1 = CreateTransactionalPartnerFaker.Generate();
        await SetupCreatingTransactionalPartnerAsync(requestCreate1);
        
        var requestCreate2 = CreateTransactionalPartnerFaker.Generate();
        var id = await SetupCreatingTransactionalPartnerAsync(requestCreate2);
        var telno = requestCreate1.TelNo;
        var updateRequest = UpdateTransactionalPartnerFaker(id)
            .Clone()
            .RuleFor(x => x.TelNo, telno)
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}", updateRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync(response);
        createdResponse!.IsSuccess.Should().BeFalse();
        createdResponse.Errors.Should().HaveCount(1);
        createdResponse.Errors[0].Should().Be(TelNoOrEmailIsTaken);
        
        await EnsureEqualAsync(id, requestCreate2);
    }
    
    [Fact]
    public async Task Update_TransactionalPartner_WhenEmailWasExistedByItself()
    {
        // Arrange
        var requestCreate = CreateTransactionalPartnerFaker.Generate();
        var id = await SetupCreatingTransactionalPartnerAsync(requestCreate);
        var request = UpdateTransactionalPartnerFaker(id)
            .Clone()
            .RuleFor(x => x.Email, requestCreate.Email)
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var transactionalPartnerUpdatedResponse = await ReadFromJsonAsync(response);
        transactionalPartnerUpdatedResponse!.IsSuccess.Should().BeTrue();
        transactionalPartnerUpdatedResponse.Errors.Should().BeEmpty();

        await EnsureEqualAsync(id, request);
    }
    
    [Fact]
    public async Task Update_TransactionalPartner_WhenTelNoWasExistedByItself()
    {
        // Arrange
        var requestCreate = CreateTransactionalPartnerFaker.Generate();
        var id = await SetupCreatingTransactionalPartnerAsync(requestCreate);
        var request = UpdateTransactionalPartnerFaker(id)
            .Clone()
            .RuleFor(x => x.TelNo, requestCreate.TelNo)
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var transactionalPartnerUpdatedResponse = await ReadFromJsonAsync(response);
        transactionalPartnerUpdatedResponse!.IsSuccess.Should().BeTrue();
        transactionalPartnerUpdatedResponse.Errors.Should().BeEmpty();

        await EnsureEqualAsync(id, request);
    }
    
    [Fact]
    public async Task Update_TransactionalPartner_WhenTelNoAndEmailWereExistedByItself()
    {
        // Arrange
        var requestCreate = CreateTransactionalPartnerFaker.Generate();
        var id = await SetupCreatingTransactionalPartnerAsync(requestCreate);
        var request = UpdateTransactionalPartnerFaker(id)
            .Clone()
            .RuleFor(x => x.TelNo, requestCreate.TelNo)
            .RuleFor(x => x.Email, requestCreate.Email)
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var transactionalPartnerUpdatedResponse = await ReadFromJsonAsync(response);
        transactionalPartnerUpdatedResponse!.IsSuccess.Should().BeTrue();
        transactionalPartnerUpdatedResponse.Errors.Should().BeEmpty();

        await EnsureEqualAsync(id, request);
    }
}