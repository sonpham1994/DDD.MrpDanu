namespace Benchmark.Domain.EntityGetHashCode.BaseEntity;

public abstract class CacheEntityWithToStringGetHashCode<TId>
{
    private int? _cachedHashCode;
    public TId Id { get; set; }

    protected CacheEntityWithToStringGetHashCode()
    {
    }

    protected CacheEntityWithToStringGetHashCode(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not EntityWithToStringGetHashCode<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (IsTransient() || other.IsTransient())
            return false;

        return Id.Equals(other.Id);
    }

    public bool IsTransient()
    {
        return Id is null || Id.Equals(default(TId));
    }

    public static bool operator ==(CacheEntityWithToStringGetHashCode<TId> a, CacheEntityWithToStringGetHashCode<TId> b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(CacheEntityWithToStringGetHashCode<TId> a, CacheEntityWithToStringGetHashCode<TId> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        if (_cachedHashCode.HasValue)
            return _cachedHashCode.Value;

        _cachedHashCode = (GetType().Name + Id).GetHashCode();

        return _cachedHashCode.Value;
    }
}