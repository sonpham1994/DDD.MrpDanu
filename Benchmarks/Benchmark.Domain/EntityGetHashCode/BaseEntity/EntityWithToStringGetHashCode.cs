namespace Benchmark.Domain.EntityGetHashCode.BaseEntity;

public abstract class EntityWithToStringGetHashCode<TId>
{
    public TId Id { get; set; }

    protected EntityWithToStringGetHashCode()
    {
    }

    protected EntityWithToStringGetHashCode(TId id)
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

    public static bool operator ==(EntityWithToStringGetHashCode<TId> a, EntityWithToStringGetHashCode<TId> b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityWithToStringGetHashCode<TId> a, EntityWithToStringGetHashCode<TId> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}