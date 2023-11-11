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
    
    //https://www.youtube.com/watch?v=zYavFBwsJxE&ab_channel=MilanJovanovi%C4%87
    public static TResult? ItemDuplication<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where T : Entity<Guid>
        where TResult : Entity<Guid>
    {
        TResult? result = null;
        if (enumerable is List<T> lst)
        {
            result = ItemDuplication(lst, predicate);
        }
        else if (enumerable is T[] array)
        {
            result = ItemDuplication(array, predicate);
        }
        else
        {
            throw new ArgumentException($"DDD.MrpDanu.Fail: Does not support {enumerable.GetType()} yet.");
        }
        
        return result;
    }
    
    public static TResult? ItemDuplication<T, TResult>(this List<T> list, Func<T, TResult> predicate)
        where T : Entity<Guid>
        where TResult : Entity<Guid>
    {
        int count = list.Count;
        TResult? result = LinearSearch(list, predicate, count);
        
        return result;
    }
    
    public static TResult? ItemDuplication<T, TResult>(this T[] array, Func<T, TResult> predicate)
        where T : Entity<Guid>
        where TResult : Entity<Guid>
    {
        int count = array.Length;
        TResult? result = LinearSearch(array, predicate, count);
        
        return result;
    }

    // O(n square) -> n bình phương 2
    private static TResult? LinearSearch<T, TResult>(IReadOnlyList<T> array, Func<T, TResult> predicate, int count)
    {
        TResult? result = default(TResult);
        for (int i = 0; i < count; i++)
        {
            var tResult1 = predicate(array[i]);
            if (tResult1 is null) continue;

            for (int j = i + 1; j < count; j++)
            {
                var tResult2 = predicate(array[j]);
                if (tResult2 is null) continue;

                int hashCode1 = tResult1.GetHashCode();
                int hashCode2 = tResult2.GetHashCode();
                if (hashCode1 != hashCode2)
                    continue;

                if (tResult1.Equals(tResult2))
                {
                    return tResult1;
                }
            }
        }

        return result;
    }
}