using System.Text.RegularExpressions;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class Website : ValueObject
{
    //https://frugalcafe.beehiiv.com/p/reuse-regular-expressions
    private static readonly Regex WebsitePattern = new(@"^http:\/\/(.+)\.\w{2,}$|https:\/\/(.+)\.\w{2,}$", RegexOptions.Compiled);
    
    public string Value { get; }

    protected Website() { }
    
    private Website(string value) => Value = value;
    
    public static Result<Website?> Create(string value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            return Result.Success(default(Website));

        value = value.Trim();
        if (!WebsitePattern.IsMatch(value))
            return DomainErrors.TransactionalPartner.InvalidWebsite(value);

        return new Website(value);
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}