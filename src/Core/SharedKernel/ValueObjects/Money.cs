using SharedKernel;
using SharedKernel.Enumerations;

namespace SharedKernel.ValueObjects;

public class Money : ValueObject<Money>
{
    public decimal Value { get; }
    public CurrencyType CurrencyType { get; } = default!;

    private Money(in decimal value, CurrencyType currencyType)
    {
        Value = value;
        CurrencyType = currencyType;
    }

    protected Money() { }

    public static Result<Money?> Create(in decimal value, CurrencyType currencyType)
    {
        if (value <= 0)
            return DomainErrors.Money.InvalidMoney;

        if (currencyType == CurrencyType.VND)
        {
            if (!decimal.IsInteger(value)) //VND should be integer
                return DomainErrors.Money.InvalidVNDCurrencyMoney;
        }

        return new Money(value, currencyType);
    }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
        yield return CurrencyType.GetHashCode();
    }

    protected override bool EqualComponents(Money other)
        => Value == other.Value 
            && CurrencyType == other.CurrencyType;
}