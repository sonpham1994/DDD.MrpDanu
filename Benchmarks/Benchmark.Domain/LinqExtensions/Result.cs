namespace Benchmark.Domain.LinqExtensions;

//https://github.com/vkhorikov/CSharpFunctionalExtensions
public readonly struct Result : IResult
{
    public bool IsFailure { get; }
    public bool IsSuccess => !IsFailure;

    private readonly DomainError _error;
    public DomainError Error => _error;

    private Result(bool isFailure, DomainError error)
    {
        IsFailure = isFailure;
        _error = error;
    }

    public static Result Success()
    {
        return new Result(false, default);
    }

    public static Result Failure(DomainError error)
    {
        return new Result(true, error);
    }

    public static implicit operator Result(DomainError domainError)
    {
        return Failure(domainError);
    }
}
