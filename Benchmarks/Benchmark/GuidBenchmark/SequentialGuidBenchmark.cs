using BenchmarkDotNet.Attributes;

namespace Benchmark.GuidBenchmark;

[MemoryDiagnoser]
public class SequentialGuidBenchmark
{
    [Benchmark]
    public void CompareSequentialGuidWithCustomMethod()
    {
        var sequentialGuid1 = new Guid("bd8bab48-8778-4b7e-20fa-08dbe1be19f5");
        var sequentialGuid2 = new Guid("b8853244-d2fc-4918-20fb-08dbe1be19f5");

        var result = SequentialGuid.CompareTo(sequentialGuid1, sequentialGuid2);
    }

    [Benchmark]
    public void CompareSequentialGuidWithSqlGuidMethod()
    {
        var sequentialGuid1 = new Guid("bd8bab48-8778-4b7e-20fa-08dbe1be19f5");
        var sequentialGuid2 = new Guid("b8853244-d2fc-4918-20fb-08dbe1be19f5");

        var result = SequentialGuid.CompareToUsingSqlGuid(sequentialGuid1, sequentialGuid2);
    }
}
