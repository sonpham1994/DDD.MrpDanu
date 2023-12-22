using System.Reflection;
using Domain.Extensions;
using Domain.SharedKernel.Base;

namespace Infrastructure.Persistence.Writes.Extensions;

internal static class EnumerationExtension
{
    public static Entity BindingEnumeration<TEnumeration>(
        this Entity entity,
        string shadowProperty,
        string propertyName,
        AppDbContext context)
        where TEnumeration : Enumeration<TEnumeration>
    {
        var id = context.Entry(entity).Property<byte>(shadowProperty).CurrentValue;
        var value = Enumeration<TEnumeration>.FromId(id).Value;
        entity.GetUnproxiedType().GetProperty(propertyName)!.SetValue(entity, value, null);

        return entity;
    }

    public static EntityGuidStronglyTypedId<TId> BindingEnumeration<TEnumeration, TId>(
        this EntityGuidStronglyTypedId<TId> entity,
        string shadowProperty,
        string propertyName,
        AppDbContext context)
        where TEnumeration : Enumeration<TEnumeration>
        where TId : struct, IEquatable<TId>, IGuidStronglyTypedId
    {
        var id = context.Entry(entity).Property<byte>(shadowProperty).CurrentValue;
        var value = Enumeration<TEnumeration>.FromId(id).Value;
        entity.GetUnproxiedType().GetProperty(propertyName)!.SetValue(entity, value, null);

        return entity;
    }

    public static ValueObject BindingEnumeration<TEnumeration>(
        this ValueObject valueObject,
        string shadowProperty,
        string propertyName,
        AppDbContext context)
        where TEnumeration : Enumeration<TEnumeration>
    {
        var id = context.Entry(valueObject).Property<byte>(shadowProperty).CurrentValue;
        var value = Enumeration<TEnumeration>.FromId(id).Value;

        var field = valueObject.GetUnproxiedType().GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;
        field.SetValue(valueObject, value);

        return valueObject;
    }
}