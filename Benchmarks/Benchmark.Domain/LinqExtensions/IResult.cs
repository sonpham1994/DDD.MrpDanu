namespace Benchmark.Domain.LinqExtensions;

public interface IResult
{
    bool IsFailure { get; }
    bool IsSuccess { get; }
}