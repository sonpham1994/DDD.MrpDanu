using SharedKernel.Extensions;
using SharedKernel.Exceptions;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Infrastructure.Errors;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;

internal static class EntityAuditFactory
{
    public static EntityAudit Create(EntityEntry entityEntry)
    {
        var type = entityEntry.Entity.GetUnproxiedType();
        var typeName = type.Name;
        var result = typeName switch
        {
            nameof(Material) => new MaterialAudit(),
            _ => throw new DomainException(InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(typeName))
        };

        return result;
    }
}
