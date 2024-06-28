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
    public void FirstOrDefault()
    {
        var test = _data.FirstOrDefault(x => x == _dataFind);
    }

    [Benchmark]
    public void Find()
    {
        var test = _data.Find(x => x == _dataFind);
    }

    [Benchmark]
    public void Index()
    {
        var data = _data[Length - 1];
        var test = _data[Length - 1];
    }

    [Benchmark]
    public void FindOrFirstOrDefaultWithPredicate()
    {
        var test = FindOrFirstDefaultWithPredicate(_data, x => x == _dataFind);
    }

    [Benchmark]
    public void FindOrFirstOrDefaultWithWithFunc()
    {
        var test = FindOrFirstDefaultWithFunc(_data, x => x == _dataFind);
    }

    private static T? FindOrFirstDefaultWithPredicate<T>(IReadOnlyList<T> input, Predicate<T> func)
    {
        if (input is List<T> list)
        {
            return list.Find(func);
        }
        else
        {
            Func<T, bool> predicate = new Func<T, bool>(func);
            return input.FirstOrDefault(predicate);
        }
    }

    private static T? FindOrFirstDefaultWithFunc<T>(IReadOnlyList<T> input, Func<T, bool> func)
    {
        if (input is List<T> list)
        {
            Predicate<T> predicate = new Predicate<T>(func);
            return list.Find(predicate);
        }
        else
        {
            return input.FirstOrDefault(func);
        }
    }
}
