using Domain.SharedKernel.Base;

namespace Domain.Extensions;

public static class TypeExtension
{
    public static Type GetUnproxiedType(this Type type)
    {
        const string EFCoreProxySuffix = "Proxy";

        string typeString = type.Name;

        if (typeString.EndsWith(EFCoreProxySuffix))
            return type.BaseType!;

        return type;
    }

    public static Type GetUnproxiedType(this object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        Type type = obj.GetType();

        return GetUnproxiedType(type);
    }
    
    
    //improving comparing performance by not using cast valueobject to object
    // please check benchmarks.benchmark.CastObject. We call GetUnproxiedType() a lot to compare type, so it's a 
    // good choice to introduce the specific type for GetUnproxiedType method to prevent casting behaviour.
    public static Type GetUnproxiedType<T>(this Enumeration<T> obj)
        where T : Enumeration<T>
    {
        ArgumentNullException.ThrowIfNull(obj);

        Type type = obj.GetType();

        return GetUnproxiedType(type);
    }
    
    public static Type GetUnproxiedType<TId>(this Entity<TId> obj)
        where TId : struct, IEquatable<TId>
    {
        ArgumentNullException.ThrowIfNull(obj);

        Type type = obj.GetType();

        return GetUnproxiedType(type);
    }

    public static Type GetUnproxiedType(this Entity obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        Type type = obj.GetType();

        return GetUnproxiedType(type);
    }

    public static Type GetUnproxiedType(this ValueObject obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        Type type = obj.GetType();

        return GetUnproxiedType(type);
    }
}