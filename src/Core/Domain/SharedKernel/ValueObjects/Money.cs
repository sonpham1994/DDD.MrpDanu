﻿using Domain.SharedKernel.Base;
using Domain.SharedKernel.Enumerations;

namespace Domain.SharedKernel.ValueObjects;

public class Money : ValueObject
{
    public decimal Value { get; }
    public CurrencyType CurrencyType { get; }

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

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not Money other)
            return false;
        if (Value != other.Value)
            return false;
        if (CurrencyType != other.CurrencyType)
            return false;

        return true;
    }
}