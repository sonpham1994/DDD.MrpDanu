namespace Domain.AuditTrail.SharedKernel;

public abstract class AuditEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string ObjectId { get; set; } = default!;

    public DateTimeOffset TimeGenerated { get; set; }

    public Guid CreatedUser { get; set; } = Guid.Empty;

    public StateAuditEntity StateAuditEntity { get; set; } =  default!;

    public string? CorrelationId { get; set; }

    protected AuditEntity(StateAuditEntity state, string? correlationId, DateTimeOffset timeGenerated)
    {
        StateAuditEntity = state;
        CorrelationId = correlationId;
        TimeGenerated = timeGenerated;
    }
}