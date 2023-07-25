using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StructWithIEquatable;

[MemoryDiagnoser]
public class StructWithIEquatableBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }
    private List<ErrorStruct> _errorStructs = new();
    private List<ErrorStructWithIEquatable> _errorStructWithIEquatables = new();
    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < Length; i++)
        {
            _errorStructs.Add(new ErrorStruct($"Code {i}", $"Message {i}"));
            _errorStructWithIEquatables.Add(new ErrorStructWithIEquatable($"Code {i}", $"Message {i}"));
        }
    }

    [Benchmark]
    public void CreateErrorStruct()
    {
        var a = new ErrorStruct("Code1", "Message1");
    }

    [Benchmark]
    public void ErrorStructWithIEquatable()
    {
        var a = new ErrorStructWithIEquatable("Code1", "Message1");
    }

    [Benchmark]
    public void CompareErrorStruct()
    {
        var a = new ErrorStruct("Code1", "Message1");
        var b = new ErrorStruct("Code1", "Message1");
        var c = a == b;
    }

    [Benchmark]
    public void CompareWithIEquatable()
    {
        var a = new ErrorStructWithIEquatable("Code1", "Message1");
        var b = new ErrorStructWithIEquatable("Code1", "Message1");
        var c = a == b;
    }

    [Benchmark]
    public void GroupErrorStruct()
    {
        var a = _errorStructs.GroupBy(x => x).Select(x=>x.FirstOrDefault()).ToList();
    }

    [Benchmark]
    public void GroupErrorStructWithIEquatable()
    {
        var a = _errorStructWithIEquatables.GroupBy(x => x).Select(x => x.FirstOrDefault()).ToList();
    }

    [Benchmark]
    public void GroupErrorStructWithDuplication()
    {
        var t = _errorStructs.ToList();
        t.Insert(Length / 2, new ErrorStruct("Code 2", "Message 2"));
        var a = t.GroupBy(x => x).Select(x => x.FirstOrDefault()).ToList();
    }

    [Benchmark]
    public void GroupErrorStructWithIEquatableWithDuplication()
    {
        var t = _errorStructWithIEquatables.ToList();
        t.Insert(Length / 2, new ErrorStructWithIEquatable("Code 2", "Message 2"));
        var a = t.GroupBy(x => x).Select(x => x.FirstOrDefault()).ToList();
    }
}
