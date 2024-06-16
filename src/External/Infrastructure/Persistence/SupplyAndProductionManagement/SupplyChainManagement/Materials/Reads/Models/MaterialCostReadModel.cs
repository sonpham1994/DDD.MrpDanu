using Infrastructure.Persistence.Reads;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.Materials.Reads.Models;

internal sealed record MaterialCostReadModel : IReadModel
{
    public Guid Id { get; init; }
    public decimal Surcharge { get; init; }
    public uint MinQuantity { get; init; }
    public decimal Price { get; init; }

    public Guid SupplierId { get; init; }
    public string SupplierName { get; init; } = default!;
    public byte CurrencyTypeId { get; init; }
}