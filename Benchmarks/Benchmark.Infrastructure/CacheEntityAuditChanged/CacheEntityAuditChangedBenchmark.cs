using BenchmarkDotNet.Attributes;

namespace Benchmark.Infrastructure.CacheEntityAuditChanged;

[MemoryDiagnoser]
public class CacheEntityAuditChangedBenchmark
{
    [Params(1, 10, 100)]
    public int Length { get; set; }
    private AppDbContextCached _dbContext;
    [GlobalSetup]
    public void Setup()
    {
        _dbContext = new(); // Instantiate DbContext here
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test1());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
        _dbContext.Add(new Test2());
    }
    
    // [IterationSetup]
    // public void IterationSetup()
    // {
    //     _dbContext = new(); // Instantiate DbContext here
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test1());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    //     _dbContext.Add(new Test2());
    // }
    //
    // [IterationCleanup]
    // public void IterationCleanup()
    // {
    //     _dbContext.Dispose(); // Ensure DbContext is disposed after each iteration
    // }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _dbContext.Dispose();
    }
    
    [Benchmark]
    public void CachedGetEntityAuditChanged()
    {
        for (int i = 0; i < Length; i++)
        {
            _dbContext.GetEntityAuditsChangedCahced();
        }
    }
    
    [Benchmark]
    public void WithoutCachedGetEntityAuditChanged()
    {
        for (int i = 0; i < Length; i++)
        {
            _dbContext.GetEntityAuditsChangedWithoutCached();
        }
    }
}