using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.FirstOrDefaultVsFind;

[MemoryDiagnoser]
public class FirstOrDefaultVsFindBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }

    private List<int> _data;

    [GlobalSetup]
    public void Setup()
    {
        _data = new List<int>(Length);
        var random = new Random();
        for (int i = 0; i < Length; i++)
        {
            _data.Add(random.Next());
        }
    }

    [Benchmark]
    public void FirstOrDefault()
    {
        var data = _data[Length - 1];
        var test = _data.FirstOrDefault(x => x == data);
    }

    [Benchmark]
    public void Find()
    {
        var data = _data[Length - 1];
        var test = _data.Find(x => x == data);
    }

    [Benchmark]
    public void Index()
    {
        var data = _data[Length - 1];
        var test = _data[Length - 1];
    }
}
