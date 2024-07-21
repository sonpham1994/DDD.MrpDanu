using Domain.AuditTrail.SharedKernel;
using Domain.SharedKernel.Base;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.DomainEvents;

namespace Infrastructure.Persistence.AuditTrail;

internal static class AuditTrailDomainEventRaising
{
    public static void RaiseAuditTrailDomainEvents(
        IAuditTable auditTable, 
        AuditTrailDomainEvents auditTrailDomainEvents,
        StateAuditEntity state)
    {
        switch (auditTable)
        {
            case Material material:
                auditTrailDomainEvents.RaiseAuditTrailDomainEvents(material.ToDomainEvent(state));
                break;
            default:
                throw new NotImplementedException();
        }
    }
}