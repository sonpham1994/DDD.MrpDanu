using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using SharedKernel;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class Website : ValueObject<Website>
{
    //https://frugalcafe.beehiiv.com/p/reuse-regular-expressions
    //https://www.youtube.com/watch?v=RSFiiKUvzLI&ab_channel=NickChapsas
    private static readonly Regex WebsitePattern = new(@"^http:\/\/(.+)\.\w{2,}$|https:\/\/(.+)\.\w{2,}$",
        RegexOptions.Compiled,
        //152.16 ns from Benchmark.RegexBenchmarks
        // why we need timeout for Regex: https://www.youtube.com/watch?v=NOLn0QwGlEE&ab_channel=NickChapsas
        TimeSpan.FromMilliseconds(250));

    private const byte WebsiteLength = 100;

    public string Value { get; } = default!;

    protected Website() { }

    private Website(string value) => Value = value;

    //String meaning: https://www.youtube.com/shorts/-7CJOU0QJC0
    public static Result<Website?> Create([StringSyntax(StringSyntaxAttribute.Uri)]string? value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            return DomainErrors.TransactionalPartner.EmptyWebsiteValue;

        value = value!.Trim();
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

    protected override bool EqualComponents(Website other) 
        => Value == other.Value;
}