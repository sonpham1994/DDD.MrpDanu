using Application.SupplyChainManagement.Shared;

namespace Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;

public sealed record MaterialCostManagementResponse(
    decimal Price,
    uint MinQuantity,
    decimal Surcharge,
    SuppliersResponse Supplier);

public sealed record MaterialResponse(
    Guid Id,
    string Code,
    string Name,
    string ColorCode,
    string Width,
    string Weight,
    string Unit,
    string Varian,
    MaterialTypeResponse MaterialType,
    RegionalMarketResponse RegionalMarket,
    IReadOnlyList<MaterialCostManagementResponse> MaterialCostManagements);