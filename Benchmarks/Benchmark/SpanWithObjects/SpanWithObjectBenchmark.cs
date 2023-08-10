using BenchmarkDotNet.Attributes;

namespace Benchmark.SpanWithObjects;

[MemoryDiagnoser]
public class SpanWithObjectBenchmark
{
    [Params(10, 100)]
    public int Length { get; set; }

    private ClassTest[] _classTests;
    private IReadOnlyList<ClassTestForSpan> _classTestForSpans;
    
    
    [GlobalSetup]
    public void Setup()
    {
        ClassTest[] classTests = new ClassTest[Length];
        ClassTestForSpan[] classTestForSpans = new ClassTestForSpan[Length];
        for (int i = 0; i < Length; i++)
        {
            classTests[i] = new ClassTest { Id = i, Name = "Name " + i };
            classTestForSpans[i] = new ClassTestForSpan { Id = i, Name = "Name " + i };
        }

        _classTests = classTests;
        _classTestForSpans = classTestForSpans;
    }

    [Benchmark]
    public void GetClassTests()
    {
        var a = new ClassTest[2]
        {
            _classTests[8],
            _classTests[9]
        };
    }
    
    [Benchmark]
    public void GetClassTestForSpans()
    {
        var a = ((ClassTestForSpan[])_classTestForSpans).AsSpan(8, 2);
    }
    
    [Benchmark]
    public void ExistsClassTest()
    {
        var a = new ClassTest[2]
        {
            _classTests[8],
            _classTests[9]
        };
        var exist = a.Any(x => x.Id == _classTests[8].Id);
    }
    
    [Benchmark]
    public void ExistsClassTestForSpans()
    {
        var a = ((ClassTestForSpan[])_classTestForSpans).AsSpan(8, 2);
        var exist = a.IndexOf(_classTestForSpans[8]);
    }
}