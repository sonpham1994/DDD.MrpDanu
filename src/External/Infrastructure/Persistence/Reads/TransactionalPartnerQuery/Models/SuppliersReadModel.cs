namespace Infrastructure.Persistence.Reads.TransactionalPartnerQuery.Models;

internal sealed record SuppliersReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public byte CurrencyTypeId { get; init; }
}