using System.Text.Json.Serialization;
using SharedKernel;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
