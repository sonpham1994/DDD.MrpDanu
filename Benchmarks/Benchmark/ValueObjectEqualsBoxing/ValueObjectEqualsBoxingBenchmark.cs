using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.ValueObjectEqualsBoxing;

[MemoryDiagnoser]
public class ValueObjectEqualsBoxingBenchmark
{
    private MyBoxingValueObject _boxing1;
    private MyBoxingValueObject _boxing2;

    private MyObjectWithNoValueBoxingValueObject _objectWithValueNoboxing1;
    private MyObjectWithNoValueBoxingValueObject _objectWithValueNoboxing2;

    private MyObjectAvoidBoxingValueObject _objectAvoidBoxing1;
    private MyObjectAvoidBoxingValueObject _objectAvoidBoxing2;

    [GlobalSetup]
    public void Setup()
    {
        var guid = Guid.NewGuid();
        _boxing1 = new() { Id = guid, Id2 = guid, Id3 = Guid.NewGuid() };
        _boxing2 = new() { Id = guid, Id2 = guid, Id3 = Guid.NewGuid() };

        _objectWithValueNoboxing1 = new() { Id = guid.ToString(), Id2 = guid.ToString(), Id3 = Guid.NewGuid().ToString() };
        _objectWithValueNoboxing2 = new() { Id = guid.ToString(), Id2 = guid.ToString(), Id3 = Guid.NewGuid().ToString() };

        _objectAvoidBoxing1 = new() { Id = guid, Id2 = guid, Id3 = Guid.NewGuid() };
        _objectAvoidBoxing2 = new() { Id = guid, Id2 = guid, Id3 = Guid.NewGuid() };
    }

    [Benchmark]
    public void ValueObjectEqualsBoxing()
    {
        var a = _boxing1 == _boxing2;
    }

    [Benchmark]
    public void ValueObjectEqualsWithNoValueBoxing()
    {
        var a = _objectWithValueNoboxing1 == _objectWithValueNoboxing2;
    }

    [Benchmark]
    public void ValueObjectEqualsWithAvoidBoxing()
    {
        var a = _objectAvoidBoxing1 == _objectAvoidBoxing2;
    }
}
