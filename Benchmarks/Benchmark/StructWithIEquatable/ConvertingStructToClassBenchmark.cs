using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StructWithIEquatable;

[MemoryDiagnoser]
public class ConvertingStructToClassBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }
    private List<ErrorStruct> _errorStructs = new();
    private List<ErrorStructWithIEquatable> _errorStructWithIEquatables = new();
    private List<ErrorClass> _errorClasses = new();

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < Length; i++)
        {
            _errorStructs.Add(new ErrorStruct($"Code {i}", $"Message {i}"));
            _errorStructWithIEquatables.Add(new ErrorStructWithIEquatable($"Code {i}", $"Message {i}"));
            _errorClasses.Add(new ErrorClass($"Code {i}", $"Message {i}"));
        }
    }

    [Benchmark]
    public void ConvertErrorStructToErrorStructWithIEquatable()
    {
        var a = _errorStructs.Select(x => new ErrorStructWithIEquatable(x.Code, x.Message)).ToList();
    }

    [Benchmark]
    public void ConvertErrorStructToErrorClass()
    {
        var a = _errorStructs.Select(x => new ErrorClass(x.Code, x.Message)).ToList();
    }

    [Benchmark]
    public void ConvertErrorStructWithIEquatableToErrorClass()
    {
        var a = _errorStructWithIEquatables.Select(x => new ErrorClass(x.Code, x.Message)).ToList();
    }

    [Benchmark]
    public void ConvertErrorClassToErrorStruct()
    {
        var a = _errorClasses.Select(x => new ErrorStruct(x.Code, x.Message)).ToList();
    }

    [Benchmark]
    public void ConvertErrorClassToErrorClass()
    {
        var a = _errorClasses.Select(x => new ErrorClass(x.Code, x.Message)).ToList();
    }
}
