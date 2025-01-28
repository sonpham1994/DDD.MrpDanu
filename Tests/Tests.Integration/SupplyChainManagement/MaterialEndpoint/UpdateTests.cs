using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using MinimalApi.ApiResponses;
using Tests.Integration.SupplyChainManagement.MaterialEndpoint.Base;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class UpdateTests(EndpointFactory apiFactory) 
    : BaseTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Update_Material_WhenDataIsValid()
    {
        //Arrange
        var materialId = await SetupCreatingMaterialAsync();
        var updateMaterialRequest = UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", updateMaterialRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialUpdatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        materialUpdatedResponse!.IsSuccess.Should().BeTrue();
        materialUpdatedResponse.Errors.Should().BeEmpty();

        var material = await GetMaterialByIdAsync(materialId);
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
    }
    
    [Fact]
    public async Task Update_ReturnsValidationError_WhenInputIsInvalid()
    {
        //Arrange
        var materialId = await SetupCreatingMaterialAsync();
        var updateMaterialRequest = UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .RuleFor(x => x.RegionalMarketId, (byte)200)
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", updateMaterialRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var materialCreatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        materialCreatedResponse!.IsSuccess.Should().BeFalse();
        materialCreatedResponse.Errors.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task Update_Material_WhenCodeIsExistedByItself()
    {
        //Arrange
        var materialId = await SetupCreatingMaterialAsync();
        var updateMaterialRequest = UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", updateMaterialRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialUpdatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        materialUpdatedResponse!.IsSuccess.Should().BeTrue();
        materialUpdatedResponse.Errors.Should().BeEmpty();

        var material = await GetMaterialByIdAsync(materialId);
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
    }
    
    [Fact]
    public async Task Update_ReturnsError_WhenCodeWasExisted()
    {
        //Arrange
        var materialId1 = await SetupCreatingMaterialAsync();
        
        var materialRequest2 = CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        var materialId2 = await SetupCreatingMaterialAsync(materialRequest2);
        
        var updateMaterialRequest = UpdateMaterialFaker(materialId2)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId2}", updateMaterialRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var materialUpdatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        materialUpdatedResponse.Should().NotBeNull();
        materialUpdatedResponse.IsSuccess.Should().BeFalse();
        materialUpdatedResponse.Errors[0].Code.Should().Be(ExistedMaterialCode.Code);
        
        var material = await GetMaterialByIdAsync(materialId2);
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
    }
}