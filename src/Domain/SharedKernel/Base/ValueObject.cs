using Domain.Extensions;

namespace Domain.SharedKernel.Base;

public abstract class ValueObject : IComparable, IComparable<ValueObject>, IEquatable<ValueObject>
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

    public virtual int CompareTo(object? other)
    {
        return CompareTo(other as ValueObject);
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