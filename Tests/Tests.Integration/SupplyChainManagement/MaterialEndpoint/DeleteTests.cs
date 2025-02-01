using System.Net;
using FluentAssertions;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

//[Collection("MaterialEndpoint")]
public class DeleteTests(EndpointFactory apiFactory) 
    : SharedTests(apiFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Delete_Material_WhenIdIsValid()
    {
        // Arrange
        var supplierId = await SetupCreatingSupplierTypeAsync();
        var id = await SetupCreatingMaterialWithCostAsync(supplierId);
        
        // Act
        var response = await HttpClient.DeleteAsync($"{Endpoint}/{id}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var deletedResponse = await ReadFromJsonAsync(response);
        deletedResponse!.IsSuccess.Should().BeTrue();
        deletedResponse.Errors.Should().BeEmpty();

        var material = await GetMaterialByIdAsync(id);
        material.Should().BeNull();

        var materials = await GetAllMaterialsAsync();
        materials.Should().BeEmpty();

        var existsMaterialCost = await GetDatabaseFacade()
            .SqlQuery<bool>($"SELECT TOP 1 Id FROM MaterialSupplierCost WHERE MaterialId = {id}")
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
        deletedResponse.Errors[0].Should().Be(NotFoundMaterialId);
    }
}