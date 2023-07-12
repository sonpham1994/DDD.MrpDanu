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
}