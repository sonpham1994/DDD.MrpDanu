namespace Benchmark.Domain.LinqExtensions;

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
    
    public static IResult AnyFailureInvoke<T>(this IEnumerable<T> enumerable, Func<T, IResult> predicate)
    {
        foreach (var element in enumerable)
        {
            var result = predicate.Invoke(element);

            if (result.IsFailure)
                return result;
        } 

        return Result.Success();
    }
    
    public static TResult? ItemDuplicationWithGroup<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : Entity
    {
        TResult? result = null;
        
        var itemDuplication = enumerable
            .Where(x => x is not null)
            .GroupBy(predicate)
            .Select(x => new { x.Key, Count = x.Count() })
            .FirstOrDefault(x => x.Count > 1);
    
        if (itemDuplication is not null)
            result = itemDuplication.Key;
        
        return result;
    }
    
    public static TResult? ItemDuplicationWithForeachDictionary<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : Entity
    {
        TResult? result = null;
        var items = new Dictionary<TResult, byte>();
        
        foreach (var element in enumerable)
        {
            if (element is null) continue;

            var tResult = predicate.Invoke(element);
            if (!items.ContainsKey(tResult))
            {
                items.Add(tResult, 1);
            }
            else
            {
                result = tResult;
                break;
            }
        }
        
        return result;
    }
    
    public static TResult? ItemDuplicationWithForeachHashSet<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : Entity
    {
        TResult? result = null;
        var items = new HashSet<TResult>();
        
        foreach (var element in enumerable)
        {
            if (element is null) continue;

            var tResult = predicate.Invoke(element);
            if (!items.Add(tResult))
            {
                result = tResult;
                break;
            }
        }
        
        return result;
    }
    
    public static TResult? ItemDuplicationWithForeachAny<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : Entity
    {
        TResult? result = null;
        int i = 1;
        var items = enumerable.Where(x => x is not null).ToList();

        foreach (var element in items)
        {
            var tResult = predicate.Invoke(element);
            bool isDuplication = items.Skip(i).Any(x => predicate.Invoke(x) == tResult);
            if (isDuplication)
            {
                result = tResult;
                break;
            }

            i++;
        }
        
        return result;
    }
    
    public static TResult? ItemDuplicationWithForAny<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : Entity
    {
        TResult? result = null;
        int count = enumerable.Count();
        var items = enumerable.Where(x => x is not null).ToList();

        for (int i = 0; i < count; i++)
        {
            var tResult = predicate.Invoke(items.ElementAt(i));
            bool isDuplication = items.Skip(i + 1).Any(x => predicate.Invoke(x) == tResult);
            if (isDuplication)
            {
                result = tResult;
                break;
            }
        }
        
        return result;
    }
    
    public static TResult? ItemDuplicationWithAlgorithm<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : Entity
    {
        TResult? result = null;
        int count = enumerable.Count();
        
        for (int i = 0; i < count; i++)
        {
            var tResult1 = predicate.Invoke(enumerable.ElementAt(i));
            if (tResult1 is null) continue;

            for (int j = i + 1; j < count; j++)
            {
                var tResult2 = predicate.Invoke(enumerable.ElementAt(j));
                if (tResult2 is null) continue;

                if (tResult1 == tResult2)
                {
                    result = tResult1;
                    return result;
                }
            }
        }
        
        return result;
    }
    
    public static TResult? ItemDuplicationWithAlgorithmAndHashCode<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : Entity
    {
        TResult? result = null;
        int count = enumerable.Count();
        
        for (int i = 0; i < count; i++)
        {
            var tResult1 = predicate.Invoke(enumerable.ElementAt(i));
            if (tResult1 is null) continue;

            for (int j = i + 1; j < count; j++)
            {
                var tResult2 = predicate.Invoke(enumerable.ElementAt(j));
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
    
    public static TResult? ItemDuplicationWithAlgorithmAndHashCodeWithCheckingList<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : Entity
    {
        TResult? result = null;
        if (enumerable is List<T> lst)
        {
            int count = lst.Count;
        
            for (int i = 0; i < count; i++)
            {
                var tResult1 = predicate.Invoke(lst[i]);
                if (tResult1 is null) continue;

                for (int j = i + 1; j < count; j++)
                {
                    var tResult2 = predicate.Invoke(lst[j]);
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
        }
        
        return result;
    }
    
    public static TResult? ItemDuplicationWithAlgorithmAndHashCodeWithCheckingIReadOnlyList<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : Entity
    {
        TResult? result = null;
        if (enumerable is IReadOnlyList<T> lst)
        {
            int count = lst.Count;
        
            for (int i = 0; i < count; i++)
            {
                var tResult1 = predicate.Invoke(lst[i]);
                if (tResult1 is null) continue;

                for (int j = i + 1; j < count; j++)
                {
                    var tResult2 = predicate.Invoke(lst[j]);
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
        }
        
        
        return result;
    }

    public static bool AnyDuplicationWithAlgorithmAndHashCode<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
    {
        bool result = false;
        int count = enumerable.Count();

        for (int i = 0; i < count; i++)
        {
            var tResult1 = predicate.Invoke(enumerable.ElementAt(i));
            if (tResult1 is null) continue;

            for (int j = i + 1; j < count; j++)
            {
                var tResult2 = predicate.Invoke(enumerable.ElementAt(j));
                if (tResult2 is null) continue;

                int hashCode1 = tResult1.GetHashCode();
                int hashCode2 = tResult2.GetHashCode();
                if (hashCode1 != hashCode2)
                    continue;

                if (tResult1.Equals(tResult2))
                {
                    return true;
                }
            }
        }

        return result;
    }

    public static bool AnyDuplicationCheckingTypeWithAlgorithmAndHashCode<T, TResult>(this IEnumerable<T> enumerable,
        Func<T, TResult> predicate)
    {
        bool result = false;
        if (enumerable is List<T> lst)
        {
            int count = lst.Count;

            for (int i = 0; i < count; i++)
            {
                var tResult1 = predicate(lst[i]);
                if (tResult1 is null) continue;

                for (int j = i + 1; j < count; j++)
                {
                    var tResult2 = predicate(lst[j]);
                    if (tResult2 is null) continue;

                    int hashCode1 = tResult1.GetHashCode();
                    int hashCode2 = tResult2.GetHashCode();
                    if (hashCode1 != hashCode2)
                        continue;

                    if (tResult1.Equals(tResult2))
                    {
                        return true;
                    }
                }
            }
        }

        return result;
    }

}