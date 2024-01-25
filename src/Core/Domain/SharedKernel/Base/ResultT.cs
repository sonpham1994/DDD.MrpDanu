using Domain.Exceptions;

namespace Domain.SharedKernel.Base;

public readonly struct Result<T> : IResult<T>
{
    private readonly DomainError _error;
    private readonly bool _isSuccess;
    private readonly T _value;

    public bool IsFailure => !IsSuccess;
    
    public bool IsSuccess
    {
        get
        {
            this.CheckSafeFailResult(_isSuccess, _error);
            return _isSuccess;
        }
    }

    public DomainError Error
    {
        get
        {
            this.CheckSafeFailResult(_isSuccess, _error);
            return _error;
        }
    }

    public T Value
    {
        get
        {
            if (!_isSuccess)
                throw new DomainException(DomainErrors.SafeFailWithCode(_error.Code));

            return _value;
        }
    }

    internal Result(in bool isSuccess, in DomainError error, T value)
    {
        _isSuccess = isSuccess;
        _error = error;
        _value = value;
        
        this.CheckSafeFailResult(_isSuccess, _error);
    }

    public static implicit operator Result<T>(T value)
    {
        return Result.Success(value);
    }
    
    public static implicit operator Result(in Result<T> result)
    {
        if (result._isSuccess)
            return Result.Success();
        else
            return Result.Failure(result.Error);
    }

    public static implicit operator Result<T>(in DomainError domainError)
    {
        return new Result<T>(false, domainError, default);
    }

    public static implicit operator Result<T?>(in Result result)
    {
        return new Result<T?>(result.IsFailure, result.Error, default);
    }
}
