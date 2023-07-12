using Domain.SharedKernel.Base;

namespace Domain.Extensions;

public static class LinqExtension
{
    public static IResult AnyFailure<T>(this IEnumerable<T> enumerable, Func<T, IResult> predicate)
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
        int count = enumerable.Count();
        
        for (int i = 0; i < count; i++)
        {
            var tResult1 = predicate(enumerable.ElementAt(i));
            if (tResult1 is null) continue;

            for (int j = i + 1; j < count; j++)
            {
                var tResult2 = predicate(enumerable.ElementAt(j));
                if (tResult2 is null) continue;
                
                int hashCode1 = tResult1.GetHashCode();
                int hashCode2 = tResult2.GetHashCode();
                if (hashCode1 != hashCode2)
                    continue;
                
                if (tResult1 == tResult2)
                {
                    result = tResult1;
                    return result;
                }
            }
        }
        
        return result;
    }
    
    public static TResult? ItemDuplication<T, TResult>(this List<T> enumerable, Func<T, TResult> predicate)
        where T : Entity<Guid>
        where TResult : Entity<Guid>
    {
        TResult? result = null;
        int count = enumerable.Count();
        
        for (int i = 0; i < count; i++)
        {
            var tResult1 = predicate.Invoke(enumerable[i]);
            if (tResult1 is null) continue;

            for (int j = i + 1; j < count; j++)
            {
                var tResult2 = predicate.Invoke(enumerable[j]);
                if (tResult2 is null) continue;
                
                int hashCode1 = tResult1.GetHashCode();
                int hashCode2 = tResult2.GetHashCode();
                if (hashCode1 != hashCode2)
                    continue;
                
                if (tResult1 == tResult2)
                {
                    result = tResult1;
                    return result;
                }
            }
        }
        
        return result;
    }
}