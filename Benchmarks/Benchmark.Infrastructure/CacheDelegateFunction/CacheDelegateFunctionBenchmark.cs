using Benchmark.Infrastructure.CacheEntityAuditChanged;
using BenchmarkDotNet.Attributes;

namespace Benchmark.Infrastructure.CacheDelegateFunction;

public static class Ext
{
    public static bool Filter(this int element)
    {
        return element % 2 == 0;
    }
}

[MemoryDiagnoser]
public class CacheDelegateFunctionBenchmark
{
    private static Func<int, bool> _filter;
    private List<int> _list;

    private bool Filter(int element)
    {
        return element % 2 == 0;
    }
    
    [Params(1, 10, 100)]
    public int Length { get; set; }
    [GlobalSetup]
    public void Setup()
    {
        _list = [1, 2, 3, 4, 5, 6, 7, 18];
        _filter = new(Filter);
    }
    
    [Benchmark]
    public void Test1Improce()
    {
        for (int i = 0; i < Length; i++)
        {
            _list.Where(_filter).ToList();
        }
    }

    [Benchmark]
    public void Test2()
    {
        for (int i = 0; i < Length; i++)
        {
            _list.Where(x=> x % 2 == 0).ToList();
        }
        
    }
    
    [Benchmark]
    public void Test3()
    {
        for (int i = 0; i < Length; i++)
        {
            _list.Where(x=> Filter(x)).ToList();
        }
    }
    
    [Benchmark]
    public void Test4()
    {
        for (int i = 0; i < Length; i++)
        {
            _list.Where(x=> x.Filter()).ToList();
        }
    }
}