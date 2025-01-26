using System.Net;
using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Bogus;
using FluentAssertions;
using MinimalApi.ApiResponses;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class GetAllMaterialEndpointTests(MaterialEndpointFactory apiFactory) : IClassFixture<MaterialEndpointFactory>
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
    public async Task GetAll_ReturnsResult_WhenMaterialsExist()
    {
        //Arrange
        var materialRequest = _createMaterialFaker.Generate();
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