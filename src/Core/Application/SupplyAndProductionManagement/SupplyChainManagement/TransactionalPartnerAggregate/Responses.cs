namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public sealed record CountryResponse(byte Id, string Name) : IApiCollectionResponse;

public sealed record CurrencyTypeResponse(byte Id, string Name) : IApiCollectionResponse;

public sealed record LocationTypeResponse(byte Id, string Name) : IApiCollectionResponse;

public sealed record TransactionalPartnerTypeResponse(byte Id, string Name) : IApiCollectionResponse;