using Domain.SharedKernel.Base;

namespace Domain.Extensions;

public static class LinqExtension
{
    public static Result AnyFailure<T>(this IEnumerable<T> enumerable, Func<T, Result> predicate)
    {
        foreach (var element in enumerable)
        {
            var result = predicate(element);

            if (result.IsFailure)
                return result;
        } 

        return Result.Success();
    }

    public static T? FindOrFirstDefault<T>(this IReadOnlyList<T> input, Func<T, bool> func)
    {
        if (input is List<T> list)
        {
            Predicate<T> predicate = new Predicate<T>(func);
            return list.Find(predicate);
        }
        else
        {
            return input.FirstOrDefault(func);
        }
    }
    
    
}