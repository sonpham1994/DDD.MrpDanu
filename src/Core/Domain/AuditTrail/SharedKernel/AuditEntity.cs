namespace Domain.AuditTrail.SharedKernel;

public abstract class AuditEntity
{
    public Guid Id { get; }

    public string ObjectId { get; }

    public DateTimeOffset TimeGenerated { get; }

    public Guid CreatedUser { get; }

    public StateAuditEntity StateAuditEntity { get; }

    public string? CorrelationId { get; }

    protected AuditEntity(string id, StateAuditEntity state, string? correlationId, DateTimeOffset timeGenerated)
    {
        Id = Guid.NewGuid();
        ObjectId = id;
        StateAuditEntity = state;
        CorrelationId = correlationId;
        TimeGenerated = timeGenerated;
        CreatedUser = Guid.Empty;
    }
}