using SharedKernel;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class TaxNo : ValueObject<TaxNo>
{
    private const byte TaxNoVietnamLength = 10;

    public string Value { get; } = default!;
    public virtual Country Country { get; } = default!;

    protected TaxNo() { }

    private TaxNo(string value, Country country)
    {
        Value = value;
        Country = country;
    }

    public static Result<TaxNo?> Create(string? value, Country country)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            return DomainErrors.TransactionalPartner.EmptyTaxNo;

        value = value.Trim();

        if (country == Country.VietNam)
        {
            if (value.Length != TaxNoVietnamLength)
                return DomainErrors.TransactionalPartner.InvalidLengthTaxNo;
            if (!value.All(char.IsDigit))
                return DomainErrors.TransactionalPartner.InvalidTaxNo;
        }

        return new TaxNo(value, country);
    }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
        yield return Country.GetHashCode();
    }

    protected override bool EqualComponents(TaxNo other) 
        => Value == other.Value && Country == other.Country;
}
