using System.Net;
using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using FluentAssertions;
using MinimalApi.ApiResponses;
using Tests.Integration.SupplyChainManagement.MaterialEndpoint.Base;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class GetAllTests(EndpointFactory apiFactory) 
    : BaseTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task GetAll_ReturnsResult_WhenMaterialsExist()
    {
        //Arrange
        var materialId = await SetupCreatingMaterialAsync();
        
        // Act
        var materialsResponse = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        materialsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var customersResponse = await materialsResponse.Content.ReadFromJsonAsync<ApiResponse<IReadOnlyList<MaterialsResponse>>>();
        customersResponse!.IsSuccess.Should().BeTrue();
        customersResponse.Errors.Should().BeEmpty();
        customersResponse.Result.Should().HaveCount(1);
        
        CreatedIds.Add(materialId);
    }
    
    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNoMaterialsExist()
    {
        // Act
        var response = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var customersResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IReadOnlyList<MaterialsResponse>>>();
        customersResponse!.IsSuccess.Should().BeTrue();
        customersResponse.Errors.Should().BeEmpty();
        customersResponse.Result.Should().BeEmpty();
    }
}