using System.Net;
using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using FluentAssertions;
using MinimalApi.ApiResponses;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class GetAllMaterialEndpointTests(MaterialEndpointFactory apiFactory) : IClassFixture<MaterialEndpointFactory>
{
    private readonly HttpClient _httpClient = apiFactory.CreateClient();

    [Fact]
    public async Task GetAll_ReturnsResult_WhenMaterialsExist()
    {
        //Arrange
        var materialRequest = MaterialEndpointFactory.CreateMaterialFaker.Generate();
        var response = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest);
        var materialCreatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        
        // Act
        var materialsResponse = await _httpClient.GetAsync(MaterialEndpointFactory.Endpoint);
        
        // Assert
        materialsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var customersResponse = await materialsResponse.Content.ReadFromJsonAsync<ApiResponse<IReadOnlyList<MaterialsResponse>>>();
        customersResponse!.IsSuccess.Should().BeTrue();
        customersResponse.Errors.Should().BeEmpty();
        customersResponse.Result.Should().HaveCount(1);
        
        await _httpClient.DeleteAsync($"{MaterialEndpointFactory.Endpoint}/{materialCreatedResponse.Result}");
    }
    
    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNoMaterialsExist()
    {
        // Act
        var response = await _httpClient.GetAsync(MaterialEndpointFactory.Endpoint);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var customersResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IReadOnlyList<MaterialsResponse>>>();
        customersResponse!.IsSuccess.Should().BeTrue();
        customersResponse.Errors.Should().BeEmpty();
        customersResponse.Result.Should().BeEmpty();
    }
}