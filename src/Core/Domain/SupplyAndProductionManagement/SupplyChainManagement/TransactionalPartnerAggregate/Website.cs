using System.Text.RegularExpressions;
using Domain.SharedKernel.Base;
using Domain.SupplyAndProductionManagement.SupplyChainManagement;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class Website : ValueObject
{
    //https://frugalcafe.beehiiv.com/p/reuse-regular-expressions
    //https://www.youtube.com/watch?v=RSFiiKUvzLI&ab_channel=NickChapsas
    private static readonly Regex WebsitePattern = new(@"^http:\/\/(.+)\.\w{2,}$|https:\/\/(.+)\.\w{2,}$",
        RegexOptions.Compiled,
        //152.16 ns from Benchmark.RegexBenchmarks
        // why we need timeout for Regex: https://www.youtube.com/watch?v=NOLn0QwGlEE&ab_channel=NickChapsas
        TimeSpan.FromMilliseconds(250));

    private static byte WebsiteLength => 100;

    public string Value { get; }

    protected Website() { }

    private Website(string value) => Value = value;

    public static Result<Website?> Create(string value)
    {
        //note: should remove this one, whether the website is null or not, it will be decided based on bounded context
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            return Result.Success(default(Website));

        value = value.Trim();
        if (value.Length > WebsiteLength)
            return DomainErrors.TransactionalPartner.ExceedsMaxLengthWebsite(WebsiteLength);
        if (!WebsitePattern.IsMatch(value))
            return DomainErrors.TransactionalPartner.InvalidWebsite(value);

        return new Website(value);
    }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    protected override bool EqualComponents(ValueObject valueObject)
    {
        if (valueObject is not Website other)
            return false;
        if (Value != other.Value)
            return false;

        return true;
    }
}