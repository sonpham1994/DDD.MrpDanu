namespace Infrastructure.Persistence.Reads.TransactionalPartnerQuery.Models;

internal sealed record SuppliersReadModel : IReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public byte CurrencyTypeId { get; init; }
}