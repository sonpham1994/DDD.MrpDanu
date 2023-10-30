namespace Benchmark.JsonSerializerBenchmarks;

public readonly struct Result<T>
{
    public bool IsFailure { get; }
    public bool IsSuccess => !IsFailure;

    private readonly DomainError _error;

    public DomainError Error => _error;

    private readonly T _value;
    public T Value => IsSuccess 
        ? _value 
        : throw new InvalidOperationException("DomainError cannot null if process is fail");
            

    internal Result(in bool isFailure, in DomainError error, T value)
    {
        if (isFailure && error.IsEmpty())
            throw new InvalidOperationException("DomainError cannot null if process is fail");
        
        IsFailure = isFailure;
        _error = error;
        _value = value;
    }

    public static implicit operator Result<T>(T value)
    {
        return Result.Success(value);
    }
    
    public static implicit operator Result(in Result<T> result)
    {
        if (result.IsSuccess)
            return Result.Success();
        else
            return Result.Failure(result.Error);
    }

    public static implicit operator Result<T>(in DomainError domainError)
    {
        return new Result<T>(true, domainError, default);
    }

    public static implicit operator Result<T?>(in Result result)
    {
        return new Result<T?>(result.IsFailure, result.Error, default);
    }
}








public readonly struct ResultWithNoJsonSourceGenerator<T>
{
    public bool IsFailure { get; }
    public bool IsSuccess => !IsFailure;

    private readonly DomainError _error;

    public DomainError Error => _error;

    private readonly T _value;
    public T Value => IsSuccess 
        ? _value 
        : throw new InvalidOperationException("DomainError cannot null if process is fail");
            

    internal ResultWithNoJsonSourceGenerator(in bool isFailure, in DomainError error, T value)
    {
        if (isFailure && error.IsEmpty())
            throw new InvalidOperationException("DomainError cannot null if process is fail");
        
        IsFailure = isFailure;
        _error = error;
        _value = value;
    }

    public static implicit operator ResultWithNoJsonSourceGenerator<T>(T value)
    {
        return ResultWithNoJsonSourceGenerator.Success(value);
    }
    
    public static implicit operator ResultWithNoJsonSourceGenerator(in ResultWithNoJsonSourceGenerator<T> result)
    {
        if (result.IsSuccess)
            return ResultWithNoJsonSourceGenerator.Success();
        else
            return ResultWithNoJsonSourceGenerator.Failure(result.Error);
    }

    public static implicit operator ResultWithNoJsonSourceGenerator<T>(in DomainError domainError)
    {
        return new ResultWithNoJsonSourceGenerator<T>(true, domainError, default);
    }

    public static implicit operator ResultWithNoJsonSourceGenerator<T?>(in Result result)
    {
        return new ResultWithNoJsonSourceGenerator<T?>(result.IsFailure, result.Error, default);
    }
}
