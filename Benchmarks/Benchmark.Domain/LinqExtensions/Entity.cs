namespace Benchmark.Domain.LinqExtensions;

public abstract class Entity<TId>
{
    private int? _cachedHashCode;
    public TId Id { get; set; }

    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (this.GetType() != other.GetType())
            return false;

        if (IsTransient() || other.IsTransient())
            return false;

        return Id.Equals(other.Id);
    }

    public bool IsTransient()
    {
        return Id is null || Id.Equals(default(TId));
    }

    public static bool operator ==(Entity<TId> a, Entity<TId> b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId> a, Entity<TId> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        if (_cachedHashCode.HasValue)
            return _cachedHashCode.Value;

        _cachedHashCode = (this.GetType().Name.GetHashCode() + Id.GetHashCode()).GetHashCode();

        return _cachedHashCode.Value;
    }
}

public abstract class Entity : Entity<Guid>
{
    protected Entity()
    {
    }

    protected Entity(Guid id)
        : base(id)
    {
    }
}