using System.Net;
using FluentAssertions;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class DeleteTests(EndpointFactory apiFactory) 
    : SharedTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Delete_Material_WhenIdIsValid()
    {
        // Arrange
        var id = await SetupCreatingMaterialAsync();
        
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
        deletedResponse.Errors[0].Should().Be(NotFoundMaterial);
    }
}