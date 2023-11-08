using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace Benchmark.RegexBenchmarks;

[MemoryDiagnoser]
public class RegexBenchmark
{
    //Max 200
    private static readonly Regex EmailPattern = new(@"^(.+)@(.+)\.\w{2,}$", RegexOptions.Compiled);
    
    //2000
    private static readonly Regex UniqueCodePattern = new("[^A-Za-z0-9]", RegexOptions.Compiled);
    
    //100
    private static readonly Regex WebsitePattern = new(@"^http:\/\/(.+)\.\w{2,}$|https:\/\/(.+)\.\w{2,}$", RegexOptions.Compiled);
    
    private List<string> _emails = new();
    
    [Params(10, 100, 1000)]
    public int Length {get; set; }
    
    private string _email;
    private string _uniqueCode;
    private string _website;
    
    [GlobalSetup]
    public void Setup()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        
        for (int i = 0; i < Length; i++)
        {
            var email = new string(Enumerable.Repeat(chars, 100)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            
            if (i % 2 == 0)
                email = email + "@gmail.com";
            
            _emails.Add(email);
        }

        _email = new string(Enumerable.Repeat(chars, 200)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        _uniqueCode = new string(Enumerable.Repeat(chars, 2000)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        _website = "http://" + new string(Enumerable.Repeat(chars, 85)
            .Select(s => s[random.Next(s.Length)]).ToArray()) + ".com";
    }
    
    [Benchmark]
    public void RegexWithEmailPattern()
    {
        foreach (var email in _emails)
        {
            var result = Regex.IsMatch(email, @"^(.+)@(.+)\.\w{2,}$");
        }
    }
    
    [Benchmark]
    public void RegexWithEmailPatternStatic()
    {
        foreach (var email in _emails)
        {
            var result = EmailPattern.IsMatch(email);
        }
    }
    
    [Benchmark]
    public void RegexWithEmailPatternForCalculatingMaximumTimeout()
    {
        var result = EmailPattern.IsMatch(_email);
    }
    
    [Benchmark]
    public void RegexWithUniqueCodePatternForCalculatingMaximumTimeout()
    {
        var result = UniqueCodePattern.IsMatch(_uniqueCode);
    }
    
    [Benchmark]
    public void RegexWithWebsitePatternForCalculatingMaximumTimeout()
    {
        var result = WebsitePattern.IsMatch(_website);
    }
}