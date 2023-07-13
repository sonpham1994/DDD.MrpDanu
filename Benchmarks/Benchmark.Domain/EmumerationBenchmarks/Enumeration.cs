using System.Reflection;

namespace Benchmark.Domain.EmumerationBenchmarks;

public abstract class Enumeration<T> : IComparable
    where T : Enumeration<T>
{
    public static readonly IReadOnlyCollection<T> List = CreateEnumerations();

    public byte Id { get; }
    public string Name { get; }

    protected Enumeration() { }

    protected Enumeration(byte id, string name) => (Id, Name) = (id, name);

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration<T> otherValue)
        {
            return false;
        }

        var typeMatches = this.GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public static bool operator ==(Enumeration<T> a, Enumeration<T> b) => a.Equals(b);

    public static bool operator !=(Enumeration<T> a, Enumeration<T> b) => !(a == b);

    public static T FromId(byte id)
    {
        var result = List.FirstOrDefault(x => x.Id == id);

        return result;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public int CompareTo(object? other) => Id.CompareTo(((Enumeration<T>)other).Id);

    public static IReadOnlyCollection<T> CreateEnumerations()
    {
        var enumerationType = typeof(T);

        var fieldForType = enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (T)fieldInfo.GetValue(default)!)
            .ToList();

        return fieldForType;
    }

    public static IReadOnlyCollection<T> CreateEnumerationsWithToArray()
    {
        var enumerationType = typeof(T);

        var fieldForType = enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (T)fieldInfo.GetValue(default)!)
            .ToArray();

        return fieldForType;
    }

    public static IReadOnlyCollection<T> CreateEnumerationsWithToFixedLengthArray()
    {
        var enumerationType = typeof(T);

        var fieldForType = enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (T)fieldInfo.GetValue(default)!)
            .ToList();

        var array = new T[fieldForType.Count];
        
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = fieldForType[i];
        }

        return array;
    }

    public static IReadOnlyCollection<T> CreateEnumerationsWithToArrayAndCopyTo()
    {
        var enumerationType = typeof(T);

        var fieldForType = enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (T)fieldInfo.GetValue(default)!)
            .ToList();

        var array = new T[fieldForType.Count];
        fieldForType.CopyTo(array, 0);
        
        return array;
    }
}