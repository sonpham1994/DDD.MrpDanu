using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class TaxNo : ValueObject
{
    private const byte TaxNoVietnamLength = 10;

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
            return MaterialManagementDomainErrors.TransactionalPartner.EmptyTaxNo;

        value = value.Trim();

        if (country == Country.VietNam)
        {
            if (value.Length != TaxNoVietnamLength)
                return MaterialManagementDomainErrors.TransactionalPartner.InvalidLengthTaxNo;
            if (!value.All(char.IsDigit))
                return MaterialManagementDomainErrors.TransactionalPartner.InvalidTaxNo;
        }
        
        return new TaxNo(value, country);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
        yield return Country;
    }
}
