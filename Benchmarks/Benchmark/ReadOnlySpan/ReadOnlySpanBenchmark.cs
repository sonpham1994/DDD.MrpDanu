using BenchmarkDotNet.Attributes;

namespace Benchmark.ReadOnlySpan;

[MemoryDiagnoser()]
public class ReadOnlySpanBenchmark
{
    [Benchmark]
    public void ReadOnlySpanWithStackAlloc()
    {
        ReadOnlySpan<char> test = stackalloc char[] { 'a', 'b', 'c' };
    }
    
    [Benchmark]
    public void ReadOnlySpanWithCollectionExpression()
    {
        ReadOnlySpan<char> test = ['a', 'b', 'c'];
    }
    
    [Benchmark]
    public void ReadOnlySpanStackAllocWithString()
    {
        ReadOnlySpan<char> test = stackalloc char[] { 'a', 'b', 'c' };
        var b = new string(test);
    }
    
    [Benchmark]
    public void ReadOnlySpanCollectionExpressionWithString()
    {
        ReadOnlySpan<char> test = ['a', 'b', 'c'];
        var b = new string(test);
    }
    
    [Benchmark]
    public void CharArrayWithString()
    {
        char[] test = ['a', 'b', 'c'];
        var b = new string(test);
    }
}