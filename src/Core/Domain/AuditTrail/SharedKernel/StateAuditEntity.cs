using Domain.SharedKernel.Base;

namespace Domain.AuditTrail.SharedKernel;

public class StateAuditEntity : Enumeration<StateAuditEntity>
{
    private StateAuditEntity(byte id, string name) : base(id, name)
    {
    }

    public static readonly StateAuditEntity None = new(0, nameof(None));
    public static readonly StateAuditEntity Added = new(1, nameof(Added));
    public static readonly StateAuditEntity Modified = new(2, nameof(Modified));
    public static readonly StateAuditEntity Deleted = new(3, nameof(Deleted));

    private StateAuditEntity() { }
}