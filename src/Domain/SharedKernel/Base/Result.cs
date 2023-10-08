using Domain.Exceptions;

namespace Domain.SharedKernel.Base;

//https://github.com/vkhorikov/CSharpFunctionalExtensions
public readonly struct Result : IResult
{
    public bool IsFailure { get; }
    public bool IsSuccess => !IsFailure;

    private readonly DomainError _error;
    public DomainError Error => _error;

    private Result(in bool isFailure, in DomainError error)
    {
        if (isFailure && error.IsEmpty())
            throw new DomainException(new DomainError("SafeFail", "DomainError cannot null if process is fail"));

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