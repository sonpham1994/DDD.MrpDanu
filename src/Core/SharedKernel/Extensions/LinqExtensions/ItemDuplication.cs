using SharedKernel.Exceptions;

namespace SharedKernel.Extensions.LinqExtensions;

public static partial class ItemDuplication
{
    //https://www.youtube.com/watch?v=zYavFBwsJxE&ab_channel=MilanJovanovi%C4%87
    public static TProperty? GetItemDuplication<T, TProperty>(
        this IEnumerable<T> enumerable, 
        Func<T?, TProperty?> predicate)
        where TProperty : IEquatable<TProperty>
    {
        TProperty? result;
        if (enumerable is List<T> lst)
        {
            result = GetItemDuplication(lst, predicate);
        }
        else if (enumerable is T[] array)
        {
            result = GetItemDuplication(array, predicate);
        }
        else
        {
            throw new DomainException(DomainErrors.ItemDuplication.NotSupportType(enumerable.GetType().Name));
        }
        
        return result;
    }

    public static T? GetItemDuplication<T>(this IEnumerable<T?> enumerable)
        where T : IEquatable<T>
    {
        T? result;
        Func<T?, T?> func = x => x;
        if (enumerable is List<T> lst)
        {
            result = GetItemDuplication(lst, func);
        }
        else if (enumerable is T[] array)
        {
            result = GetItemDuplication(array, func);
        }
        else
        {
            throw new DomainException(DomainErrors.ItemDuplication.NotSupportType(enumerable.GetType().Name));
        }
        
        return result;
    }
    
    public static T? GetItemDuplication<T>(this List<T> list)
        where T : IEquatable<T>
    {
        int count = list.Count;
        var result = LinearSearch(list, x=> x, count);
        
        return result;
    }
    
    public static T? GetItemDuplication<T>(this T[] array)
        where T : IEquatable<T>
    {
        int count = array.Length;
        var result = LinearSearch(array, x=> x, count);
        
        return result;
    }
    
    public static TProperty? GetItemDuplication<T, TProperty>(this List<T> list, Func<T?, TProperty?> predicate)
        where TProperty : IEquatable<TProperty>
    {
        int count = list.Count;
        var result = LinearSearch(list, predicate, count);
        
        return result;
    }
    
    public static TProperty? GetItemDuplication<T, TProperty>(this T[] array, Func<T?, TProperty?> predicate)
        where TProperty : IEquatable<TProperty>
    {
        int count = array.Length;
        var result = LinearSearch(array, predicate, count);
        
        return result;
    }
    
    // O(n square) -> n bình phương 2
    private static TProperty? LinearSearch<T, TProperty>(
        IReadOnlyList<T?> array, 
        Func<T?, TProperty?> func, 
        int count)
        where TProperty : IEquatable<TProperty>
    {
        TProperty? result = default;
        for (int i = 0; i < count; i++)
        {
            if (array[i] is null)
                continue;
            
            var tResult1 = func(array[i]);
            if (tResult1 is null || tResult1.Equals(default)) continue;

            for (int j = i + 1; j < count; j++)
            {
                if (array[j] is null)
                    continue;
                
                var tResult2 = func(array[j]);
                if (tResult2 is null || tResult2.Equals(default)) continue;

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