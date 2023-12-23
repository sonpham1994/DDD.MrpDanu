namespace Application.MaterialManagement.MaterialAggregate.Commands;

public sealed record MaterialSupplierCostCommand
{
    public Guid SupplierId { get; init; }
    public decimal Surcharge { get; init; }
    public decimal Price { get; init; }
    public uint MinQuantity { get; init; }
}