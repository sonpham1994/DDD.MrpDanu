using BenchmarkDotNet.Attributes;

namespace Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;

[MemoryDiagnoser()]
public class ItemDuplicationWithCheckingTypeBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }
    public class MyClass : Entity
    {
        public string Name {get; set; }
    }
    
    private List<MyClass> _myclasses1 = new();
    private List<MyClass> _myclasses2 = new();

    [GlobalSetup]
    public void Setup()
    {
        for(int i =0; i < Length; i++)
        {
            _myclasses1.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
            _myclasses2.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
        }
        _myclasses2.Insert(Length - 1, _myclasses2[Length / 2]);
    }

    [Benchmark]
    public void Enumerable()
    {
        var a = _myclasses1.ItemDuplicationWithAlgorithmAndHashCode(x => x);
    }
    
    [Benchmark]
    public void EnumerableWithCheckingList()
    {
        var a = _myclasses1.ItemDuplicationWithAlgorithmAndHashCodeWithCheckingList(x => x);
    }
    
    [Benchmark]
    public void EnumerableWithCheckingIReadOnlyList()
    {
        var a = _myclasses1.ItemDuplicationWithAlgorithmAndHashCodeWithCheckingIReadOnlyList(x => x);
    }
    
    [Benchmark]
    public void EnumerableWithDuplication()
    {
        var a = _myclasses2.ItemDuplicationWithAlgorithmAndHashCode(x => x);
    }
    
    [Benchmark]
    public void EnumerableWithCheckingListWithDuplication()
    {
        var a = _myclasses2.ItemDuplicationWithAlgorithmAndHashCodeWithCheckingList(x => x);
    }
    
    [Benchmark]
    public void EnumerableWithCheckingIReadOnlyListWithDuplication()
    {
        var a = _myclasses2.ItemDuplicationWithAlgorithmAndHashCodeWithCheckingIReadOnlyList(x => x);
    }
}