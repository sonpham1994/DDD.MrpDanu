namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands;

public sealed record AddressCommand
{
    public string Street { get; init; } = default!;
    public string City { get; init; } = default!;
    public string District { get; init; } = default!;
    public string Ward { get; init; } = default!;
    public string ZipCode { get; init; } = default!;
    public byte CountryId { get; init; }
}