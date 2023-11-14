using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.ValueObjectEqualsBoxing;

public class MyBoxingValueObject : ValueObject
{
    public Guid Id { get; set; }
    public Guid Id2 { get; set; }
    public Guid Id3 { get; set; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Id;
        yield return Id2;
        yield return Id3;
    }
}

public class MyObjectWithNoValueBoxingValueObject : ValueObject
{
    public string Id { get; set; }
    public string Id2 { get; set; }
    public string Id3 { get; set; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Id;
        yield return Id2;
        yield return Id3;
    }
}

public class MyObjectAvoidBoxingValueObject : ValueObjectAvoidBoxing
{
    public Guid Id { get; set; }
    public Guid Id2 { get; set; }
    public Guid Id3 { get; set; }

    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Id.GetHashCode();
        yield return Id2.GetHashCode();
        yield return Id3.GetHashCode();
    }

    protected override bool EqualComponents(ValueObjectAvoidBoxing obj)
    {
        if (obj is not MyObjectAvoidBoxingValueObject other)
            return false;

        if (Id != other.Id)
            return false;
        if (Id2 != other.Id2)
            return false;
        if (Id3 != other.Id3)
            return false;

        return true;
    }
}

public abstract class ValueObject : IComparable<ValueObject>, IEquatable<ValueObject>
{
    private int? _cachedHashCode;

    protected abstract IEnumerable<IComparable> GetEqualityComponents();

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

    public bool Equals(ValueObject? valueObject)
    {
        if (valueObject is null)
            return false;

        if (ReferenceEquals(this, valueObject))
            return true;

        if (this.GetType() != valueObject.GetType())
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

        Type thisType = this.GetType();
        Type otherType = other.GetType();
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
}

public abstract class ValueObjectAvoidBoxing : IComparable<ValueObjectAvoidBoxing>, IEquatable<ValueObjectAvoidBoxing>
{
    private int? _cachedHashCode;

    protected abstract IEnumerable<int> GetHashCodeComponents();

    protected abstract bool EqualComponents(ValueObjectAvoidBoxing other);

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not ValueObjectAvoidBoxing otherValue)
        {
            return false;
        }

        return Equals(otherValue);
    }

    public bool Equals(ValueObjectAvoidBoxing? valueObject)
    {
        if (valueObject is null)
            return false;

        if (ReferenceEquals(this, valueObject))
            return true;

        if (this.GetType() != valueObject.GetType())
            return false;

        return EqualComponents(valueObject);
    }

    public override int GetHashCode()
    {
        if (!_cachedHashCode.HasValue)
        {
            _cachedHashCode = GetHashCodeComponents()
                .Aggregate(1, (current, hashCode) =>
                {
                    unchecked
                    {
                        return current * 23 + hashCode;
                    }
                });
        }

        return _cachedHashCode.Value;
    }


    public virtual int CompareTo(ValueObjectAvoidBoxing? other)
    {
        if (other is null)
            return 1;

        if (ReferenceEquals(this, other))
            return 0;

        Type thisType = this.GetType();
        Type otherType = other.GetType();
        if (thisType != otherType)
            return string.Compare(thisType.Name, otherType.Name, StringComparison.Ordinal);

        return
            GetHashCodeComponents().Zip(
                other.GetHashCodeComponents(),
                (left, right) =>
                    left.CompareTo(right))
            .FirstOrDefault(cmp => cmp != 0);
    }

    public static bool operator ==(ValueObjectAvoidBoxing? a, ValueObjectAvoidBoxing? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ValueObjectAvoidBoxing a, ValueObjectAvoidBoxing b)
    {
        return !(a == b);
    }
}
