namespace Infrastructure.Persistence.Read.Models;

internal sealed record SuppliersReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public byte CurrencyTypeId { get; init; }
}