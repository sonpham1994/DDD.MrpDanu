﻿using System.Reflection;
using Domain.Extensions;

namespace Domain.SharedKernel.Base;

public abstract class Enumeration<T> : IComparable<Enumeration<T>>, IEquatable<Enumeration<T>>
    where T : Enumeration<T>
{
    private int? cachedHashCode;
    protected static readonly T[] list = CreateEnumerations();
    public static IReadOnlyCollection<T> List => list;

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
        
        var typeMatches = this.GetUnproxiedType() == otherValue.GetUnproxiedType();
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }
    
    public static bool operator ==(Enumeration<T> a, Enumeration<T> b) => a.Equals(b);

    public static bool operator !=(Enumeration<T> a, Enumeration<T> b) => !(a == b);

    public static Result<T> FromId(in byte id)
    {
        int index = id - 1;
        if (index < 0 || index >= list.Length)
            return new DomainError("Enumeration.Null", $"Cannot get {typeof(T).Name} by id '{id}'");

        //using index is much faster than FirstOrDefault or Find and optimize memory usage. Please check Benchmark/FirstOrDefaultVsFind
        var result = list[index];

        return result;
    }

    public override int GetHashCode()
    {
        if (cachedHashCode.HasValue) 
            return cachedHashCode.Value;

        cachedHashCode = (this.GetUnproxiedType().Name.GetHashCode() + Id).GetHashCode();

        return cachedHashCode.Value;
    }

    public int CompareTo(Enumeration<T>? other) => Id.CompareTo(other!.Id);

    private static T[] CreateEnumerations()
    {
        var enumerationType = typeof(T);

        var enumerationData = enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (T)fieldInfo.GetValue(default)!)
            .Order()
            .ToArray();

        return enumerationData;
    }
}