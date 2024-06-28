using System.Text.Json.Serialization;
using Domain.SharedKernel.Exceptions;
using Domain.Extensions;
using Domain.SharedKernel.Base;
using Infrastructure.Errors;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;

internal abstract class EntityAudit
{
    public string Id { get; protected set; } = default!;

    [JsonIgnore]
    public string Content { get; protected set; } = default!;
    [JsonIgnore]
    public string ObjectName { get; protected set; } = default!;

    public abstract Result Serialize(EntityEntry entityEntry);
}
