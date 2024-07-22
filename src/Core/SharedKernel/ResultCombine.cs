namespace SharedKernel;

public static class ResultCombine
{
    public static Result<(T1, T2)> Create<T1, T2>(
        in Result<T1> result1, 
        in Result<T2> result2)
    {
        if (!result1.IsSuccess)
            return result1.Error;

        if (!result2.IsSuccess)
            return result2.Error;

        return Result.Success((result1.Value, result2.Value));
    }
    
    public static Result<(T1, T2, T3)> Create<T1, T2, T3>(
        in Result<T1> result1, 
        in Result<T2> result2,
        in Result<T3> result3)
    {
        var result = Create(result1, result2);
        if (!result.IsSuccess)
            return result.Error;

        if (!result3.IsSuccess)
            return result3.Error;

        return Result.Success((result1.Value, result2.Value, result3.Value));
    }
    
    public static Result<(T1, T2, T3, T4)> Create<T1, T2, T3, T4>(
        in Result<T1> result1, 
        in Result<T2> result2,
        in Result<T3> result3,
        in Result<T4> result4)
    {
        var result = Create(result1, result2, result3);
        if (!result.IsSuccess)
            return result.Error;

        if (!result4.IsSuccess)
            return result4.Error;

        return Result.Success((result1.Value, result2.Value, result3.Value, result4.Value));
    }
    
    public static Result<(T1, T2, T3, T4, T5)> Create<T1, T2, T3, T4, T5>(
        in Result<T1> result1, 
        in Result<T2> result2,
        in Result<T3> result3,
        in Result<T4> result4,
        in Result<T5> result5)
    {
        var result = Create(result1, result2, result3, result4);
        if (!result.IsSuccess)
            return result.Error;

        if (!result5.IsSuccess)
            return result5.Error;

        return Result.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value));
    }
    
    public static Result<(T1, T2, T3, T4, T5, T6)> Create<T1, T2, T3, T4, T5, T6>(
        in Result<T1> result1, 
        in Result<T2> result2,
        in Result<T3> result3,
        in Result<T4> result4,
        in Result<T5> result5,
        in Result<T6> result6)
    {
        var result = Create(result1, result2, result3, result4, result5);
        if (!result.IsSuccess)
            return result.Error;

        if (!result6.IsSuccess)
            return result6.Error;

        return Result.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value));
    }
    
    public static Result<(T1, T2, T3, T4, T5, T6, T7)> Create<T1, T2, T3, T4, T5, T6, T7>(
        in Result<T1> result1, 
        in Result<T2> result2,
        in Result<T3> result3,
        in Result<T4> result4,
        in Result<T5> result5,
        in Result<T6> result6,
        in Result<T7> result7)
    {
        var result = Create(result1, result2, result3, result4, result5, result6);
        if (!result.IsSuccess)
            return result.Error;

        if (!result7.IsSuccess)
            return result7.Error;

        return Result.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value, result7.Value));
    }
    
    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8)> Create<T1, T2, T3, T4, T5, T6, T7, T8>(
        in Result<T1> result1, 
        in Result<T2> result2,
        in Result<T3> result3,
        in Result<T4> result4,
        in Result<T5> result5,
        in Result<T6> result6,
        in Result<T7> result7,
        in Result<T8> result8)
    {
        var result = Create(result1, result2, result3, result4, result5, result6, result7);
        if (!result.IsSuccess)
            return result.Error;

        if (!result8.IsSuccess)
            return result8.Error;

        return Result.Success((result1.Value, result2.Value, result3.Value, result4.Value, result5.Value, result6.Value, result7.Value, result8.Value));
    }
}