namespace Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;

public sealed record TransactionalPartnerResponse(Guid Id, 
    string Name, 
    string TaxNo, 
    string Website, 
    TransactionalPartnerTypeResponse TransactionalPartnerType,
    CurrencyTypeResponse CurrencyType,
    AddressResponse Address, 
    ContactPersonInformationResponse ContactPersonInformation,
    LocationTypeResponse LocationType);