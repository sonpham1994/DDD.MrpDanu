namespace Domain.AuditTrail.SharedKernel;

public interface IAuditEntity
{
    DateTimeOffset DateOccured { get; }
}

public class AuditEntity<TAuditEntity> : IAuditEntity
    where TAuditEntity : IAuditEntity
{
    public Guid Id { get; private set; }

    public string ObjectId { get; private set; } = default!;

    public DateTimeOffset DateOccured { get; private set; } = default!;

    public Guid CreatedUser { get; private set; }

    public StateAuditEntity StateAuditEntity { get; private set; } = default!;

    public string? CorrelationId { get; private set; }

    public TAuditEntity Object { get; private set; } = default!;
    
    public string ObjectType { get; private set; } = default!;
    
    public static AuditEntity<TAuditEntity> Create(
        string id,
        StateAuditEntity state, 
        string? correlationId, 
        TAuditEntity entity)
    {
        return new AuditEntity<TAuditEntity>
        {
            Id = Guid.NewGuid(),
            ObjectId = id,
            StateAuditEntity = state,
            CorrelationId = correlationId,
            DateOccured = DateTimeOffset.UtcNow,
            CreatedUser = Guid.Empty,
            Object = entity,
            ObjectType = entity.GetType().Name
        };
    }
}