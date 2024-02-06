using Domain.SharedKernel.Exceptions;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Externals.AuditTables.Factories;

internal static class AuditTableFactory
{
    public static readonly Type[] Entities = new[] { typeof(Material) };

    public static AuditTable Create(EntityEntry entityEntry)
    {
        var state = StateAuditTable.FromEntityState(entityEntry);
        if (state.IsFailure)
            throw new DomainException(state.Error);

        var entityAudit = EntityAudit.Create(entityEntry); 
        var serializationResult = entityAudit.Serialize(entityEntry);
        if (serializationResult.IsFailure)
            throw new DomainException(serializationResult.Error);
        
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
