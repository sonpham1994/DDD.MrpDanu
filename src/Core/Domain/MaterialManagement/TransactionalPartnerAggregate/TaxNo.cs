using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class TaxNo : ValueObject
{
    private static byte TaxNoVietnamLength => 10;

    public string Value { get; }
    public virtual Country Country { get; }
    
    protected TaxNo() { }

    private TaxNo(string value, Country country) 
    {
        Value = value;
        Country = country;
    }

    public static Result<TaxNo> Create(string value, Country country)
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

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not TaxNo other)
            return false;
        if (Value != other.Value)
            return false;
        if (Country != other.Country) 
            return false;

        return true;
    }
}
