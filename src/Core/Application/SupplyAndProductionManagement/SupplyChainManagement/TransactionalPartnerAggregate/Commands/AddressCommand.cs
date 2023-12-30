namespace Application.SupplyChainManagement.TransactionalPartnerAggregate.Commands;

public sealed record AddressCommand
{
    public string Street { get; init; }
    public string City { get; init; }
    public string District { get; init; }
    public string Ward { get; init; }
    public string ZipCode { get; init; }
    public byte CountryId { get; init; }
}