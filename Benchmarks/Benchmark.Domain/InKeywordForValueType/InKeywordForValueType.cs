using BenchmarkDotNet.Attributes;

namespace Benchmark.Domain.InKeywordForValueType;

[MemoryDiagnoser()]
public class InKeywordForValueType
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }

    [Benchmark]
    public void PassDataWithInKeyword()
    {
        var error = new DomainError("ErrorCode", "ErrorMessage");
        for (int i = 0; i < Length; i++)
        {
            WithInKeyword(in error);
        }
    }
    
    [Benchmark]
    public void PassDataWithoutInKeyword()
    {
        var error = new DomainError("ErrorCode", "ErrorMessage");
        for (int i = 0; i < Length; i++)
        {
            WithoutInKeyword(error);
        }
    }
    
    private void WithInKeyword(in DomainError error)
    {
        WithInKeywordSecond(in error);
    }
    
    private void WithInKeywordSecond(in DomainError error)
    {
        WithInKeywordThird(in error);
    }
    
    private void WithInKeywordThird(in DomainError error)
    {
    }
    
    private void WithoutInKeyword(DomainError error)
    {
        WithoutInKeywordSecond(error);
    }
    
    private void WithoutInKeywordSecond(DomainError error)
    {
        WithoutInKeywordThird(error);
    }
    
    private void WithoutInKeywordThird(DomainError error)
    {
    }
}