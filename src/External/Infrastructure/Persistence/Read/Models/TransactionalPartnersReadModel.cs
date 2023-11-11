namespace Infrastructure.Persistence.Read.Models;

internal sealed record TransactionalPartnersReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string TaxNo { get; init; }
    public string Website { get; init; }
    public byte TransactionalPartnerTypeId { get; init; }
    public byte CurrencyTypeId { get; init; }
}