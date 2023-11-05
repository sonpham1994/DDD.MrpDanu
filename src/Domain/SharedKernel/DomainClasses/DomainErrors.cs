using Domain.SharedKernel.Base;

namespace Domain.SharedKernel.DomainClasses;

public sealed class DomainErrors
{
    public static class CurrencyType
    {
        public static DomainError NotFoundId(in byte id) => new("CurrencyType.NotFoundId", $"Currency type id '{id}' does not exist.");
    }

    public static class Money
    {
        public static DomainError InvalidMoney => new("Money.InvalidMoney", "Money should not be less than or equal to 0");
        public static DomainError InvalidVNDCurrencyMoney => new("Money.InvalidVNDCurrencyMoney", "Money should be integer for VND currency");
    }
    
    public static DomainError NullRequestBodyParameter => new("General.NullRequestBodyParameter", "Request body parameter should not be null");
}