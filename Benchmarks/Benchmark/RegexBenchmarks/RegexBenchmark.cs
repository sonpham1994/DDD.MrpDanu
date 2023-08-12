using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace Benchmark.RegexBenchmarks;

[MemoryDiagnoser]
public class RegexBenchmark
{
    private static readonly Regex EmailPattern = new(@"^(.+)@(.+)\.\w{2,}$", RegexOptions.Compiled);
    private List<string> _emails = new();
    
    [Params(10, 100, 1000)]
    public int Length {get; set; }
    
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
}