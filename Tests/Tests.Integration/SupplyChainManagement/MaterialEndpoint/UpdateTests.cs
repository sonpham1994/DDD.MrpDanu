using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class UpdateTests(EndpointFactory apiFactory) 
    : SharedTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Update_Material_WhenDataIsValid()
    {
        //Arrange
        var materialId = await SetupCreatingMaterialAsync();
        var request = UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse!.IsSuccess.Should().BeTrue();
        updatedResponse.Errors.Should().BeEmpty();

        await EnsureEqualAsync(materialId, request);
    }
    
    [Fact]
    public async Task Update_ReturnsValidationError_WhenInputIsInvalid()
    {
        //Arrange
        var createRequest = CreateMaterialFaker.Generate();
        var materialId = await SetupCreatingMaterialAsync(createRequest);
        var request = UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .RuleFor(x => x.RegionalMarketId, (byte)200)
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync(response);
        createdResponse!.IsSuccess.Should().BeFalse();
        createdResponse.Errors.Should().HaveCount(1);
        
        await EnsureEqualAsync(materialId, createRequest);
    }
    
    [Fact]
    public async Task Update_Material_WhenCodeWasExistedByItself()
    {
        //Arrange
        var materialId = await SetupCreatingMaterialAsync();
        var request = UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse!.IsSuccess.Should().BeTrue();
        updatedResponse.Errors.Should().BeEmpty();

        await EnsureEqualAsync(materialId, request);
    }
    
    [Fact]
    public async Task Update_ReturnsError_WhenCodeWasExisted()
    {
        //Arrange
        await SetupCreatingMaterialAsync();
        
        var createRequest = CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        var materialId2 = await SetupCreatingMaterialAsync(createRequest);
        
        var updateRequest = UpdateMaterialFaker(materialId2)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId2}", updateRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse.Should().NotBeNull();
        updatedResponse.IsSuccess.Should().BeFalse();
        updatedResponse.Errors[0].Should().Be(ExistedMaterialCode);
        
        await EnsureEqualAsync(materialId2, createRequest);
    }
}