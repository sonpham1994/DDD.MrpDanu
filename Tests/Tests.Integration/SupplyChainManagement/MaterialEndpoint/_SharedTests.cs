using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Bogus;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using FluentAssertions;
using MinimalApi.ApiResponses;
using SharedKernel.ValueObjects;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public abstract class SharedTests(HttpClient httpClient) : Tests.Integration.SharedTests(httpClient)
{
    public const string Endpoint = "supply-chain-management/materials";
    protected override string BaseEndpoint => Endpoint;
    protected override string Url => $"{Host}/{Endpoint}";
    
    public static readonly Faker<CreateMaterialCommand> CreateMaterialFaker = new Faker<CreateMaterialCommand>()
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Code, f => "code")
        .RuleFor(x => x.Unit, f => f.Random.Words(1))
        .RuleFor(x => x.Varian, f => f.Random.Words(1))
        .RuleFor(x => x.Weight, f => f.Random.Words(1))
        .RuleFor(x => x.Width, f => f.Random.Words(1))
        .RuleFor(x => x.ColorCode, f => f.Random.Words(1))
        .RuleFor(x => x.MaterialTypeId, MaterialType.Material.Id)
        .RuleFor(x => x.RegionalMarketId, RegionalMarket.None.Id);
    
    public static Faker<UpdateMaterialCommand> UpdateMaterialFaker(Guid id) => new Faker<UpdateMaterialCommand>()
        .RuleFor(x=>x.Id, id)
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Code, f => "code")
        .RuleFor(x => x.Unit, f => f.Random.Words(1))
        .RuleFor(x => x.Varian, f => f.Random.Words(1))
        .RuleFor(x => x.Weight, f => f.Random.Words(1))
        .RuleFor(x => x.Width, f => f.Random.Words(1))
        .RuleFor(x => x.ColorCode, f => f.Random.Words(1))
        .RuleFor(x => x.MaterialTypeId, MaterialType.Material.Id)
        .RuleFor(x => x.RegionalMarketId, RegionalMarket.None.Id);

    public static readonly ApiError ExistedMaterialCode = DomainErrors.Material.ExistedCode("code", Guid.NewGuid()).ToApiError();
    public static readonly ApiError NotFoundMaterial = DomainErrors.Material.NotFoundId(Guid.NewGuid()).ToApiError();

    public async Task<Guid> SetupCreatingMaterialAsync()
    {
        var request = CreateMaterialFaker.Generate();
        var id = await SetupCreatingMaterialAsync(request);
        
        return id;
    }
    
    public async Task<Guid> SetupCreatingMaterialAsync(CreateMaterialCommand request)
    {
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        var result = await ReadFromJsonAsync<Guid>(response);
        var id = result!.Result;
        
        CreatedIds.Add(id);
        
        return id;
    }
    
    public async Task<MaterialResponse> GetMaterialByIdAsync(Guid id)
    {
        var response = await HttpClient.GetAsync($"{Endpoint}/{id}");
        var result = await ReadFromJsonAsync<MaterialResponse>(response);

        return result!.Result;
    }
    
    public async Task<IReadOnlyList<MaterialsResponse>> GetAllMaterialsAsync()
    {
        var response = await HttpClient.GetAsync(Endpoint);
        var result = await ReadFromJsonAsync<IReadOnlyList<MaterialsResponse>>(response);

        return result!.Result;
    }

    protected async Task EnsureEqualAsync(Guid id, CreateMaterialCommand request)
    {
        var material = await GetMaterialByIdAsync(id);
        material.Id.Should().Be(id);
        material.Code.Should().Be(request.Code);
        material.Name.Should().Be(request.Name);
        material.Unit.Should().Be(request.Unit.ToUpper());
        material.Varian.Should().Be(request.Varian);
        material.Weight.Should().Be(request.Weight);
        material.Width.Should().Be(request.Width);
        material.ColorCode.Should().Be(request.ColorCode);
        material.MaterialType.Id.Should().Be(request.MaterialTypeId);
        material.RegionalMarket.Id.Should().Be(request.RegionalMarketId);
        material.MaterialCostManagements.Should().BeEmpty();
    }
    
    protected async Task EnsureEqualAsync(Guid id, UpdateMaterialCommand request)
    {
        var material = await GetMaterialByIdAsync(id);
        material.Id.Should().Be(id);
        material.Code.Should().Be(request.Code);
        material.Name.Should().Be(request.Name);
        material.Unit.Should().Be(request.Unit.ToUpper());
        material.Varian.Should().Be(request.Varian);
        material.Weight.Should().Be(request.Weight);
        material.Width.Should().Be(request.Width);
        material.ColorCode.Should().Be(request.ColorCode);
        material.MaterialType.Id.Should().Be(request.MaterialTypeId);
        material.RegionalMarket.Id.Should().Be(request.RegionalMarketId);
        material.MaterialCostManagements.Should().BeEmpty();
    }
}