using SharedKernel.Exceptions;
using SharedKernel.Extensions;

namespace SharedKernel;

public readonly struct Result<T> : IResult<T>
{
    private readonly DomainError _error;
    private readonly bool _isSuccess;
    private readonly T _value;
    
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
                throw new SafeFailWithCodeResultDomainException(_error.Code);

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
        => Result.Success(value);
    
    public static implicit operator Result(in Result<T> result)
    {
        if (result._isSuccess)
            return Result.Success();
        else
            return Result.Failure(result.Error);
    }

    public static implicit operator Result<T>(in DomainError domainError)
        => Failure(domainError);

    public static implicit operator Result<T?>(in Result result)
        => new(result.IsSuccess, result.Error, default);

    public static Result<T> Failure(in DomainError domainError) 
        => new(false, domainError, default!);
}
