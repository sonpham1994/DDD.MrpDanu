using Application.MaterialManagement.Shared;

namespace Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;

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

public sealed record MaterialTypeResponse(byte Id, string Name);

public sealed record RegionalMarketResponse(byte Id, string Name);