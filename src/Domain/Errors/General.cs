using Domain.SharedKernel.Base;

namespace Domain.Errors;

public sealed class GeneralDomainErrors
{
    public static DomainError InvalidMoney => new("General.InvalidMoney", "Money should not be less than or equal to 0");
    public static DomainError InvalidVNDCurrencyMoney => new("General.InvalidVNDCurrencyMoney", "Money should be integer for VND currency");
    public static DomainError NullRequestBodyParameter => new("General.NullRequestBodyParameter", "Request body parameter should not be null");
}