namespace SharedKernel.Extensions.LinqExtensions;

public static class LinqExtension
{
    public static Result AnyFailure<T>(this IEnumerable<T> enumerable, Func<T, Result> predicate)
    {
        foreach (var element in enumerable)
        {
            var result = predicate(element);

            if (!result.IsSuccess)
                return result;
        } 

        return Result.Success();
    }

    // Please check DDD.MrpDanu\Benchmarks\Benchmark\FirstOrDefaultVsFind\FirstOrDefaultVsFindBenchmark.cs
    public static T? FindOrFirstDefault<T>(this IReadOnlyList<T> input, Predicate<T> predicate)
    {
        if (input is List<T> list)
        {
            return list.Find(predicate);
        }
        else
        {
            Func<T, bool> func = new(predicate);
            return input.FirstOrDefault(func);
        }
    }

    //Please check DDD.MrpDanu\Benchmarks\Benchmark\ExistsOrAny\ExistsOrAnyBenchmark.cs
    public static bool ExistOrAny<T>(this IReadOnlyList<T> input, Predicate<T> predicate)
    {
        if (input is List<T> list)
        {
            return list.Exists(predicate);
        }
        else
        {
            Func<T, bool> func = new(predicate);
            return input.Any(func);
        }
    }
}