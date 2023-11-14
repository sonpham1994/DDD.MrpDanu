using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Benchmark.BoxingEntityEquals;

public abstract class EntityBoxingEquals<TId> where TId : struct
{
    private int? _cachedHashCode;
    public TId Id { get; set; }

    protected EntityBoxingEquals()
    {
    }

    public override bool Equals(object? obj)
    {
        if (obj is not EntityBoxingEquals<TId> other)
            return false;

        return Equals(other);
    }

    public bool Equals(EntityBoxingEquals<TId>? other)
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

    public bool IsTransient()
    {
        return Id.Equals(default(TId));
    }

    public static bool operator ==(EntityBoxingEquals<TId>? a, EntityBoxingEquals<TId>? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityBoxingEquals<TId>? a, EntityBoxingEquals<TId>? b)
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

public abstract class EntityAvoidBoxingEquals<TId> : IEquatable<EntityAvoidBoxingEquals<TId>>
    where TId : struct, IEquatable<TId>
{
    private int? _cachedHashCode;
    public TId Id { get; set; }

    protected EntityAvoidBoxingEquals()
    {
    }

    public override bool Equals(object? obj)
    {
        if (obj is not EntityAvoidBoxingEquals<TId> other)
            return false;

        return Equals(other);
    }

    public bool Equals(EntityAvoidBoxingEquals<TId>? other)
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

    public bool IsTransient()
    {
        return Id.Equals(default(TId));
    }

    public static bool operator ==(EntityAvoidBoxingEquals<TId>? a, EntityAvoidBoxingEquals<TId>? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityAvoidBoxingEquals<TId>? a, EntityAvoidBoxingEquals<TId>? b)
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

public class MyClassBoxingEqualsAndCompare : EntityBoxingEqualsAndCompare
{
}

public class MyClassAvoidBoxingEqualsAndCompare : EntityAvoidBoxingEqualsAndCompare
{
}


public abstract class EntityBoxingEqualsAndCompare : EntityBoxingEquals<Guid>, IComparable
{
    protected EntityBoxingEqualsAndCompare()
    {
    }

    public int CompareTo(object? obj)
    {
        if (obj is not EntityBoxingEqualsAndCompare other)
            return -1;
        if (other is null)
            return 1;
        if (ReferenceEquals(this, obj))
            return 0;

        return Id.SequentialGuidCompareTo(other.Id);
    }
}

public abstract class EntityAvoidBoxingEqualsAndCompare : EntityAvoidBoxingEquals<Guid>, IComparable<EntityAvoidBoxingEqualsAndCompare>
{
    protected EntityAvoidBoxingEqualsAndCompare()
    {
    }

    public int CompareTo(EntityAvoidBoxingEqualsAndCompare? other)
    {
        if (other is null)
            return 1;
        if (ReferenceEquals(this, other))
            return 0;

        return Id.SequentialGuidCompareTo(other.Id);
    }
}

public static class GuidExtensions
{
    private static readonly SequentialGuidValueGenerator _sequantialGuidGenerator = new();
    public static Guid New()
    {
        return _sequantialGuidGenerator.Next(null);
    }


    public static int SequentialGuidCompareTo(this Guid left, Guid right)
    {
        const byte sizeGuid = 16;
        ReadOnlySpan<byte> sqlGuidOrder = stackalloc byte[sizeGuid] { 10, 11, 12, 13, 14, 15, 8, 9, 6, 7, 4, 5, 0, 1, 2, 3 };

        Span<byte> leftGuids = stackalloc byte[sizeGuid];
        Span<byte> rightGuids = stackalloc byte[sizeGuid];
        left.TryWriteBytes(leftGuids);
        right.TryWriteBytes(rightGuids);

        for (int i = 0; i < sizeGuid; i++)
        {
            if (leftGuids[sqlGuidOrder[i]] > rightGuids[sqlGuidOrder[i]])
                return 1;
            else if (leftGuids[sqlGuidOrder[i]] < rightGuids[sqlGuidOrder[i]])
                return -1;
        }

        return 0;
    }
}