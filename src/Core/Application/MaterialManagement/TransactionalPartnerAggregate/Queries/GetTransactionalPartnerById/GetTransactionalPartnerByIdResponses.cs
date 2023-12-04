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