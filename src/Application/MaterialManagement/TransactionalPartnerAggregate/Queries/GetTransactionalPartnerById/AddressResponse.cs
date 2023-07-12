namespace Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;

public sealed record AddressResponse(string City, 
    string District, 
    string Street, 
    string Ward, 
    string ZipCode, 
    CountryResponse Country);