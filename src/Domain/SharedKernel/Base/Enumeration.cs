using System.Reflection;
using Domain.Extensions;

namespace Domain.SharedKernel.Base;

public abstract class Enumeration<T> : IComparable, IEquatable<Enumeration<T>>
    where T : Enumeration<T>
{
    private int? cachedHashCode;
    public static readonly IReadOnlyCollection<T> List = CreateEnumerations();

    public byte Id { get; }
    public string Name { get; }

    protected Enumeration() { }

    protected Enumeration(in byte id, string name) => (Id, Name) = (id, name);

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration<T> otherValue)
        {
            return false;
        }

        return Equals(otherValue);
    }

    //improving comparing performance by not using cast valueobject to object
    // please check benchmarks.benchmark.CastObject
    public bool Equals(Enumeration<T>? otherValue)
    {
        if (otherValue is null)
            return false;
        
        if (ReferenceEquals(this, otherValue))
            return true;
        
        var typeMatches = this.GetUnproxiedType().Equals(otherValue.GetUnproxiedType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }
    
    public static bool operator ==(Enumeration<T> a, Enumeration<T> b) => a.Equals(b);

    public static bool operator !=(Enumeration<T> a, Enumeration<T> b) => !(a == b);

    public static Result<T> FromId(byte id)
    {
        var result = List.FirstOrDefault(x => x.Id == id);
        if (result is null)
            return new DomainError("Enumeration.Null", $"Cannot get {typeof(T).GetUnproxiedType()} by id '{id}'");

        return result;
    }

    public override int GetHashCode()
    {
        if (cachedHashCode.HasValue) 
            return cachedHashCode.Value;

        cachedHashCode = (typeof(T).GetUnproxiedType().Name.GetHashCode() + Id).GetHashCode();

        return cachedHashCode.Value;
    }

    public int CompareTo(object? other) => Id.CompareTo(((Enumeration<T>)other!).Id);

    private static IReadOnlyCollection<T> CreateEnumerations()
    {
        var enumerationType = typeof(T);

        var enumerationData = enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (T)fieldInfo.GetValue(default)!)
            .ToArray();

        return enumerationData;
    }
}