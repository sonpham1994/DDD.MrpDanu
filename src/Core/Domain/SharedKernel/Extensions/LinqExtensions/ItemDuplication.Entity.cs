using Domain.SharedKernel.Base;
using Domain.SharedKernel.Exceptions;

namespace Domain.SharedKernel.Extensions.LinqExtensions;

public static partial class ItemDuplication
{
    public static TProperty GetEntityDuplication<T, TProperty>(
        this IEnumerable<T> enumerable, 
        Func<T, TProperty> predicate)
        where T : class, IEntity
        where TProperty : IEntity
    {
        TProperty? result;
        if (enumerable is List<T> lst)
        {
            result = GetEntityDuplication(lst, predicate);
        }
        else if (enumerable is T[] array)
        {
            result = GetEntityDuplication(array, predicate);
        }
        else
        {
            throw new DomainException(DomainErrors.ItemDuplication.NotSupportType(enumerable.GetType().Name));
        }
        
        return result;
    }

    public static TProperty? GetEntityDuplication<T, TProperty>(
        this List<T> list, 
        Func<T, TProperty> predicate)
        where T : IEntity
        where TProperty : class, IEntity
    {
        var result = LinearSearchEntity(list, predicate);
        
        return result;
    }
    
    public static TProperty? GetEntityDuplication<T, TProperty>(
        this T[] array, 
        Func<T, TProperty> predicate)
        where T : IEntity
        where TProperty : class, IEntity
    {
        var result = LinearSearchEntity(array, predicate);
        
        return result;
    }
    
    private static TProperty? LinearSearchEntity<T, TProperty>(
        IReadOnlyList<T?>? array, 
        Func<T, TProperty> func)
        where T : IEntity
        where TProperty : class, IEntity
    {
        if (array is null)
        {
            return null;
        }
        
        var count = array.Count;
        var result = LinearSearchEntity( index =>
        {
            if (array[index] is null)
                return null;
            
            return func(array[index]!);
        }, 
        count);

        return result;
    }
    
    public static T? GetEntityDuplication<T>(this IEnumerable<T?> enumerable)
        where T : IEntity
    {
        T? result;
        if (enumerable is List<T> lst)
        {
            result = GetEntityDuplication(lst!);
        }
        else if (enumerable is T[] array)
        {
            result = GetEntityDuplication(array);
        }
        else
        {
            throw new DomainException(DomainErrors.ItemDuplication.NotSupportType(enumerable.GetType().Name));
        }
        
        return result;
    }
    
    public static T? GetEntityDuplication<T>(this List<T?> list)
        where T : IEntity
    {
        int count = list.Count;
        var result = LinearSearchEntity(list, count);
        
        return result;
    }
    
    public static T? GetEntityDuplication<T>(this T[] array)
        where T : IEntity
    {
        int count = array.Length;
        var result = LinearSearchEntity(array, count);
        
        return result;
    }
    
    private static T? LinearSearchEntity<T>(
        IReadOnlyList<T?> array, 
        int count)
        where T : IEntity
    {
        var result = LinearSearchEntity(index => array[index], count);
    
        return result;
    }
    
    // O(n square) -> n bình phương 2
    private static TResult? LinearSearchEntity<TResult>(
        Func<int, TResult?> tResult,
        int count)
        where TResult: IEntity
    {
        TResult? result = default;
        for (int i = 0; i < count; i++)
        {
            var tResult1 = tResult(i);
            if (tResult1 is null) continue;

            for (int j = i + 1; j < count; j++)
            {
                var tResult2 = tResult(j);
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