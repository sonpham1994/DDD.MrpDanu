using System.Text.Json.Serialization;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel.Base;
using Infrastructure.Errors;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;

public abstract class EntityAudit
{
    public string Id { get; protected set; }

    [JsonIgnore]
    public string Content { get; protected set; }
    [JsonIgnore]
    public string ObjectName { get; protected set; }

    public abstract Result Serialize(EntityEntry entityEntry);

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
