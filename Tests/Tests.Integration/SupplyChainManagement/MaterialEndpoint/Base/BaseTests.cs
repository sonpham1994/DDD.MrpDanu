using System.Net;
using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Bogus;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Microsoft.AspNetCore.Http;
using MinimalApi.ApiResponses;
using SharedKernel.ValueObjects;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint.Base;

public abstract class BaseTests : IAsyncLifetime
{
    protected readonly HttpClient HttpClient;
    
    protected BaseTests(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
    
    public const string Host = "http://localhost";
    public const string Endpoint = "supply-chain-management/materials";
    public const string Url = $"{Host}/{Endpoint}";
    protected List<Guid> CreatedIds { get; } = new();
    
    public static readonly Faker<CreateMaterialCommand> CreateMaterialFaker = new Faker<CreateMaterialCommand>()
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Code, f => "code")
        .RuleFor(x => x.Unit, f => f.Random.Words(5))
        .RuleFor(x => x.Varian, f => f.Random.Words(5))
        .RuleFor(x => x.Weight, f => f.Random.Words(5))
        .RuleFor(x => x.Width, f => f.Random.Words(5))
        .RuleFor(x => x.ColorCode, f => f.Random.Words(5))
        .RuleFor(x => x.MaterialTypeId, MaterialType.Material.Id)
        .RuleFor(x => x.RegionalMarketId, RegionalMarket.None.Id);
    
    public static Faker<UpdateMaterialCommand> UpdateMaterialFaker(Guid id) => new Faker<UpdateMaterialCommand>()
        .RuleFor(x=>x.Id, id)
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Code, f => "code")
        .RuleFor(x => x.Unit, f => f.Random.Words(5))
        .RuleFor(x => x.Varian, f => f.Random.Words(5))
        .RuleFor(x => x.Weight, f => f.Random.Words(5))
        .RuleFor(x => x.Width, f => f.Random.Words(5))
        .RuleFor(x => x.ColorCode, f => f.Random.Words(5))
        .RuleFor(x => x.MaterialTypeId, MaterialType.Material.Id)
        .RuleFor(x => x.RegionalMarketId, RegionalMarket.None.Id);

    public static readonly ApiError ExistedMaterialCode = new(DomainErrors.Material.ExistedCode(MaterialCode.Create("code").Value, new MaterialId(Guid.Empty)).Code, "message");
    public static readonly ApiError NotFoundMaterial = new(DomainErrors.Material.NotFoundId(Guid.NewGuid()).Code, "message");

    public async Task<Guid> SetupCreatingMaterialAsync()
    {
        var materialRequest = CreateMaterialFaker.Generate();
        var materialId = await SetupCreatingMaterialAsync(materialRequest);
        
        return materialId;
    }
    
    public async Task<Guid> SetupCreatingMaterialAsync(CreateMaterialCommand materialRequest)
    {
        var response = await HttpClient.PostAsJsonAsync(Endpoint, materialRequest);
        var materialCreatedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>();
        var materialId = materialCreatedResponse!.Result;
        
        CreatedIds.Add(materialId);
        
        return materialId;
    }
    
    public async Task<MaterialResponse> GetMaterialByIdAsync(Guid id)
    {
        var materialResponse = await HttpClient.GetAsync($"{Endpoint}/{id}");
        var materialApiResponse = await materialResponse.Content.ReadFromJsonAsync<ApiResponse<MaterialResponse>>();

        return materialApiResponse!.Result;
    }
    
    public async Task<IReadOnlyList<MaterialsResponse>> GetAllMaterialsAsync()
    {
        var materialsResponse = await HttpClient.GetAsync(Endpoint);
        var materialApiResponses = await materialsResponse.Content.ReadFromJsonAsync<ApiResponse<IReadOnlyList<MaterialsResponse>>>();
        var materials = materialApiResponses!.Result;

        return materials;
    }
    
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        foreach (var createdId in CreatedIds)
        {
            await HttpClient.DeleteAsync($"{Endpoint}/{createdId}");
        }
    }
}