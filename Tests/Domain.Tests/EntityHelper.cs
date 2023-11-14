using System.Reflection;
using Domain.SharedKernel.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Tests;

/*
 * https://stackoverflow.com/questions/21452984/setting-the-identity-of-a-domain-entity
 * https://gist.github.com/ilyapalkin/8711592
 */
public static class EntityHelper
{
    public static TEntity WithId<TEntity>(this TEntity entity, Guid id)
        where TEntity : Entity
    {
        SetId(entity, id);
        return entity;
    }
    
    public static TEntity WithId<TEntity>(this TEntity entity, long id)
        where TEntity : Entity<long>
    {
        SetId(entity, id);
        return entity;
    }

    private static void SetId<TEntity, TDataType>(TEntity entity, TDataType id) where TEntity : class
    {
        var type = entity!.GetType();
        var idProperty = type.GetProperty("Id", BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        if (idProperty is not null)
        {
            idProperty.SetValue(entity, id);
        }
        else
        {
            var field = type.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field is null)
            {
                type = type.BaseType;
                field = type.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                if (field is null)
                {
                    type = type.BaseType.BaseType;
                    field = type.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                }
                if (field is null)
                {
                    type = type.BaseType.BaseType.BaseType;
                    field = type.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                }
            }

            field.SetValue(entity, id);
        }
    }

    private static PropertyInfo GetProperty(Type? type, string propertyName, BindingFlags bindibgAttr)
    {
        PropertyInfo? result = null;

        if (type is not null)
        {
            result = type.GetProperty(propertyName, bindibgAttr);
        }

        return result;
    }

    private static FieldInfo GetField(Type? type, string propertyName, BindingFlags bindibgAttr)
    {
        FieldInfo? result = null;

        if (type is not null)
        {
            result = type.GetField($"<{propertyName}>k__BackingField", bindibgAttr)!;
        }

        return result;
    }
}