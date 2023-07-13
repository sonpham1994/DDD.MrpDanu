using Domain.Extensions;

namespace Domain.SharedKernel.Base;

public abstract class ValueObject : IComparable, IComparable<ValueObject>
{
    private int? _cachedHashCode;

    protected abstract IEnumerable<IComparable> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (this.GetUnproxiedType() != obj.GetUnproxiedType())
            return false;

        var valueObject = (ValueObject)obj;

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
            return string.Compare($"{thisType}", $"{otherType}", StringComparison.Ordinal);

        return
            GetEqualityComponents().Zip(
                other.GetEqualityComponents(),
                (left, rigth) =>
                    left?.CompareTo(rigth) ?? (rigth is null ? 0 : -1))
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