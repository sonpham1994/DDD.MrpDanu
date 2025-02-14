using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Bogus;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Shouldly;
using MinimalApi.ApiResponses;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

[Collection(CollectionEndpointName.MaterialEndpoint)]
public abstract class SharedTests(EndpointFactory endpointFactory) 
    : Tests.Integration.SharedTests(endpointFactory)
{
    protected const string Endpoint = "supply-chain-management/materials";
    protected override string BaseEndpoint => Endpoint;
    protected override string Url => $"{Host}/{Endpoint}";

    protected static readonly Faker<CreateMaterialCommand> CreateMaterialFaker = new Faker<CreateMaterialCommand>()
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

    private static Faker<MaterialSupplierCostCommand> CreateMaterialCostFaker(Guid id) =>
        new Faker<MaterialSupplierCostCommand>()
            .RuleFor(x => x.Price, f => decimal.Round(f.Random.Decimal(0.1m, 1_000_000_000), 4)) // can be exceeded length in db, TODO: Write test for it
            .RuleFor(x => x.Surcharge, f => decimal.Round(f.Random.Decimal(0.1m, 1_000_000_000), 4)) // can be exceeded length in db, TODO: Write test for it
            .RuleFor(x => x.MinQuantity, f => f.Random.UInt(1, 1_000_000_000)) // can be exceeded length in db, TODO: Write test for it
            .RuleFor(x => x.SupplierId, id);

    private static MaterialSupplierCostCommand[] CreateMaterialCosts(Guid[] ids)
    {
        var result = new MaterialSupplierCostCommand[ids.Length];

        for (int i = 0; i < ids.Length; i++)
        {
            result[i] = CreateMaterialCostFaker(ids[i]).Generate();
        }

        return result;
    }

    protected static Faker<CreateMaterialCommand> CreateMaterialWithMaterialCostsFaker(Guid[] supplierIds) =>
        CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.MaterialCosts, CreateMaterialCosts(supplierIds));

    protected static Faker<CreateMaterialCommand> CreateMaterialWithMaterialCostFaker(Guid supplierId) =>
        CreateMaterialWithMaterialCostsFaker([supplierId]);

    protected static Faker<UpdateMaterialCommand> UpdateMaterialFaker(Guid id) => new Faker<UpdateMaterialCommand>()
        .RuleFor(x => x.Id, id)
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Code, f => "code")
        .RuleFor(x => x.Unit, f => f.Random.Words(1))
        .RuleFor(x => x.Varian, f => f.Random.Words(1))
        .RuleFor(x => x.Weight, f => f.Random.Words(1))
        .RuleFor(x => x.Width, f => f.Random.Words(1))
        .RuleFor(x => x.ColorCode, f => f.Random.Words(1))
        .RuleFor(x => x.MaterialTypeId, MaterialType.Material.Id)
        .RuleFor(x => x.RegionalMarketId, RegionalMarket.None.Id);
    
    protected static Faker<UpdateMaterialCommand> UpdateMaterialWithMaterialCostsFaker(Guid materialId, Guid[] supplierIds) =>
        UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.MaterialCosts, CreateMaterialCosts(supplierIds));

    protected static Faker<UpdateMaterialCommand> UpdateMaterialWithMaterialCostFaker(Guid materialId, Guid supplierId) =>
        UpdateMaterialWithMaterialCostsFaker(materialId, [supplierId]);

    protected static readonly ApiError ExistedMaterialCode = DomainErrors.Material.ExistedCode("code", Guid.NewGuid()).ToApiError();
    protected static readonly ApiError NotFoundMaterialId = DomainErrors.Material.NotFoundId(Guid.NewGuid()).ToApiError();
    protected static readonly ApiError NotFoundSupplierId = DomainErrors.MaterialSupplierCost.NotFoundSupplierId(Guid.NewGuid()).ToApiError();
    protected static readonly ApiError DuplicatedSupplierId = DomainErrors.MaterialSupplierCost.DuplicationSupplierId(Guid.NewGuid()).ToApiError();
    
    protected async Task<Guid> SetupCreatingMaterialAsync()
    {
        var request = CreateMaterialFaker.Generate();
        var id = await SetupCreatingMaterialAsync(request);
        
        return id;
    }

    protected async Task<Guid> SetupCreatingMaterialAsync(CreateMaterialCommand request)
    {
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        var result = await ReadFromJsonAsync<Guid>(response);
        var id = result!.Result;
        
        CreatedIds.Add(id);
        
        return id;
    }
    
    protected async Task<Guid> SetupCreatingMaterialWithCostAsync(Guid[] supplierIds)
    {
        var request = CreateMaterialWithMaterialCostsFaker(supplierIds).Generate();
        var id = await SetupCreatingMaterialAsync(request);
        
        return id;
    }
    
    protected async Task<Guid> SetupCreatingMaterialWithCostAsync(Guid supplierId)
    {
        var request = CreateMaterialWithMaterialCostFaker(supplierId).Generate();
        var id = await SetupCreatingMaterialAsync(request);
        
        return id;
    }

    protected async Task<MaterialResponse> GetMaterialByIdAsync(Guid id)
    {
        var response = await HttpClient.GetAsync($"{Endpoint}/{id}");
        var result = await ReadFromJsonAsync<MaterialResponse>(response);

        return result!.Result;
    }

    protected async Task<IReadOnlyList<MaterialsResponse>> GetAllMaterialsAsync()
    {
        var response = await HttpClient.GetAsync(Endpoint);
        var result = await ReadFromJsonAsync<IReadOnlyList<MaterialsResponse>>(response);

        return result!.Result;
    }

    protected async Task EnsureEqualAsync(Guid id, CreateMaterialCommand request)
    {
        var material = await GetMaterialByIdAsync(id);
        material.Id.ShouldBe(id);
        material.Code.ShouldBe(request.Code);
        material.Name.ShouldBe(request.Name);
        material.Unit.ShouldBe(request.Unit.ToUpper());
        material.Varian.ShouldBe(request.Varian);
        material.Weight.ShouldBe(request.Weight);
        material.Width.ShouldBe(request.Width);
        material.ColorCode.ShouldBe(request.ColorCode);
        material.MaterialType.Id.ShouldBe(request.MaterialTypeId);
        material.RegionalMarket.Id.ShouldBe(request.RegionalMarketId);
        material.MaterialCostManagements.ShouldBeEmpty();
    }
    
    protected async Task EnsureEqualAsync(Guid id, UpdateMaterialCommand request)
    {
        var material = await GetMaterialByIdAsync(id);
        material.Id.ShouldBe(id);
        material.Code.ShouldBe(request.Code);
        material.Name.ShouldBe(request.Name);
        material.Unit.ShouldBe(request.Unit.ToUpper());
        material.Varian.ShouldBe(request.Varian);
        material.Weight.ShouldBe(request.Weight);
        material.Width.ShouldBe(request.Width);
        material.ColorCode.ShouldBe(request.ColorCode);
        material.MaterialType.Id.ShouldBe(request.MaterialTypeId);
        material.RegionalMarket.Id.ShouldBe(request.RegionalMarketId);
        material.MaterialCostManagements.ShouldBeEmpty();
    }
    
    protected async Task EnsureMaterialCostEqualAsync(Guid id, IReadOnlyList<MaterialSupplierCostCommand> request)
    {
        var material = await GetMaterialByIdAsync(id);
        request.Count.ShouldBe(material.MaterialCostManagements.Count);
        
        foreach (var materialCostRequest in request)
        {
            var materialCost = material.MaterialCostManagements
                .FirstOrDefault(x => x.Supplier.Id == materialCostRequest.SupplierId);

            materialCost.ShouldNotBeNull();
            materialCost.Supplier.Id.ShouldBe(materialCostRequest.SupplierId);
            materialCost.Price.ShouldBe(materialCostRequest.Price);
            materialCost.Surcharge.ShouldBe(materialCostRequest.Surcharge);
            materialCost.MinQuantity.ShouldBe(materialCostRequest.MinQuantity);
        }
    }
}