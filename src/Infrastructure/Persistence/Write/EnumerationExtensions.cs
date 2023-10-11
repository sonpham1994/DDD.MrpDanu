using System.Linq.Expressions;
using System.Reflection;
using Domain.SharedKernel.Base;

namespace Infrastructure.Persistence.Write;

internal static class EnumerationExtensions
{
    public static void BindingEnumeration<TEntity, TEnumeration>(
        this TEntity entity,
        string shadowProperty,
        string propertyName,
        AppDbContext context)
        where TEntity : Entity
        where TEnumeration : Enumeration<TEnumeration>
    {
        var id = context.Entry(entity).Property<byte>(shadowProperty).CurrentValue;
        var value = Enumeration<TEnumeration>.FromId(id).Value;
;       typeof(TEntity).GetProperty(propertyName)!.SetValue(entity, value, null);
    }
    
    public static void BindingEnumeration<TValueObject, TEnumeration>(
        this ValueObject valueObject,
        string shadowProperty,
        string propertyName,
        AppDbContext context)
        where TValueObject : ValueObject
        where TEnumeration : Enumeration<TEnumeration>
    {
        var id = context.Entry(valueObject).Property<byte>(shadowProperty).CurrentValue;
        var value = Enumeration<TEnumeration>.FromId(id).Value;
        
        var field = typeof(TValueObject).GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;
        field.SetValue(valueObject, value);
    }
}