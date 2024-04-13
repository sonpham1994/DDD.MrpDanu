namespace Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks.EntityDuplication;

public abstract class Entity<TId> : IEntity, IEquatable<Entity<TId>> 
    where TId : struct, IEquatable<TId>
{
    private int? _cachedHashCode;
    public TId Id { get; set; }

    protected Entity()
    {
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        return Equals(other);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (this.GetType() != other.GetType())
            return false;

        if (IsTransient() || other.IsTransient())
            return false;

        return Id.Equals(other.Id);
    }

    public virtual bool IsTransient()
    {
        return Id.Equals(default(TId));
    }

    public static bool operator ==(Entity<TId>? a, Entity<TId>? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId>? a, Entity<TId>? b)
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
}

public abstract class EntityGuidStronglyTypedId<TId> : Entity<TId>
    where TId : struct, IEquatable<TId>
{
}

public interface IEntity
{
}