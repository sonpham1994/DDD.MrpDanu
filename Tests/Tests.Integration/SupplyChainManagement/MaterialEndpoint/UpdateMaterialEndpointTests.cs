using System.Net;
using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using FluentAssertions;
using MinimalApi.ApiResponses;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class UpdateMaterialEndpointTests(MaterialEndpointFactory apiFactory) : IClassFixture<MaterialEndpointFactory>
{
    private readonly HttpClient _httpClient = apiFactory.CreateClient();
    
    [Fact]
    public async Task Update_Material_WhenDataIsValid()
    {
        //Arrange
        var materialRequest = MaterialEndpointFactory.CreateMaterialFaker.Generate();
        var materialCreatedResponse = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest);
        var apiResponse = await materialCreatedResponse.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        var materialId = apiResponse!.Result;
        var updateMaterialRequest = MaterialEndpointFactory.UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        
        // Act
        var response = await _httpClient.PutAsJsonAsync($"{MaterialEndpointFactory.Endpoint}/{materialId}", updateMaterialRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialUpdatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        materialUpdatedResponse!.IsSuccess.Should().BeTrue();
        materialUpdatedResponse.Errors.Should().BeEmpty();

        var materialResponse = await _httpClient.GetAsync($"{MaterialEndpointFactory.Endpoint}/{materialId}");
        var materialApiResponse = await materialResponse.Content.ReadFromJsonAsync<ApiResponse<MaterialResponse>>();
        var material = materialApiResponse!.Result;
        material.Id.Should().Be(materialId);
        material.Code.Should().Be(updateMaterialRequest.Code);
        material.Name.Should().Be(updateMaterialRequest.Name);
        material.Unit.Should().Be(updateMaterialRequest.Unit.ToUpper());
        material.Varian.Should().Be(updateMaterialRequest.Varian);
        material.Weight.Should().Be(updateMaterialRequest.Weight);
        material.Width.Should().Be(updateMaterialRequest.Width);
        material.ColorCode.Should().Be(updateMaterialRequest.ColorCode);
        material.MaterialType.Id.Should().Be(updateMaterialRequest.MaterialTypeId);
        material.RegionalMarket.Id.Should().Be(updateMaterialRequest.RegionalMarketId);
        material.MaterialCostManagements.Should().BeEmpty();
        
        await _httpClient.DeleteAsync($"{MaterialEndpointFactory.Endpoint}/{materialId}");
    }
    
    [Fact]
    public async Task Update_Material_WhenCodeIsExistedByItself()
    {
        //Arrange
        var materialRequest = MaterialEndpointFactory.CreateMaterialFaker.Generate();
        var materialCreatedResponse = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest);
        var apiResponse = await materialCreatedResponse.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        var materialId = apiResponse!.Result;
        var updateMaterialRequest = MaterialEndpointFactory.UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .Generate();
        
        // Act
        var response = await _httpClient.PutAsJsonAsync($"{MaterialEndpointFactory.Endpoint}/{materialId}", updateMaterialRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialUpdatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        materialUpdatedResponse!.IsSuccess.Should().BeTrue();
        materialUpdatedResponse.Errors.Should().BeEmpty();

        var materialResponse = await _httpClient.GetAsync($"{MaterialEndpointFactory.Endpoint}/{materialId}");
        var materialApiResponse = await materialResponse.Content.ReadFromJsonAsync<ApiResponse<MaterialResponse>>();
        var material = materialApiResponse!.Result;
        material.Id.Should().Be(materialId);
        material.Code.Should().Be(updateMaterialRequest.Code);
        material.Name.Should().Be(updateMaterialRequest.Name);
        material.Unit.Should().Be(updateMaterialRequest.Unit.ToUpper());
        material.Varian.Should().Be(updateMaterialRequest.Varian);
        material.Weight.Should().Be(updateMaterialRequest.Weight);
        material.Width.Should().Be(updateMaterialRequest.Width);
        material.ColorCode.Should().Be(updateMaterialRequest.ColorCode);
        material.MaterialType.Id.Should().Be(updateMaterialRequest.MaterialTypeId);
        material.RegionalMarket.Id.Should().Be(updateMaterialRequest.RegionalMarketId);
        material.MaterialCostManagements.Should().BeEmpty();
        
        await _httpClient.DeleteAsync($"{MaterialEndpointFactory.Endpoint}/{materialId}");
    }
    
    [Fact]
    public async Task Update_ReturnsError_WhenCodeWasExisted()
    {
        //Arrange
        var materialRequest1 = MaterialEndpointFactory.CreateMaterialFaker.Generate();
        var materialCreatedResponse1 = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest1);
        var apiCreatedResponse1 = await materialCreatedResponse1.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        var materialId1 = apiCreatedResponse1!.Result;
        
        var materialRequest2 = MaterialEndpointFactory.CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        var materialCreatedResponse2 = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest2);
        var apiCreatedResponse2 = await materialCreatedResponse2.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        var materialId2 = apiCreatedResponse2!.Result;
        var updateMaterialRequest = MaterialEndpointFactory.UpdateMaterialFaker(materialId2)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .Generate();
        
        // Act
        var response = await _httpClient.PutAsJsonAsync($"{MaterialEndpointFactory.Endpoint}/{materialId2}", updateMaterialRequest);
        
        // Assert
        var materialUpdatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        materialUpdatedResponse.Should().NotBeNull();
        materialUpdatedResponse.IsSuccess.Should().BeFalse();
        materialUpdatedResponse.Errors[0].Code.Should().Be(MaterialEndpointFactory.ExistedMaterialCode.Code);
        
        var materialResponse = await _httpClient.GetAsync($"{MaterialEndpointFactory.Endpoint}/{materialId2}");
        var materialApiResponse = await materialResponse.Content.ReadFromJsonAsync<ApiResponse<MaterialResponse>>();
        var material = materialApiResponse!.Result;
        material.Id.Should().Be(materialId2);
        material.Code.Should().Be(materialRequest2.Code);
        material.Name.Should().Be(materialRequest2.Name);
        material.Unit.Should().Be(materialRequest2.Unit.ToUpper());
        material.Varian.Should().Be(materialRequest2.Varian);
        material.Weight.Should().Be(materialRequest2.Weight);
        material.Width.Should().Be(materialRequest2.Width);
        material.ColorCode.Should().Be(materialRequest2.ColorCode);
        material.MaterialType.Id.Should().Be(materialRequest2.MaterialTypeId);
        material.RegionalMarket.Id.Should().Be(materialRequest2.RegionalMarketId);
        material.MaterialCostManagements.Should().BeEmpty();
        
        await _httpClient.DeleteAsync($"{MaterialEndpointFactory.Endpoint}/{materialId1}");
        await _httpClient.DeleteAsync($"{MaterialEndpointFactory.Endpoint}/{materialId2}");
    }
}