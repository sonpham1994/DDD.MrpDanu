using BenchmarkDotNet.Attributes;

namespace Benchmark.BoxingCompareTo;

[MemoryDiagnoser]
public class BoxingCompareToBenchmark
{
    [Benchmark]
    public void CompareToMyClassGenericWithComparable()
    {
        var a = new MyClassGenericWithComparable<int>();
        a.Id = 1;
        var b = new MyClassGenericWithComparable<int>();
        b.Id = 1;

        var c = a.CompareTo(b.Id);
    }
    
    [Benchmark]
    public void CompareToMyClassGenericWithComparableGeneric()
    {
        var a = new MyClassGenericWithComparableGeneric<int>();
        a.Id = 1;
        var b = new MyClassGenericWithComparableGeneric<int>();
        b.Id = 1;

        var c = a.CompareTo(b.Id);
    }
    
    [Benchmark]
    public void CompareToMyClassGenericWithComparableInt()
    {
        var a = new MyClassGenericWithComparableInt();
        a.Id = 1;
        var b = new MyClassGenericWithComparableInt();
        b.Id = 1;

        var c = a.CompareTo(b.Id);
    }
}