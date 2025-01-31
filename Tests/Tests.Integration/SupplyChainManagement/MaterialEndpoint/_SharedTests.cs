using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands;
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

[Collection(CollectionEndpointName.MaterialEndpoint)]
public abstract class SharedTests(EndpointFactory endpointFactory) 
    : Tests.Integration.SharedTests(endpointFactory.CreateClient())
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
        .RuleFor(x => x.RegionalMarketId, RegionalMarket.None.Id)
        .RuleFor(x => x.MaterialCosts, Array.Empty<MaterialSupplierCostCommand>());

    public static Faker<MaterialSupplierCostCommand> CreateMaterialCostFaker(Guid id) =>
        new Faker<MaterialSupplierCostCommand>()
            .RuleFor(x => x.Price, f => decimal.Round(f.Random.Decimal(0.1m, 1_000_000_000), 4)) // can be exceeded length in db, TODO: Write test for it
            .RuleFor(x => x.Surcharge, f => decimal.Round(f.Random.Decimal(0.1m, 1_000_000_000), 4)) // can be exceeded length in db, TODO: Write test for it
            .RuleFor(x => x.MinQuantity, f => f.Random.UInt(1, 1_000_000_000)) // can be exceeded length in db, TODO: Write test for it
            .RuleFor(x => x.SupplierId, id);

    public static MaterialSupplierCostCommand[] CreateMaterialCosts(Guid[] ids)
    {
        var result = new MaterialSupplierCostCommand[ids.Length];

        for (int i = 0; i < ids.Length; i++)
        {
            result[i] = CreateMaterialCostFaker(ids[i]).Generate();
        }

        return result;
    }

    public static Faker<CreateMaterialCommand> CreateMaterialWithMaterialCostsFaker(Guid[] supplierIds) =>
        CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.MaterialCosts, CreateMaterialCosts(supplierIds));

    public static Faker<CreateMaterialCommand> CreateMaterialWithMaterialCostFaker(Guid supplierId) =>
        CreateMaterialWithMaterialCostsFaker([supplierId]);
    
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
    }
    
    protected async Task EnsureMaterialCostEqualAsync(Guid id, CreateMaterialCommand request)
    {
        var material = await GetMaterialByIdAsync(id);
        request.MaterialCosts.Should().HaveCount(material.MaterialCostManagements.Count);
        
        for (int i = 0; i < request.MaterialCosts.Count; i++)
        {
            var materialCostRequest = request.MaterialCosts[i];
            var materialCost = material.MaterialCostManagements
                .FirstOrDefault(x => x.Supplier.Id == materialCostRequest.SupplierId);

            materialCost.Should().NotBeNull();
            materialCost.Supplier.Id.Should().Be(materialCostRequest.SupplierId);
            materialCost.Price.Should().Be(materialCostRequest.Price);
            materialCost.Surcharge.Should().Be(materialCostRequest.Surcharge);
            materialCost.MinQuantity.Should().Be(materialCostRequest.MinQuantity);
        }
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