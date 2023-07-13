using Domain.Exceptions;
using Domain.Extensions;
using Domain.MaterialManagement.MaterialAggregate;
using Infrastructure.Errors;
using Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Externals.AuditTables.Factories;

internal static class AuditTableFactory
{
    public static readonly Type[] Entities = new[] { typeof(Material) };

    public static AuditTable Create(EntityEntry entityEntry)
    {
        var state = StateAuditTable.FromEntityState(entityEntry.State);
        if (state.IsFailure)
            throw new DomainException(state.Error);

        var entityAudit = EntityAudit.Create(entityEntry); 
        var serializationResult = entityAudit.Serialize(entityEntry);
        if (serializationResult.IsFailure)
            throw new DomainException(DomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity.GetUnproxiedType().Name));
        
        var result = new AuditTable
        (
            entityAudit.Content,
            entityAudit.Id,
            entityAudit.ObjectName,
            state.Value,
            Guid.Empty
        );

        return result;
    }
}
