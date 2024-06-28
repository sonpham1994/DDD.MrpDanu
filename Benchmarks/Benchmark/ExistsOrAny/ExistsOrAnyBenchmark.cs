using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.ExistsOrAny;

[MemoryDiagnoser]
public class ExistsOrAnyBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }

    private List<int> _data;
    private int _dataFind;

    [GlobalSetup]
    public void Setup()
    {
        _data = new List<int>(Length);
        var random = new Random();
        for (int i = 0; i < Length; i++)
        {
            _data.Add(random.Next());
        }

        _dataFind = _data[Length - 1];
    }

    [Benchmark]
    public void Exists()
    {
        var test = _data.Exists(x => x == _dataFind);
    }

    [Benchmark]
    public void Any()
    {
        var test = _data.Any(x => x == _dataFind);
    }

    [Benchmark]
    public void ExistOrAnyWithPredicate()
    {
        var test = ExistOrAnyWithPredicate(_data, x => x == _dataFind);
    }

    [Benchmark]
    public void ExistOrAnWithFunc()
    {
        var test = ExistOrAnyWithFunc(_data, x => x == _dataFind);
    }

    private static bool ExistOrAnyWithPredicate<T>(IReadOnlyList<T> input, Predicate<T> func)
    {
        if (input is List<T> list)
        {
            return list.Exists(func);
        }
        else
        {
            Func<T, bool> predicate = new Func<T, bool>(func);
            return input.Any(predicate);
        }
    }

    private static bool ExistOrAnyWithFunc<T>(IReadOnlyList<T> input, Func<T, bool> func)
    {
        if (input is List<T> list)
        {
            Predicate<T> predicate = new Predicate<T>(func);
            return list.Exists(predicate);
        }
        else
        {
            return input.Any(func);
        }
    }
}
