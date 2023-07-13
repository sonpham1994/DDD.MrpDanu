using BenchmarkDotNet.Attributes;

namespace Benchmark.Domain.LinqExtensions;

[MemoryDiagnoser()]
public class AnyFailureBenchmark
{
    [Params(10, 100, 1000)]
    public static int Length { get; set; }


    public static readonly IEnumerable<int> Ints = Enumerable.Range(0, Length).ToList();
    
    
    [Benchmark]
    public void AnyFailure()
    {
        var array = new List<int> { 4, -1 };
        var isFail = array.AnyFailure(ExistElement);
    }
    
    [Benchmark]
    public void AnyFailureInvoke()
    {
        var array = new List<int> { 4, -1 };
        var isFail = array.AnyFailureInvoke(ExistElement);
    }
    
    private IResult ExistElement(int id)
    {
        var exist = Ints.Any(x => x == id);
        if (!exist)
            return Result.Failure(new DomainError("Fail", "Fail Message"));

        return Result.Success();
    }
}