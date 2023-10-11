namespace Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;

public sealed record TransactionalPartnerResponse(
    Guid Id,
    string Name,
    string TaxNo,
    string Website,
    TransactionalPartnerTypeResponse TransactionalPartnerType,
    CurrencyTypeResponse CurrencyType,
    AddressResponse Address,
    ContactPersonInformationResponse ContactPersonInformation,
    LocationTypeResponse LocationType);

public sealed record AddressResponse(
    string City,
    string District,
    string Street,
    string Ward,
    string ZipCode,
    CountryResponse Country);

public sealed record ContactPersonInformationResponse(string Name, string TelNo, string Email);

public sealed record CountryResponse(byte Id, string Name);

public sealed record CurrencyTypeResponse(byte Id, string Name);

public sealed record LocationTypeResponse(byte Id, string Name);

public sealed record TransactionalPartnerTypeResponse(byte Id, string Name);