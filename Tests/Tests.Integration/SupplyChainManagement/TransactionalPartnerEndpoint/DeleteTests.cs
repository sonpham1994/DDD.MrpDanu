using System.Net;
using Shouldly;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

public class DeleteTests(EndpointFactory endpointFactory) 
    : SharedTests(endpointFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Delete_TransactionalPartner_WhenIdIsValid()
    {
        // Arrange
        var id = await SetupCreatingTransactionalPartnerAsync();
        
        // Act
        var response = await HttpClient.DeleteAsync($"{Endpoint}/{id}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var deletedResponse = await ReadFromJsonAsync(response);
        deletedResponse!.IsSuccess.ShouldBeTrue();
        deletedResponse.Errors.ShouldBeEmpty();

        var transactionalPartner = await GetTransactionalPartnerByIdAsync(id);
        transactionalPartner.ShouldBeNull();

        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.ShouldBeEmpty();
        
        var existsMaterialCost = await GetDatabaseFacade()
            .SqlQuery<bool>($"SELECT TOP 1 Id FROM ContactPersonInformation WHERE Id = {id}")
            .AnyAsync();
        existsMaterialCost.ShouldBeFalse();
        
        CreatedIds.Clear();
    }
    
    [Fact]
    public async Task Delete_ReturnsError_WhenIdIsNotFound()
    {
        // Act
        var response = await HttpClient.DeleteAsync($"{Endpoint}/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var deletedResponse = await ReadFromJsonAsync(response);
        deletedResponse!.IsSuccess.ShouldBeFalse();
        deletedResponse.Errors.ShouldNotBeEmpty();
        deletedResponse.Errors[0].ShouldBe(NotFoundId);
    }
}