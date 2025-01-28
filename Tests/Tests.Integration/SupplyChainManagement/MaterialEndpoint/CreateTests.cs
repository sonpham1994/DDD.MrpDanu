using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class CreateTests(EndpointFactory apiFactory)
    : SharedTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Create_Material_WhenDataIsValid()
    {
        //Arrange
        var request = CreateMaterialFaker.Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        var createdResponse = await ReadFromJsonAsync<Guid>(response);
        var materialId = createdResponse!.Result;
        response.Headers.Location!.ToString().Should().Be($"{Url}/{materialId}");
        createdResponse.IsSuccess.Should().BeTrue();
        createdResponse.Errors.Should().BeEmpty();

        await EnsureEqualAsync(materialId, request);
        
        var materials = await GetAllMaterialsAsync();
        materials.Should().NotBeEmpty();
        materials.Should().HaveCount(1);
        materials[0].Id.Should().Be(materialId);
    }
    
    [Fact]
    public async Task Create_ReturnsValidationError_WhenInputIsInvalid()
    {
        //Arrange
        var request = CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.RegionalMarketId, (byte)200)
            .Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.Should().BeFalse();
        createdResponse.Errors.Should().HaveCount(1);
        createdResponse.Result.Should().BeNull();
        
        var materials = await GetAllMaterialsAsync();
        materials.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Create_ReturnsError_WhenCodeWasExisted()
    {
        //Arrange
        var request1 = CreateMaterialFaker.Generate();
        var id1 = await SetupCreatingMaterialAsync(request1);
        var code = request1.Code;
        var request2 = CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.Code, code)
            .Generate();
        
        // Act
        var response2 = await HttpClient.PostAsJsonAsync(Endpoint, request2);
        var createdResponse2 = await ReadFromJsonAsync<Guid?>(response2);
        
        // Assert
        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        createdResponse2.Should().NotBeNull();
        createdResponse2.IsSuccess.Should().BeFalse();
        createdResponse2.Errors[0].Should().Be(ExistedMaterialCode);
        createdResponse2.Result.Should().BeNull();

        var materials = await GetAllMaterialsAsync();
        materials.Should().NotBeEmpty();
        materials.Should().HaveCount(1);
        materials[0].Id.Should().Be(id1);
    }
}