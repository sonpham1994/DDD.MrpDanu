using SharedKernel.Extensions;
using SharedKernel;
using SharedKernel.Exceptions;
using Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Externals.AuditTables;

internal static class Extensions
{
    //we don't use like this: List<Type> Entities = [typeof(Material)]. Because the default behavior of List will create 4 items in array. 
    //public static readonly IReadOnlySet<Type> Entities = new HashSet<Type>()
    //{ 
    //    typeof(Material) 
    //};
    // encapsulate this array. Because we might change to another data structure for optimizing performance. For ex previously we use List<T>,
    // now we use HashSet and the checking exist will be different based on data structrure
    private static readonly HashSet<Type> _entities = CreateAuditTables();

    public static AuditTable CreateAuditTable(this EntityEntry entityEntry)
    {
        var state = StateAuditTable.FromEntityState(entityEntry);
        if (!state.IsSuccess)
            throw new DomainException(state.Error);

        var entityAudit = EntityAuditFactory.Create(entityEntry);
        var serializationResult = entityAudit.Serialize(entityEntry);
        if (!serializationResult.IsSuccess)
            throw new DomainException(serializationResult.Error);

        var result = new AuditTable
        (
            entityAudit.Content,
            entityAudit.Id,
            entityAudit.ObjectName,
            state.Value!,
            Guid.Empty
        );

        return result;
    }

    public static bool IsEntityAudit(this object obj)
    {
        var type = obj.GetUnproxiedType();
        return _entities.Contains(type);
    }

    private static HashSet<Type> CreateAuditTables()
    {
        var interfaceType = typeof(IAuditTable);
        var types = Domain.DomainAssembly.Instance.GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass)
            .ToHashSet();

        return types;
    }
}
