using Domain.SharedKernel.Base;
using Domain.SharedKernel.Exceptions;

namespace Domain.Extensions.LinqExtensions;

public static partial class ItemDuplication
{
    public static TProperty GetEntityDuplication<T, TProperty>(
        this IEnumerable<T> enumerable, 
        Func<T, TProperty> predicate)
        where T : IEntity, IEquatable<T>
        where TProperty : IEquatable<TProperty>
    {
        TProperty result = default;
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

    public static TProperty GetEntityDuplication<T, TProperty>(
        this List<T> list, 
        Func<T, TProperty> predicate)
        where T : IEntity, IEquatable<T>
        where TProperty : IEquatable<TProperty>
    {
        int count = list.Count;
        var result = LinearSearchEntity(list, predicate, count);
        
        return result;
    }
    
    public static TProperty GetEntityDuplication<T, TProperty>(
        this T[] array, 
        Func<T, TProperty> predicate)
        where T : IEntity, IEquatable<T>
        where TProperty : IEquatable<TProperty>
    {
        int count = array.Length;
        var result = LinearSearchEntity(array, predicate, count);
        
        return result;
    }
    
    // O(n square) -> n bình phương 2
    private static TProperty LinearSearchEntity<T, TProperty>(
        IReadOnlyList<T> array, 
        Func<T, TProperty> predicate, 
        int count)
        where T : IEntity, IEquatable<T>
    {
        TProperty result = default;
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
    
    public static T GetEntityDuplication<T>(this IEnumerable<T> enumerable)
        where T : IEntity
    {
        T result = default;
        Func<T, T> func = x => x;
        if (enumerable is List<T> lst)
        {
            result = GetEntityDuplication(lst);
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
    
    public static T GetEntityDuplication<T>(this List<T> list)
        where T : IEntity
    {
        int count = list.Count;
        var result = LinearSearchEntity(list, count);
        
        return result;
    }
    
    public static T GetEntityDuplication<T>(this T[] array)
        where T : IEntity, IEquatable<T>
    {
        int count = array.Length;
        var result = LinearSearchEntity(array, count);
        
        return result;
    }
    
    // O(n square) -> n bình phương 2
    private static T LinearSearchEntity<T>(
        IReadOnlyList<T> array, 
        int count)
        where T : IEntity
    {
        T result = default;
        for (int i = 0; i < count; i++)
        {
            var tResult1 = array[i];
            if (tResult1 is null) continue;

            for (int j = i + 1; j < count; j++)
            {
                var tResult2 = (array[j]);
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