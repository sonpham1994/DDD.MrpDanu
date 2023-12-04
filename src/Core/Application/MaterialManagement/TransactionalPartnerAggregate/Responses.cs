namespace Application.MaterialManagement.TransactionalPartnerAggregate;

public sealed record CountryResponse(byte Id, string Name);

public sealed record CurrencyTypeResponse(byte Id, string Name);

public sealed record LocationTypeResponse(byte Id, string Name);

public sealed record TransactionalPartnerTypeResponse(byte Id, string Name);