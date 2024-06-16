using Infrastructure.Persistence.Reads;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Reads.Models;

internal sealed record TransactionalPartnerReadModel : IReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string TaxNo { get; init; } = default!;
    public string Website { get; init; } = default!;
    public byte LocationTypeId { get; init; }
    public byte TransactionalPartnerTypeId { get; init; }
    public byte CurrencyTypeId { get; init; }
    public string Address_City { get; init; } = default!;
    public string Address_District { get; init; } = default!;
    public string Address_Street { get; init; } = default!;
    public string Address_Ward { get; init; } = default!;
    public string Address_ZipCode { get; init; } = default!;
    public byte CountryId { get; init; }
    public string ContactPersonName { get; init; } = default!;
    public string TelNo { get; init; } = default!;
    public string Email { get; init; } = default!;
}