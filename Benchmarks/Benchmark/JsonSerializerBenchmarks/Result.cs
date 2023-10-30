using System.Text.Json.Serialization;

namespace Benchmark.JsonSerializerBenchmarks;

public readonly struct Result
{
    public bool IsFailure { get; }
    public bool IsSuccess => !IsFailure;

    private readonly DomainError _error;
    public DomainError Error => _error;

    private Result(in bool isFailure, in DomainError error)
    {
        if (isFailure && error.IsEmpty())
            throw new InvalidOperationException("DomainError cannot null if process is fail");

        IsFailure = isFailure;
        _error = error;
    }

    public static Result Success()
    {
        return new Result(false, DomainError.Empty);
    }

    public static Result<T> Success<T>(T? value)
    {
        return new Result<T>(false, DomainError.Empty, value);
    }

    public static Result Failure(in DomainError error)
    {
        return new Result(true, error);
    }

    public static implicit operator Result(in DomainError domainError)
    {
        return Failure(domainError);
    }


}






public readonly struct ResultWithNoJsonSourceGenerator
{
    public bool IsFailure { get; }
    public bool IsSuccess => !IsFailure;

    private readonly DomainError _error;
    public DomainError Error => _error;

    private ResultWithNoJsonSourceGenerator(in bool isFailure, in DomainError error)
    {
        if (isFailure && error.IsEmpty())
            throw new InvalidOperationException("DomainError cannot null if process is fail");

        IsFailure = isFailure;
        _error = error;
    }

    public static ResultWithNoJsonSourceGenerator Success()
    {
        return new ResultWithNoJsonSourceGenerator(false, DomainError.Empty);
    }


    public static ResultWithNoJsonSourceGenerator<T> Success<T>(T? value)
    {
        return new ResultWithNoJsonSourceGenerator<T>(false, DomainError.Empty, value);
    }

    public static ResultWithNoJsonSourceGenerator Failure(in DomainError error)
    {
        return new ResultWithNoJsonSourceGenerator(true, error);
    }

    public static implicit operator ResultWithNoJsonSourceGenerator(in DomainError domainError)
    {
        return Failure(domainError);
    }


}