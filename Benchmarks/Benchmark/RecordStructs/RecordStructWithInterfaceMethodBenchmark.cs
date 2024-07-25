using BenchmarkDotNet.Attributes;

namespace Benchmark.RecordStructs;

[MemoryDiagnoser]
public class RecordStructWithInterfaceMethodBenchmark
{
    [Benchmark]
    public void AssignGuidUsingInterfaceExt()
    {
        var a = new UserId();
        a.AssignGuidUsingInterfaceExt();
    }
    
    [Benchmark]
    public void AssignGuidUsingStructExt()
    {
        var a = new UserId();
        a.AssignGuidUsingStructExt();
    }
    
    [Benchmark]
    public void AssignGuidUsingInterfaceExtAndComparision()
    {
        var a = new UserId();
        a.AssignGuidUsingInterfaceExt();
        var b = new UserId();
        b.AssignGuidUsingInterfaceExt();
        var _ = a == b;
    }
    
    [Benchmark]
    public void AssignGuidUsingStructExtAndComparision()
    {
        var a = new UserId();
        a.AssignGuidUsingStructExt();
        var b = new UserId();
        b.AssignGuidUsingStructExt();
        var _ = a == b;
    }
    
    [Benchmark]
    public void NotAssignGuid()
    {
        var a = new UserId();
        a.NotAssignGuidExt();
    }
}

