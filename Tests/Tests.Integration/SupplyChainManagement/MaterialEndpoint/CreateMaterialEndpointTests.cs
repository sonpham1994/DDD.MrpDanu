using System.Net;
using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Bogus;
using FluentAssertions;
using MinimalApi.ApiResponses;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class CreateMaterialEndpointTests(MaterialEndpointFactory apiFactory) : IClassFixture<MaterialEndpointFactory>
{
    private readonly HttpClient _httpClient = apiFactory.CreateClient();

    private readonly Faker<CreateMaterialCommand> _createMaterialFaker = new Faker<CreateMaterialCommand>()
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Code, f => "code")
        .RuleFor(x => x.Unit, f => f.Random.Words(5))
        .RuleFor(x => x.Varian, f => f.Random.Words(5))
        .RuleFor(x => x.Weight, f => f.Random.Words(5))
        .RuleFor(x => x.Width, f => f.Random.Words(5))
        .RuleFor(x => x.ColorCode, f => f.Random.Words(5))
        .RuleFor(x => x.MaterialTypeId, (byte)1)
        .RuleFor(x => x.RegionalMarketId, (byte)1);

    [Fact]
    public async Task Create_Material_WhenDataIsValid()
    {
        //Arrange
        var materialRequest = _createMaterialFaker.Generate();
        
        // Act
        var response = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest);
        var materialCreatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        response.Headers.Location!.ToString().Should().Be($"{MaterialEndpointFactory.Url}/{materialCreatedResponse.Result}");
        materialCreatedResponse.IsSuccess.Should().BeTrue();
        materialCreatedResponse.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Create_ReturnsError_WhenCodeWasExisted()
    {
        //Arrange
        var materialRequest = _createMaterialFaker
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        
        // Act
        await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest);
        var response = await _httpClient.PostAsJsonAsync(MaterialEndpointFactory.Endpoint, materialRequest);
        var materialCreatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Guid?>>();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        materialCreatedResponse.Should().NotBeNull();
        materialCreatedResponse.IsSuccess.Should().BeFalse();
        materialCreatedResponse.Errors[0].Code.Should().Be(MaterialEndpointFactory.ExistedMaterialCode.Code);
    }
}