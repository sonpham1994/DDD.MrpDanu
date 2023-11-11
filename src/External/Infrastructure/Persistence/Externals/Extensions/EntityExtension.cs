using Domain.Extensions;
using Domain.SharedKernel.Base;

namespace Infrastructure.Persistence.Externals.Extensions;

internal static class EntityExtension
{
    public static bool IsEntity(this object obj)
    {
        var type = obj.GetUnproxiedType();
        bool isAggregateRoot = type.BaseType == typeof(AggregateRoot)
                                || (type.BaseType.IsGenericType
                                    && type.BaseType.GetGenericTypeDefinition() == typeof(AggregateRoot<>));
        bool isEntity = type.BaseType == typeof(Entity)
                        || (type.BaseType.IsGenericType
                            && type.BaseType.GetGenericTypeDefinition() == typeof(Entity<>));

        if (type.BaseType != null && (isAggregateRoot || isEntity))
        {
            return true;
        }

        return false;
    }

    public static string GetEntityId(this object obj)
    {
        var result = string.Empty;

        if (obj.IsEntity())
        {
            if (obj is Entity<Guid>)
            {
                var entity = obj as Entity<Guid>;
                result = entity!.Id.ToString();
            }
            else if (obj is Entity<long>)
            {
                var entity = obj as Entity<long>;
                result = entity!.Id.ToString();
            }
            else if (obj is Entity<int>)
            {
                var entity = obj as Entity<int>;
                result = entity!.Id.ToString();
            }
            else
            {
                var type = obj.GetUnproxiedType();
                var typeName = type.Name;
                var idTypeName = type.GetProperty("Id")!.PropertyType.Name;
                throw new ArgumentException($"Do not support entity Id from {typeName}, with Id type: {idTypeName} yet.");
            }
        }

        return result;
    }
}
