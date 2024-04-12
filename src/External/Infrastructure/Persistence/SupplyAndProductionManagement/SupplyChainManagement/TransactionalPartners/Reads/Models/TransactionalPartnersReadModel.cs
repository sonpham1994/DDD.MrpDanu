using Infrastructure.Persistence.Reads;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Reads.Models;

internal sealed record TransactionalPartnersReadModel : IReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string TaxNo { get; init; }
    public string Website { get; init; }
    public byte TransactionalPartnerTypeId { get; init; }
    public byte CurrencyTypeId { get; init; }
}