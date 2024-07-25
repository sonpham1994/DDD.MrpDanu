using BenchmarkDotNet.Attributes;

namespace Benchmark.StringBenchmarks;

[MemoryDiagnoser]
public class TrimBenchmark
{
    private string Hello = "Hello World";
    private string HelloWithTrailingAndLeadingSpace = " Hello World ";

    [Benchmark]
    public void TrimHello()
    {
        var _ = Hello.Trim();
    }
    
    [Benchmark]
    public void TrimHelloWithTrailingAndLeadingSpace()
    {
        var _ = HelloWithTrailingAndLeadingSpace.Trim();
    }

    [Benchmark]
    public void CheckWhiteSpaceHello()
    {
        if (Hello[0] == ' ' || Hello[Hello.Length - 1] == ' ')
        {
            var _ = Hello.Trim();
        }
    }
    
    [Benchmark]
    public void CheckWhiteSpaceHelloWithTrailingAndLeadingSpace()
    {
        if (HelloWithTrailingAndLeadingSpace[0] == ' ' || HelloWithTrailingAndLeadingSpace[HelloWithTrailingAndLeadingSpace.Length - 1] == ' ')
        {
            var _ = HelloWithTrailingAndLeadingSpace.Trim();
        }
    }
}