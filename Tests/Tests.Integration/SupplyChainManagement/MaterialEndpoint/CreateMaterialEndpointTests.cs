using System.Net;
using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using FluentAssertions;
using MinimalApi.ApiResponses;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class CreateMaterialEndpointTests(MaterialEndpointFactory apiFactory) : IClassFixture<MaterialEndpointFactory>
{
    private readonly HttpClient _httpClient = apiFactory.CreateClient();

    [Fact]
    public async Task Create_Material_WhenDataIsValid()
    {
        //Arrange
        var materialRequest = MaterialEndpointFactory.CreateMaterialFaker.Generate();
        
        // Act
        var response = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest);
        
        // Assert
        var materialCreatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        var materialId = materialCreatedResponse!.Result;
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        response.Headers.Location!.ToString().Should().Be($"{MaterialEndpointFactory.Url}/{materialId}");
        materialCreatedResponse.IsSuccess.Should().BeTrue();
        materialCreatedResponse.Errors.Should().BeEmpty();

        var materialResponse = await _httpClient.GetAsync($"{MaterialEndpointFactory.Endpoint}/{materialId}");
        var materialApiResponse = await materialResponse.Content.ReadFromJsonAsync<ApiResponse<MaterialResponse>>();
        var material = materialApiResponse!.Result;
        material.Id.Should().Be(materialId);
        material.Code.Should().Be(materialRequest.Code);
        material.Name.Should().Be(materialRequest.Name);
        material.Unit.Should().Be(materialRequest.Unit.ToUpper());
        material.Varian.Should().Be(materialRequest.Varian);
        material.Weight.Should().Be(materialRequest.Weight);
        material.Width.Should().Be(materialRequest.Width);
        material.ColorCode.Should().Be(materialRequest.ColorCode);
        material.MaterialType.Id.Should().Be(materialRequest.MaterialTypeId);
        material.RegionalMarket.Id.Should().Be(materialRequest.RegionalMarketId);
        material.MaterialCostManagements.Should().BeEmpty();
        
        var materialsResponse = await _httpClient.GetAsync(MaterialEndpointFactory.Endpoint);
        var materialApiResponses = await materialsResponse.Content.ReadFromJsonAsync<ApiResponse<IReadOnlyList<MaterialsResponse>>>();
        var materials = materialApiResponses!.Result;
        materials[0].Id.Should().Be(materialId);
        
        await _httpClient.DeleteAsync($"{MaterialEndpointFactory.Endpoint}/{materialId}");
    }
    
    [Fact]
    public async Task Create_ReturnsError_WhenCodeWasExisted()
    {
        //Arrange
        var materialRequest = MaterialEndpointFactory.CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        
        // Act
        var response1 = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest);
        var response2 = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest);
        var materialCreatedResponse2 = await response2.Content.ReadFromJsonAsync<ApiResponse<Guid?>>();
        
        // Assert
        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        materialCreatedResponse2.Should().NotBeNull();
        materialCreatedResponse2.IsSuccess.Should().BeFalse();
        materialCreatedResponse2.Errors[0].Code.Should().Be(MaterialEndpointFactory.ExistedMaterialCode.Code);

        var materialsResponse = await _httpClient.GetAsync(MaterialEndpointFactory.Endpoint);
        var materialApiResponses = await materialsResponse.Content.ReadFromJsonAsync<ApiResponse<IReadOnlyList<MaterialsResponse>>>();
        var materials = materialApiResponses!.Result;
        materials.Should().HaveCount(1);
        var materialCreatedResponse1 = await response1.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        var materialId1 = materialCreatedResponse1!.Result;
        materials[0].Id.Should().Be(materialId1);
        
        await _httpClient.DeleteAsync($"{MaterialEndpointFactory.Endpoint}/{materialId1}");
    }
}