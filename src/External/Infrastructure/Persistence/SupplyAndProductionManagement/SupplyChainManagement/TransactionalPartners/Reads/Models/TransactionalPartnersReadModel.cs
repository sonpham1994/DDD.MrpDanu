using Infrastructure.Persistence.Reads;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Reads.Models;

internal sealed record TransactionalPartnersReadModel : IReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string TaxNo { get; init; } = default!;
    public string Website { get; init; } = default!;
    public byte TransactionalPartnerTypeId { get; init; }
    public byte CurrencyTypeId { get; init; }
}