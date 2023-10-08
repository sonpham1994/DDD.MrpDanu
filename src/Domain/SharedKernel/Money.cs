using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.SharedKernel;

public class Money : ValueObject
{
    public decimal Value { get; }
    public virtual CurrencyType CurrencyType { get; }

    private Money(in decimal value, CurrencyType currencyType)
    {
        Value = value;
        CurrencyType = currencyType;
    }

    protected Money() { }

    public static Result<Money> Create(in decimal value, CurrencyType currencyType)
    {
        if (value <= 0)
            return GeneralDomainErrors.InvalidMoney;
        
        if (currencyType == CurrencyType.VND)
        {
            if (!decimal.IsInteger(value)) //VND should be integer
                return GeneralDomainErrors.InvalidVNDCurrencyMoney;
        }

        return new Money(value, currencyType);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
        yield return CurrencyType;
    }
}