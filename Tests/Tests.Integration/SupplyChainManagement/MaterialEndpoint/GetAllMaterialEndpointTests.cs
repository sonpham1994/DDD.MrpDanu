using System.Net;
using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Bogus;
using FluentAssertions;
using MinimalApi.ApiResponses;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class GetAllMaterialEndpointTests : IClassFixture<MaterialEndpointFactory>
{
    private readonly HttpClient _httpClient;

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
    
    public GetAllMaterialEndpointTests(MaterialEndpointFactory apiFactory)
    {
        _httpClient = apiFactory.CreateClient();
    }
    
    [Fact]
    public async Task GetAll_ReturnsResult_WhenMaterialsExist()
    {
        //Arrange
        var materialRequest = _createMaterialFaker.Generate();
        await _httpClient.PostAsJsonAsync($"supply-chain-management/materials", materialRequest);
        
        // Act
        var materialsResponse = await _httpClient.GetAsync($"supply-chain-management/materials");
        
        // Assert
        materialsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var customersResponse = await materialsResponse.Content.ReadFromJsonAsync<ApiResponse<IReadOnlyList<MaterialsResponse>>>();
        customersResponse!.IsSuccess.Should().BeTrue();
        customersResponse.Errors.Should().BeEmpty();
        customersResponse.Result.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNoMaterialsExist()
    {
        // Act
        var response = await _httpClient.GetAsync($"supply-chain-management/materials");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var customersResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IReadOnlyList<MaterialsResponse>>>();
        customersResponse!.IsSuccess.Should().BeTrue();
        customersResponse.Errors.Should().BeEmpty();
        customersResponse.Result.Should().BeEmpty();
    }
}