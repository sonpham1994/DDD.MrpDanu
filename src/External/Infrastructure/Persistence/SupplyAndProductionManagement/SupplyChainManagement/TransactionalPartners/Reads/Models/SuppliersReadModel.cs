using Infrastructure.Persistence.Reads;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Reads.Models;

internal sealed record SuppliersReadModel : IReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public byte CurrencyTypeId { get; init; }
}