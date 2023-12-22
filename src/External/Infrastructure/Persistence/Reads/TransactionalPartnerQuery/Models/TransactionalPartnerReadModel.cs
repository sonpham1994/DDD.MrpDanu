namespace Infrastructure.Persistence.Read.TransactionalPartnerQuery.Models;

internal sealed record TransactionalPartnerReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string TaxNo { get; init; }
    public string Website { get; init; }
    public byte LocationTypeId { get; init; }
    public byte TransactionalPartnerTypeId { get; init; }
    public byte CurrencyTypeId { get; init; }
    public string Address_City { get; init; }
    public string Address_District { get; init; }
    public string Address_Street { get; init; }
    public string Address_Ward { get; init; }
    public string Address_ZipCode { get; init; }
    public byte CountryId { get; init; }
    public string ContactPersonName { get; init; }
    public string TelNo { get; init; }
    public string Email { get; init; }
}