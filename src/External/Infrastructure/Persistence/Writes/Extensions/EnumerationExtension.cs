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

    // EF 7
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

    // .NET 8 or later version with disable lazy loading for removing virtual entities or disable lazyloading for specific entities
    // please check: https://learn.microsoft.com/en-gb/ef/core/what-is-new/ef-core-8.0/whatsnew - "This can be changed in EF8 to opt-in to the classic EF6 behavior such that a navigation can be made to not lazy-load simply by making the navigation non-virtual"
    public static EntityGuidStronglyTypedId<TId> BindingEnumeration<TEnumeration, TId>(
        this EntityGuidStronglyTypedId<TId> entity,
        TEnumeration enumeration,
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