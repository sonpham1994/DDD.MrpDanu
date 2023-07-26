using Domain.Exceptions;

namespace Domain.SharedKernel.Base;

//https://github.com/vkhorikov/CSharpFunctionalExtensions
public readonly struct Result : IResult
{
    public bool IsFailure { get; }
    public bool IsSuccess => !IsFailure;

    private readonly DomainError _error = DomainError.Empty;
    public DomainError Error => _error;

    private Result(bool isFailure, DomainError error)
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

    public static Result Failure(DomainError error)
    {
        return new Result(true, error);
    }

    public static implicit operator Result(DomainError domainError)
    {
        return Failure(domainError);
    }
    
    public static Result<(T1, T2)> Combine<T1, T2>(Result<T1> result1, 
        Result<T2> result2)
    {
        if (result1.IsFailure)
            return result1.Error;

        if (result2.IsFailure)
            return result2.Error;

        return Success((result1.Value, result2.Value));
    }
    
    public static Result<(T1, T2, T3)> Combine<T1, T2, T3>(Result<T1> result1, 
        Result<T2> result2,
        Result<T3> result3)
    {
        var result = Combine(result1, result2);
        if (result.IsFailure)
            return result.Error;

        if (result3.IsFailure)
            return result3.Error;

        return Success((result1.Value, result2.Value, result3.Value));
    }
    
    public static Result<(T1, T2, T3, T4)> Combine<T1, T2, T3, T4>(Result<T1> result1, 
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4)
    {
        var result = Combine(result1, result2, result3);
        if (result.IsFailure)
            return result.Error;

        if (result4.IsFailure)
            return result4.Error;

        return Success((result1.Value, result2.Value, result3.Value, result4.Value));
    }
    
    public static Result<(T1, T2, T3, T4, T5)> Combine<T1, T2, T3, T4, T5>(Result<T1> result1, 
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4,
        Result<T5> result5)
    {
        var result = Combine(result1, result2, result3, result4);
        if (result.IsFailure)
            return result.Error;

        if (result5.IsFailure)
            return result5.Error;

        return Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value));
    }
    
    public static Result<(T1, T2, T3, T4, T5, T6)> Combine<T1, T2, T3, T4, T5, T6>(Result<T1> result1, 
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4,
        Result<T5> result5,
        Result<T6> result6)
    {
        var result = Combine(result1, result2, result3, result4, result5);
        if (result.IsFailure)
            return result.Error;

        if (result6.IsFailure)
            return result6.Error;

        return Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value));
    }
    
    public static Result<(T1, T2, T3, T4, T5, T6, T7)> Combine<T1, T2, T3, T4, T5, T6, T7>(Result<T1> result1, 
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4,
        Result<T5> result5,
        Result<T6> result6,
        Result<T7> result7)
    {
        var result = Combine(result1, result2, result3, result4, result5, result6);
        if (result.IsFailure)
            return result.Error;

        if (result7.IsFailure)
            return result7.Error;

        return Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value, result7.Value));
    }
    
    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8)> Combine<T1, T2, T3, T4, T5, T6, T7, T8>(Result<T1> result1, 
        Result<T2> result2,
        Result<T3> result3,
        Result<T4> result4,
        Result<T5> result5,
        Result<T6> result6,
        Result<T7> result7,
        Result<T8> result8)
    {
        var result = Combine(result1, result2, result3, result4, result5, result6, result7);
        if (result.IsFailure)
            return result.Error;

        if (result8.IsFailure)
            return result8.Error;

        return Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value, result7.Value, result8.Value));
    }
}