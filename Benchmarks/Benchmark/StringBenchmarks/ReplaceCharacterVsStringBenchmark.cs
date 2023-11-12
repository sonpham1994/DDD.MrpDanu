using BenchmarkDotNet.Attributes;

namespace Benchmark.StringBenchmarks;

[MemoryDiagnoser]
public class ReplaceCharacterVsStringBenchmark
{
    private const string s = "-AhxC5qsam0mRI+8C1p_9Jug==";
    [Benchmark]
    public void ReplaceChar()
    {
        var b = s.Replace('-', '/')
            .Replace('_', '+')
            .Replace('=', '/');
    }
    
    [Benchmark]
    public void ReplaceString()
    {
        var b = s.Replace("-", "/")
            .Replace("_", "+")
            .Replace("=", "/");
    }
}