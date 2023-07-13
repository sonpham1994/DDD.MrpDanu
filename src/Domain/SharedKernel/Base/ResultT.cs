using Domain.Exceptions;

namespace Domain.SharedKernel.Base;

public readonly struct Result<T> : IResult<T>
{
    public bool IsFailure { get; }
    public bool IsSuccess => !IsFailure;

    private readonly DomainError _error;

    public DomainError Error => _error;

    private readonly T _value;
    public T Value => IsSuccess 
        ? _value 
        : throw new DomainException(new DomainError($"FailSafe.{_error.Code}"
            , $"Should check failure for {_error.Code} before executing operation."));

    internal Result(bool isFailure, DomainError error, T value)
    {
        IsFailure = isFailure;
        _error = error;
        _value = value;
    }

    public static implicit operator Result<T>(T value)
    {
        return Result.Success(value);
    }
    
    public static implicit operator Result(Result<T> result)
    {
        if (result.IsSuccess)
            return Result.Success();
        else
            return Result.Failure(result.Error);
    }

    public static implicit operator Result<T>(DomainError domainError)
    {
        return new Result<T>(true, domainError, default);
    }

    public static implicit operator Result<T?>(Result result)
    {
        return new Result<T?>(result.IsFailure, result.Error, default);
    }
}
