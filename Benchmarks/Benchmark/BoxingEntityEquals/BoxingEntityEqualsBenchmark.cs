using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.BoxingEntityEquals;

[MemoryDiagnoser]
public class BoxingEntityEqualsBenchmark
{
    private MyClassBoxingEqualsAndCompare _myClassBoxingEqualsAndCompare1;
    private MyClassBoxingEqualsAndCompare _myClassBoxingEqualsAndCompare2;
    private MyClassAvoidBoxingEqualsAndCompare _myClassAvoidBoxingEqualsAndCompare1;
    private MyClassAvoidBoxingEqualsAndCompare _myClassAvoidBoxingEqualsAndCompare2;
    private List<MyClassBoxingEqualsAndCompare> _boxingCompares = new();
    private List<MyClassAvoidBoxingEqualsAndCompare> _avoidBoxingCompares = new();
    [GlobalSetup]
    public void Setup()
    {
        _myClassBoxingEqualsAndCompare1 = new MyClassBoxingEqualsAndCompare()
        {
            Id = Guid.NewGuid()
        };
        _myClassBoxingEqualsAndCompare2 = new MyClassBoxingEqualsAndCompare()
        {
            Id = Guid.NewGuid()
        };

        _myClassAvoidBoxingEqualsAndCompare1 = new MyClassAvoidBoxingEqualsAndCompare()
        {
            Id = Guid.NewGuid()
        };
        _myClassAvoidBoxingEqualsAndCompare2 = new MyClassAvoidBoxingEqualsAndCompare()
        {
            Id = Guid.NewGuid()
        };

        _boxingCompares.Add(_myClassBoxingEqualsAndCompare1);
        _boxingCompares.Add(_myClassBoxingEqualsAndCompare2);

        _avoidBoxingCompares.Add(_myClassAvoidBoxingEqualsAndCompare1);
        _avoidBoxingCompares.Add(_myClassAvoidBoxingEqualsAndCompare2);
    }

    [Benchmark]
    public void BoxingEquals()
    {
        var result = _myClassBoxingEqualsAndCompare1 == _myClassBoxingEqualsAndCompare2;
    }

    [Benchmark]
    public void AvoidBoxingEquals()
    {
        var result = _myClassAvoidBoxingEqualsAndCompare1 == _myClassAvoidBoxingEqualsAndCompare2;
    }

    [Benchmark]
    public void BoxingCompare()
    {
        _boxingCompares.Sort();
    }

    [Benchmark]
    public void AvoidBoxingCompare()
    {
        _avoidBoxingCompares.Sort();
    }
}
