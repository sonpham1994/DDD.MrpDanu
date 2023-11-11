using Domain.Extensions;

namespace Domain.SharedKernel.Base;

public abstract class ValueObject : IComparable<ValueObject>, IEquatable<ValueObject>
{
    private int? _cachedHashCode;

    protected abstract IEnumerable<IComparable> GetEqualityComponents();

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

        if (obj is not ValueObject otherValue)
        {
            return false;
        }

        return Equals(otherValue);
    }
    
    //improving comparing performance by not using cast valueobject to object
    // please check benchmarks.benchmark.CastObject
    public bool Equals(ValueObject? valueObject)
    {
        if (valueObject is null)
            return false;
        
        if (ReferenceEquals(this, valueObject))
            return true;

        if (this.GetUnproxiedType() != valueObject.GetUnproxiedType())
            return false;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        if (!_cachedHashCode.HasValue)
        {
            _cachedHashCode = GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        return _cachedHashCode.Value;
    }


    public virtual int CompareTo(ValueObject? other)
    {
        if (other is null)
            return 1;

        if (ReferenceEquals(this, other))
            return 0;

        Type thisType = this.GetUnproxiedType();
        Type otherType = other.GetUnproxiedType();
        if (thisType != otherType)
            return string.Compare(thisType.Name, otherType.Name, StringComparison.Ordinal);

        return
            GetEqualityComponents().Zip(
                other.GetEqualityComponents(),
                (left, right) =>
                    left?.CompareTo(right) ?? (right is null ? 0 : -1))
            .FirstOrDefault(cmp => cmp != 0);
    }

    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return !(a == b);
    }

    public T GetCopy<T>()
    {
        return (T)this.MemberwiseClone();
    }
}