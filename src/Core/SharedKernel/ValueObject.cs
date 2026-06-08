using SharedKernel.Extensions;

namespace SharedKernel;

public interface IValueObject;

/*TODO: Apply Source Generator: Generate EqualComponents and GetHashCodeComponents
 * - Source Generator can be longer at compile time. It can apply for small project such as micro service
 * - If it's a big project or modular project, and we you don't care about memory, we can apply Sequential Component as
 * Vladimir Khorikov does, this will reduce your code when inherit ValueObject. However, applying source generator can
 * make the big project or modular project significantly slow at compile time, hence it's not ideal for big or modular project.
 *  - Otherwise, apply this approach for improve performance and reduce memory usage.
 */
public abstract class ValueObject<T> : IValueObject, IEquatable<T>
    where T : ValueObject<T>
{
    private int? _cachedHashCode;

    // this will cause boxing when the property is value type and IComparable is interface and it returns value type as IComparable -> boxing
    // so we don't use this approach from lib of Vladimir khorikov (https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/master/CSharpFunctionalExtensions/ValueObject/ValueObject.cs#L13)
    // please check Benchmarks\Benchmark\ValueObjectEqualsBoxing\ValueObjectEqualsBoxingResult.md to see the boxing
    //protected abstract IEnumerable<IComparable> GetEqualityComponents();

    protected abstract IEnumerable<int> GetHashCodeComponents();

    protected abstract bool EqualComponents(T valueObject);

    /* don't know why .Net or EntityFramework calls this Equals(object? obj) a lot. It will cast from valueObject
     * to object and decrease your app performance.
     * Answer:
     *  - When you load the internal entities, it seems like the EF will call the ValueObject, which is owned
     *  entity type to compare (not to know compare what) and it will call all owned entity types in an entity
     *  to compare. For example currently we use LazyLoading, in TransactionalPartner when we refer to
     *  ContactPersonInformation, which is internal entity of TransactionalPartner. It calls this Equals method to compare
     * - Another case is when you call ChangeTracker from DbContext, it also calls these owned entity types to compare
     * - And another case is when you call SaveChanges method from DbContext, it also calls these owned entity types to compare
     */
    /*
     * by calling this Equals(object? obj), it increases a lot of casting behaviour and reduce performance.
     * Need to somehow to void this casting object and use Equals(ValueObject? valueObject) instead
     */
    /*
     * When we use ChangeTracker for load data. It will call DetectValueChange and result in a lot of comparing values in
     * Entity. (it comes from "public object? this[IPropertyBase propertyBase]" in InternalEntityEntry, this place
     * will return value type and convert it to object -> boxing in "Microsoft.EntityFrameworkCore.ChangeTracking.Internal"
     * namespace)
     * When I investigate this method why EF call this a lot and I see the EF boxing value a lot. It will call all
     * properties that exist in object and compare them for Detecting value change in DetectValueChange method.
     * It also compare two objects. For example Website1 and Website2 objects, and after that comparing the properties
     * in two objects Website1 and Website2.
     * EF call this Equals method and also introduce a lot of boxing through this "Equals(object? left, object? right)"
     * in "public class ValueComparer" class in "Microsoft.EntityFrameworkCore.ChangeTracking" namespace, and also in
     * "bool Equals(object? objA, object? objB)" in "Object class" in "System" namespace
     */
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not T otherValue)
        {
            return false;
        }

        return Equals(otherValue);
    }
    
    //improving comparing performance by not using cast valueobject to object
    // please check benchmarks.benchmark.CastObject
    public bool Equals(T? valueObject)
    {
        if (valueObject is null)
            return false;
        
        if (ReferenceEquals(this, valueObject))
            return true;

        if (this.GetUnproxiedType() != valueObject.GetUnproxiedType())
            return false;

        return EqualComponents(valueObject);
    }

    public override int GetHashCode()
    {
        if (!_cachedHashCode.HasValue)
        {
            _cachedHashCode = nameof(ValueObject<>).GetHashCode() + typeof(T).Name.GetHashCode() + GetHashCodeComponents()
                // Dbj2 hash
                .Aggregate(1, (currentHashCode, hashCodeComponent) =>
                {
                    unchecked
                    {
                        return currentHashCode * 23 + hashCodeComponent;
                    }
                });
        }

        return _cachedHashCode.Value;
    }

    public static bool operator ==(ValueObject<T>? a, ValueObject<T>? b)
    {
        if (a is null || b is null)
            return false;
        
        if (a is not T aT || b is not T bT)
        {
            return false;
        }
        
        return aT.Equals(bT);
    }

    public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        => !(a == b);
}