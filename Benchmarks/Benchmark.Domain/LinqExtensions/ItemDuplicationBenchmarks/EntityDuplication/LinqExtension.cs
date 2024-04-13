namespace Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks.EntityDuplication;

public static class LinqExtension
{
    public static TResult? ItemDuplicationWithGroup<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> predicate)
        where TResult : class, IEntity
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
        where TResult : class, IEntity
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
        where TResult : class, IEntity
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
        where TResult : class, IEntity
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
        where TResult : class, IEntity
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
}