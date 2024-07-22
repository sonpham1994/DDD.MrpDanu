using SharedKernel;

namespace Infrastructure.Persistence.AuditTrail;

internal sealed class AuditTrailDomainEvents : HasDomainEventsBase
{
    public void RaiseAuditTrailDomainEvents(DomainEventBase domainEvent) => RaiseDomainEvent(domainEvent);
}