using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Domain.LinqExtensions.DistinctBenchmarks;

[MemoryDiagnoser()]
public class DistinctBenchmark
{
    [Params(10, 100, 1000)] 
    public int Length { get; set; }
    public class MyClass : Entity
    {
        public string Name {get; set; }
    }
    
    private List<Guid> _guids1 = new();
    private List<Guid> _guids2 = new();
    
    private List<MyClass> _myclasses1 = new();
    private List<MyClass> _myclasses2 = new();

    [GlobalSetup]
    public void Setup()
    {
        for(int i =0; i < Length; i++)
        {
            _guids1.Add(Guid.NewGuid());
            _guids2.Add(Guid.NewGuid());
            _myclasses1.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
            _myclasses2.Add(new MyClass(){Id = Guid.NewGuid(), Name = "Name " + i});
        }
        _guids2.Insert(Length - 1, _guids2[Length / 2]);
        _myclasses2.Insert(Length - 1, _myclasses2[Length / 2]);
    }

    // [Benchmark]
    // public void DistinctWithoutDuplication()
    // {
    //     var a = _guids1.Distinct().ToList();
    // }
    //
    // [Benchmark]
    // public void CheckingDuplicationBeforeExecuteWithoutDuplication()
    // {
    //     if (_guids1.AnyDuplicationWithAlgorithmAndHashCode(x=>x))
    //     {
    //         var a = _guids1.Distinct().ToList();
    //     }
    // }
    //
    // [Benchmark]
    // public void DistinctWithDuplication()
    // {
    //     var a = _guids2.Distinct().ToList();
    // }
    //
    // [Benchmark]
    // public void CheckingDuplicationBeforeExecuteWithDuplication()
    // {
    //     if (_guids2.AnyDuplicationWithAlgorithmAndHashCode(x => x))
    //     {
    //         var a = _guids2.Distinct().ToList();
    //     }
    // }
    
    //
    // [Benchmark]
    // public void EntityDistinctWithoutDuplication()
    // {
    //     var a = _myclasses1.Distinct().ToList();
    // }
    //
    // [Benchmark]
    // public void EntityCheckingDuplicationBeforeExecuteWithoutDuplication()
    // {
    //     if (_myclasses1.AnyDuplicationWithAlgorithmAndHashCode(x=>x))
    //     {
    //         var a = _myclasses1.Distinct().ToList();
    //     }
    // }
    //
    // [Benchmark]
    // public void EntityDistinctWithDuplication()
    // {
    //     var a = _myclasses2.Distinct().ToList();
    // }
    //
    // [Benchmark]
    // public void EntityCheckingDuplicationBeforeExecuteWithDuplication()
    // {
    //     if (_myclasses2.AnyDuplicationWithAlgorithmAndHashCode(x => x))
    //     {
    //         var a = _myclasses2.Distinct().ToList();
    //     }
    // }
    
    
    [Benchmark]
    public void EntityCheckingDuplicationWithCheckingTypeBeforeExecuteWithoutDuplication()
    {
        if (_myclasses1.AnyDuplicationCheckingTypeWithAlgorithmAndHashCode(x=>x))
        {
            var a = _myclasses1.Distinct().ToList();
        }
    }
    
    [Benchmark]
    public void EntityCheckingDuplicationWithCheckingTypeBeforeExecuteWithDuplication()
    {
        if (_myclasses2.AnyDuplicationCheckingTypeWithAlgorithmAndHashCode(x => x))
        {
            var a = _myclasses2.Distinct().ToList();
        }
    }
}
