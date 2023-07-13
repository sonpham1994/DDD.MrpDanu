using Domain.SharedKernel.Base;

namespace Domain.Errors;

public static partial class DomainErrors
{
    public static class General
    {
        public static readonly DomainError InvalidMoney = new("General.InvalidMoney", "Money should not be less than or equal to 0");
        public static readonly DomainError InvalidVNDCurrencyMoney = new("General.InvalidVNDCurrencyMoney", "Money should be integer for VND currency");
        public static readonly DomainError NullRequestBodyParameter = new("General.NullRequestBodyParameter", "Request body parameter should not be null");
    }
}