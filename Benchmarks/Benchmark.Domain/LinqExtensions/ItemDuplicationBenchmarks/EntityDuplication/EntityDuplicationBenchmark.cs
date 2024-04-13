using BenchmarkDotNet.Attributes;

namespace Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks.EntityDuplication;

[MemoryDiagnoser()]
public class EntityDuplicationBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }

    private List<Material> _middleDup;
    private List<Material> _lastDup;
    [GlobalSetup]
    public void Setup()
    {

        _middleDup = new();
        _lastDup = new();
        for (int i = 0; i < Length; i++)
        {
            _middleDup.Add(new Material
            {
                Id = new MaterialId(Guid.NewGuid()),
            });
            
            _lastDup.Add(new Material
            {
                Id = new MaterialId(Guid.NewGuid()),
            });
        }

        _lastDup.Add(_middleDup.FirstOrDefault());
        _middleDup.Insert(Length / 2, _middleDup.FirstOrDefault());
    }

    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithGroup()
    {
        var t = _lastDup.ItemDuplicationWithGroup(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithForeachDictionary()
    {
        var t = _lastDup.ItemDuplicationWithForeachDictionary(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithForeachHashSet()
    {
        var t = _lastDup.ItemDuplicationWithForeachHashSet(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithForeachAny()
    {
        var t = _lastDup.ItemDuplicationWithForeachAny(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithForAny()
    {
        var t = _lastDup.ItemDuplicationWithForAny(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithEntityDup()
    {
        var t = _lastDup.GetEntityDuplication();
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithGroup()
    {
        var t = _middleDup.ItemDuplicationWithGroup(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithForeachDictionary()
    {
        var t = _middleDup.ItemDuplicationWithForeachDictionary(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithForeachHashSet()
    {
        var t = _middleDup.ItemDuplicationWithForeachHashSet(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithForeachAny()
    {
        var t = _middleDup.ItemDuplicationWithForeachAny(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithForAny()
    {
        var t = _middleDup.ItemDuplicationWithForAny(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithEntityDup()
    {
        var t = _middleDup.GetEntityDuplication();
    }
}