using System.Net;
using FluentAssertions;
using Tests.Integration.SupplyChainManagement.MaterialEndpoint.Base;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class DeleteTests(EndpointFactory apiFactory) 
    : BaseTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Delete_Material_WhenIdIsValid()
    {
        // Arrange
        var materialId = await SetupCreatingMaterialAsync();
        
        // Act
        var response = await HttpClient.DeleteAsync($"{Endpoint}/{materialId}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialDeletedResponse = await EndpointFactory.ReadFromJsonAsync(response);
        materialDeletedResponse!.IsSuccess.Should().BeTrue();
        materialDeletedResponse.Errors.Should().BeEmpty();

        var material = await GetMaterialByIdAsync(materialId);
        material.Should().BeNull();

        var materials = await GetAllMaterialsAsync();
        materials.Should().BeEmpty();
        
        CreatedIds.Clear();
    }
    
    [Fact]
    public async Task Delete_ReturnsError_WhenIdIsNotFound()
    {
        // Act
        var response = await HttpClient.DeleteAsync($"{Endpoint}/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var materialDeletedResponse = await EndpointFactory.ReadFromJsonAsync(response);
        materialDeletedResponse!.IsSuccess.Should().BeFalse();
        materialDeletedResponse.Errors.Should().NotBeEmpty();
        materialDeletedResponse.Errors[0].Code.Should().Be(NotFoundMaterial.Code);
    }
}