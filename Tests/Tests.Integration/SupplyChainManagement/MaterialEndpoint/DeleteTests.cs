using System.Net;
using Shouldly;
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
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var deletedResponse = await ReadFromJsonAsync(response);
        deletedResponse!.IsSuccess.ShouldBeTrue();
        deletedResponse.Errors.ShouldBeEmpty();

        var material = await GetMaterialByIdAsync(id);
        material.ShouldBeNull();

        var materials = await GetAllMaterialsAsync();
        materials.ShouldBeEmpty();

        var existsMaterialCost = await GetDatabaseFacade()
            .SqlQuery<bool>($"SELECT TOP 1 Id FROM MaterialSupplierCost WHERE MaterialId = {id}")
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
        deletedResponse.Errors[0].ShouldBe(NotFoundMaterialId);
    }
}