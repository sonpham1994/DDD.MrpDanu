namespace Infrastructure.Persistence.Read.Models;

internal sealed record MaterialCostReadModel
{
    public Guid Id { get; init; }
    public decimal Surcharge { get; init; }
    public uint MinQuantity { get; init; }
    public decimal Price { get; init; }

    public Guid SupplierId { get; init; }
    public string SupplierName { get; init; }
    public byte CurrencyTypeId { get; init; }
}