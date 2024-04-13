using Domain.SharedKernel.Base;
using Domain.SharedKernel.Exceptions;

namespace Domain.Extensions.LinqExtensions;

public static partial class ItemDuplication
{
    public static T GetValueObjectDuplication<T>(this IEnumerable<T> enumerable)
        where T : ValueObject
    {
        T result = default;
        if (enumerable is List<T> lst)
        {
            result = GetValueObjectDuplication(lst);
        }
        else if (enumerable is T[] array)
        {
            result = GetValueObjectDuplication(array);
        }
        else
        {
            throw new DomainException(DomainErrors.ItemDuplication.NotSupportType(enumerable.GetType().Name));
        }
        
        return result;
    }
    
    public static T GetValueObjectDuplication<T>(this List<T> list)
        where T : ValueObject
    {
        int count = list.Count;
        var result = LinearSearchValueObject(list, count);
        
        return result;
    }
    
    public static T GetValueObjectDuplication<T>(this T[] array)
        where T : ValueObject
    {
        int count = array.Length;
        var result = LinearSearchValueObject(array, count);
        
        return result;
    }
    
    private static T LinearSearchValueObject<T>(
        IReadOnlyList<T> array, 
        int count)
        where T : ValueObject
    {
        var result = LinearSearchValueObject(index => array[index], count);
    
        return result;
    }
    
    private static TProperty LinearSearchValueObject<T, TProperty>(
        IReadOnlyList<T> array, 
        Func<T, TProperty> func)
        where T : IEntity
        where TProperty : ValueObject
    {
        var count = array.Count;
        var result = LinearSearchValueObject( index =>
        {
            if (array[index] is null)
                return null;
            
            return func(array[index]);
        }, count);

        return result;
    }
    
    // O(n square) -> n bình phương 2
    private static TResult LinearSearchValueObject<TResult>(
        Func<int, TResult> tResult,
        int count)
        where TResult: ValueObject
    {
        TResult result = default;
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