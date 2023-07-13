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

    private List<Guid> _guids = new();

    [GlobalSetup]
    public void Setup()
    {
        for(int i =0; i < Length; i++)
        {
            _guids.Add(Guid.NewGuid());
        }
        
    }

    [Benchmark]
    public void DistinctWithoutDupplication()
    {
        var a = _guids.Distinct().ToList();
    }

    [Benchmark]
    public void CheckingDuplicationBeforeExecuteWithoutDuplication()
    {
        if (_guids.AnyDuplicationWithAlgorithmAndHashCode(x=>x))
        {
            var a = _guids.Distinct().ToList();
        }
    }

    [Benchmark]
    public void DistinctWithDupplication()
    {
        _guids.Insert(Length - 1, _guids[Length / 2]);
        var a = _guids.Distinct().ToList();
    }

    [Benchmark]
    public void CheckingDuplicationBeforeExecuteWithDuplication()
    {
        _guids.Insert(Length - 1, _guids[Length / 2]);
        if (_guids.AnyDuplicationWithAlgorithmAndHashCode(x => x))
        {
            var a = _guids.Distinct().ToList();
        }
    }
}
