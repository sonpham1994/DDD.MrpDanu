using System.Net;
using FluentAssertions;
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
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var deletedResponse = await ReadFromJsonAsync(response);
        deletedResponse!.IsSuccess.Should().BeTrue();
        deletedResponse.Errors.Should().BeEmpty();

        var transactionalPartner = await GetTransactionalPartnerByIdAsync(id);
        transactionalPartner.Should().BeNull();

        var transactionalPartners = await GetAllTransactionalPartnersAsync();
        transactionalPartners.Should().BeEmpty();
        
        var existsMaterialCost = await GetDatabaseFacade()
            .SqlQuery<bool>($"SELECT TOP 1 Id FROM ContactPersonInformation WHERE Id = {id}")
            .AnyAsync();
        existsMaterialCost.Should().BeFalse();
        
        CreatedIds.Clear();
    }
    
    [Fact]
    public async Task Delete_ReturnsError_WhenIdIsNotFound()
    {
        // Act
        var response = await HttpClient.DeleteAsync($"{Endpoint}/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var deletedResponse = await ReadFromJsonAsync(response);
        deletedResponse!.IsSuccess.Should().BeFalse();
        deletedResponse.Errors.Should().NotBeEmpty();
        deletedResponse.Errors[0].Should().Be(NotFoundId);
    }
}