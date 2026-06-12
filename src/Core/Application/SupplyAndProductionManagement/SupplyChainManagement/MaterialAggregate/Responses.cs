namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public sealed record MaterialTypeResponse(byte Id, string Name) : IApiCollectionResponse;

public sealed record RegionalMarketResponse(byte Id, string Name) : IApiCollectionResponse;
