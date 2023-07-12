using System.Reflection;
using Domain.SharedKernel.Base;

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

    private static void SetId<TEntity, TDataType>(TEntity entity, TDataType id)
    {
        var idProperty = GetProperty(entity!.GetType(), "Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new Exception("'Id' property cannot be found.");
        idProperty.SetValue(entity, id);
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
}