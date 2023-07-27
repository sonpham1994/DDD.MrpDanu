using System.Text.RegularExpressions;
using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class Website : ValueObject
{
    public string Value { get; }

    protected Website() { }
    
    private Website(string value) => Value = value;
    
    public static Result<Website?> Create(string value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            return Result.Success((Website)null);

        value = value.Trim();
        if (!Regex.IsMatch(value, @"^http:\/\/(.+)\.\w{2,}$|https:\/\/(.+)\.\w{2,}$"))
            return MaterialManagementDomainErrors.TransactionalPartner.InvalidWebsite(value);

        return new Website(value);
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}