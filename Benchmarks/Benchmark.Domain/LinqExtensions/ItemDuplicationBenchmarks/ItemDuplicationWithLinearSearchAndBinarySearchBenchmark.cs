using BenchmarkDotNet.Attributes;

namespace Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;

[MemoryDiagnoser()]
public class ItemDuplicationWithLinearSearchAndBinarySearchBenchmark
{
    [Params(10, 100, 1000)] public int Length { get; set; } = 10;

    public class MyClass : Entity
    {
        public string Name {get; set; }
    }
    
    private List<MyClass> _myclasses1 = new();
    private List<MyClass> _myclasses2 = new();
    private List<MyClass> _myclasses3 = new();
    private List<MyClass> _myclasses4 = new();

    
    public ItemDuplicationWithLinearSearchAndBinarySearchBenchmark()
    {
        for(int i =0; i < Length; i++)
        {
            _myclasses1.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
            _myclasses2.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
            _myclasses3.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
            _myclasses4.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
        }
        _myclasses2.Insert((Length - 1) - 3, _myclasses2[Length / 2]);
        _myclasses3.Insert(3, _myclasses3[0]);
        _myclasses4.Insert((Length - 1) / 2, _myclasses4[0]);
    }
    
    [GlobalSetup]
    public void Setup()
    {
        for(int i =0; i < Length; i++)
        {
            _myclasses1.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
            _myclasses2.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
            _myclasses3.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
            _myclasses4.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
        }
        _myclasses2.Insert((Length - 1) - 3, _myclasses2[Length / 2]);
        _myclasses3.Insert(3, _myclasses2[0]);
        _myclasses4.Insert((Length - 1) / 2, _myclasses2[0]);
    }

    [Benchmark]
    public void LinearSearchWithoutDuplication()
    {
        var a = _myclasses1.ItemDuplicationWithAlgorithmAndHashCodeWithCheckingList(x => x);
    }
    
    [Benchmark]
    public void BinarySearchWithoutDuplication()
    {
        var a = _myclasses1.ItemDuplicationWithBinarySearchAndHashCodeWithCheckingList(x => x);
    }
    
    [Benchmark]
    public void LinearSearchWithDuplicationAtFirstAndMiddlePosition()
    {
        var a = _myclasses4.ItemDuplicationWithAlgorithmAndHashCodeWithCheckingList(x => x);
    }
    
    [Benchmark]
    public void BinarySearchWithDuplicationAtFirstAndMiddlePosition()
    {
        var a = _myclasses4.ItemDuplicationWithBinarySearchAndHashCodeWithCheckingList(x => x);
    }
    
    [Benchmark]
    public void LinearSearchWithDuplicationAtFirstAndNearFirstPosition()
    {
        var a = _myclasses3.ItemDuplicationWithAlgorithmAndHashCodeWithCheckingList(x => x);
    }
    
    [Benchmark]
    public void BinarySearchWithDuplicationAtFirstAndNearFirstPosition()
    {
        var a = _myclasses3.ItemDuplicationWithBinarySearchAndHashCodeWithCheckingList(x => x);
    }
    
    [Benchmark]
    public void LinearSearchWithDuplicationAtNearMiddleAndLastPosition()
    {
        var a = _myclasses2.ItemDuplicationWithAlgorithmAndHashCodeWithCheckingList(x => x);
    }
    
    [Benchmark]
    public void BinarySearchWithDuplicationAtNearMiddleAndLastPosition()
    {
        var a = _myclasses2.ItemDuplicationWithBinarySearchAndHashCodeWithCheckingList(x => x);
    }
}