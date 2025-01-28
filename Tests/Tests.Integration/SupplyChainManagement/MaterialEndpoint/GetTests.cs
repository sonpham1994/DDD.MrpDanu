using System.Net;
using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using FluentAssertions;
using MinimalApi.ApiResponses;
using Tests.Integration.SupplyChainManagement.MaterialEndpoint.Base;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class GetTests(EndpointFactory apiFactory)
    : BaseTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Get_ReturnsMaterial_WhenMaterialExists()
    {
        // Arrange
        var materialId = await SetupCreatingMaterialAsync();
        
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/{materialId}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialResponse = await response.Content.ReadFromJsonAsync<ApiResponse<MaterialResponse>>();
        materialResponse!.IsSuccess.Should().BeTrue();
        materialResponse.Errors.Should().BeEmpty();
        materialResponse.Result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Get_ReturnsNotFound_WhenNoMaterialExists()
    {
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var materialResponse = await response.Content.ReadFromJsonAsync<ApiResponse<MaterialResponse>>();
        materialResponse!.IsSuccess.Should().BeFalse();
        materialResponse.Errors.Should().NotBeEmpty();
        materialResponse.Errors[0].Code.Should().Be(NotFoundMaterial.Code);
        materialResponse.Result.Should().BeNull();
    }
}