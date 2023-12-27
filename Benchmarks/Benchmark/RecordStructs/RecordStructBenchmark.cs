using BenchmarkDotNet.Attributes;

namespace Benchmark.RecordStructs;

[MemoryDiagnoser]
public class RecordStructBenchmark
{
    [Params(1, 10, 100)]
    public int Length { get; set; }

    [Benchmark]
    public void CompareEqualsRecordStruct()
    {
        var a = new RecordStruct(Guid.NewGuid());
        var b = new RecordStruct(Guid.NewGuid());
        for (int i = 0; i < Length; i++)
        {
            var c = a.Equals(b);
        }
        
    }
    
    [Benchmark]
    public void CompareEqualsEqualsRecordStruct()
    {
        var a = new RecordStruct(Guid.NewGuid());
        var b = new RecordStruct(Guid.NewGuid());
        for (int i = 0; i < Length; i++)
        {
            var c = a == b;
        }
        
    }
}