using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using MinimalApi.ApiResponses;
using Tests.Integration.SupplyChainManagement.MaterialEndpoint.Base;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class CreateTests(EndpointFactory apiFactory)
    : BaseTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Create_Material_WhenDataIsValid()
    {
        //Arrange
        var materialRequest = CreateMaterialFaker.Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, materialRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        var materialCreatedResponse = await EndpointFactory.ReadFromJsonAsync<Guid>(response);
        var materialId = materialCreatedResponse!.Result;
        response.Headers.Location!.ToString().Should().Be($"{Url}/{materialId}");
        materialCreatedResponse.IsSuccess.Should().BeTrue();
        materialCreatedResponse.Errors.Should().BeEmpty();

        var material = await GetMaterialByIdAsync(materialId);
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
        
        var materials = await GetAllMaterialsAsync();
        materials[0].Id.Should().Be(materialId);
        
        CreatedIds.Add(materialId);
    }
    
    [Fact]
    public async Task Create_ReturnsValidationError_WhenInputIsInvalid()
    {
        //Arrange
        var materialRequest = CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.RegionalMarketId, (byte)200)
            .Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, materialRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var materialCreatedResponse = await EndpointFactory.ReadFromJsonAsync<Guid?>(response);
        materialCreatedResponse!.IsSuccess.Should().BeFalse();
        materialCreatedResponse.Errors.Should().HaveCount(1);
        materialCreatedResponse.Result.Should().BeNull();
    }
    
    [Fact]
    public async Task Create_ReturnsError_WhenCodeWasExisted()
    {
        //Arrange
        var materialRequest = CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        
        // Act
        var response1 = await HttpClient.PostAsJsonAsync(Endpoint, materialRequest);
        var response2 = await HttpClient.PostAsJsonAsync(Endpoint, materialRequest);
        var materialCreatedResponse2 = await EndpointFactory.ReadFromJsonAsync<Guid?>(response2);
        
        // Assert
        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        materialCreatedResponse2.Should().NotBeNull();
        materialCreatedResponse2.IsSuccess.Should().BeFalse();
        materialCreatedResponse2.Errors[0].Code.Should().Be(ExistedMaterialCode.Code);
        materialCreatedResponse2.Result.Should().BeNull();

        var materials = await GetAllMaterialsAsync();
        materials.Should().HaveCount(1);
        var materialCreatedResponse1 = await EndpointFactory.ReadFromJsonAsync<Guid>(response1);
        var materialId1 = materialCreatedResponse1!.Result;
        materials[0].Id.Should().Be(materialId1);
        
        CreatedIds.Add(materialId1);
    }
}