using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StructWithIEquatable;

[MemoryDiagnoser]
public class StructWithIEquatableAndClassBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }
    private List<ErrorStruct> _errorStructs = new();
    private List<ErrorStructWithIEquatable> _errorStructWithIEquatables = new();
    private List<ErrorClass> _errorClasses = new();
    
    private HashSet<ErrorStruct> _errorStructsHashSet = new();
    private HashSet<ErrorStructWithIEquatable> _errorStructWithIEquatablesHashSet  = new();
    private HashSet<ErrorClass> _errorClassesHashSet  = new();
    
    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < Length; i++)
        {
            _errorStructs.Add(new ErrorStruct($"Code {i}", $"Message {i}"));
            _errorStructWithIEquatables.Add(new ErrorStructWithIEquatable($"Code {i}", $"Message {i}"));
            _errorClasses.Add(new ErrorClass($"Code {i}", $"Message {i}"));
            
            _errorStructsHashSet.Add(new ErrorStruct($"Code {i}", $"Message {i}"));
            _errorStructWithIEquatablesHashSet.Add(new ErrorStructWithIEquatable($"Code {i}", $"Message {i}"));
            _errorClassesHashSet.Add(new ErrorClass($"Code {i}", $"Message {i}"));
        }
    }

    [Benchmark]
    public void CreateErrorStruct()
    {
        var a = new ErrorStruct("Code1", "Message1");
    }
    
    [Benchmark]
    public void CreateErrorStructWithIEquatable()
    {
        var a = new ErrorStructWithIEquatable("Code1", "Message1");
    }
    
    [Benchmark]
    public void CreateErrorClass()
    {
        var a = new ErrorClass("Code1", "Message1");
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
    public void CompareErrorClass()
    {
        var a = new ErrorClass("Code1", "Message1");
        var b = new ErrorClass("Code1", "Message1");
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
    public void GroupErrorClass()
    {
        var a = _errorClasses.GroupBy(x => x).Select(x=>x.FirstOrDefault()).ToList();
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

    [Benchmark]
    public void GroupErrorClassWithDuplication()
    {
        var t = _errorClasses.ToList();
        t.Insert(Length / 2, new ErrorClass("Code 2", "Message 2"));
        var a = t.GroupBy(x => x).Select(x => x.FirstOrDefault()).ToList();
    }
    
    [Benchmark]
    public void HashSetErrorStruct()
    {
        var b = _errorStructsHashSet.ToHashSet();
        var a = b.Add(new ErrorStruct("CodeAbc", "MessageAbc"));
    }
    
    [Benchmark]
    public void HashSetErrorStructWithIEquatable()
    {
        var b = _errorStructWithIEquatablesHashSet.ToHashSet();
        var a = b.Add(new ErrorStructWithIEquatable("CodeAbc", "MessageAbc"));
    }
    
    [Benchmark]
    public void HashSetErrorClass()
    {
        var b = _errorClassesHashSet.ToHashSet();
        var a = b.Add(new ErrorClass("CodeAbc", "MessageAbc"));
    }
    
    [Benchmark]
    public void HashSetErrorStructWithDuplication()
    {
        var b = _errorStructsHashSet.ToHashSet();
        var a = b.Add(new ErrorStruct($"Code {Length / 2}", $"Message {Length / 2}"));
    }
    
    [Benchmark]
    public void HashSetErrorStructWithIEquatableWithDuplication()
    {
        var b = _errorStructWithIEquatablesHashSet.ToHashSet();
        var a = b.Add(new ErrorStructWithIEquatable($"Code {Length / 2}", $"Message {Length / 2}"));
    }
    
    [Benchmark]
    public void HashSetErrorClassWithDuplication()
    {
        var b = _errorClassesHashSet.ToHashSet();
        var a = b.Add(new ErrorClass("CodeAbc", "MessageAbc"));
    }

    [Benchmark]
    public void CreateListWithErrorStruct()
    {
        List<ErrorStruct> t = new(Length);
        for (int i = 0; i < Length; i++)
        {
            t.Add(new ErrorStruct($"Code {i}", $"Message {i}"));
        }
    }
    
    [Benchmark]
    public void CreateListWithErrorStructIEquatable()
    {
        List<ErrorStructWithIEquatable> t = new(Length);
        for (int i = 0; i < Length; i++)
        {
            t.Add(new ErrorStructWithIEquatable($"Code {i}", $"Message {i}"));
        }
    }
    
    [Benchmark]
    public void CreateListWithErrorClass()
    {
        List<ErrorClass> t = new(Length);
        for (int i = 0; i < Length; i++)
        {
            t.Add(new ErrorClass($"Code {i}", $"Message {i}"));
        }
    }
}
