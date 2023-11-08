using System.Text.RegularExpressions;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class Website : ValueObject
{
    //https://frugalcafe.beehiiv.com/p/reuse-regular-expressions
    //https://www.youtube.com/watch?v=RSFiiKUvzLI&ab_channel=NickChapsas
    private static readonly Regex WebsitePattern = new(@"^http:\/\/(.+)\.\w{2,}$|https:\/\/(.+)\.\w{2,}$", 
        RegexOptions.Compiled,
        //152.16 ns from Benchmark.RegexBenchmarks
        TimeSpan.FromMilliseconds(100));
    
    private static byte WebsiteLength => 100;
    
    public string Value { get; }

    protected Website() { }
    
    private Website(string value) => Value = value;
    
    public static Result<Website?> Create(string value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            return Result.Success(default(Website));

        value = value.Trim();
        if (value.Length > WebsiteLength)
            return DomainErrors.TransactionalPartner.ExceedsMaxLengthWebsite(WebsiteLength);
        if (!WebsitePattern.IsMatch(value))
            return DomainErrors.TransactionalPartner.InvalidWebsite(value);

        return new Website(value);
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}