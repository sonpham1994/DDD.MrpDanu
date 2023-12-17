using System.Reflection;
using Domain.Extensions;
using Domain.SharedKernel.Base;

namespace Infrastructure.Persistence.Write.EfRepositories.Extensions;

internal static class EnumerationExtension
{
    public static void BindingEnumeration<TEnumeration>(
        this Entity entity,
        string shadowProperty,
        string propertyName,
        AppDbContext context)
        where TEnumeration : Enumeration<TEnumeration>
    {
        var id = context.Entry(entity).Property<byte>(shadowProperty).CurrentValue;
        var value = Enumeration<TEnumeration>.FromId(id).Value;
        entity.GetUnproxiedType().GetProperty(propertyName)!.SetValue(entity, value, null);
    }

    public static void BindingEnumeration<TEnumeration>(
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
    }
}