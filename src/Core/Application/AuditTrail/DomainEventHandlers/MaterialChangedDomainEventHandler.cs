using Application.Interfaces.Writes.AuditTrailWrite;
using Domain.AuditTrail.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.DomainEvents;
using MediatR;

namespace Application.AuditTrail.DomainEventHandlers;

public sealed class MaterialChangedDomainEventHandler(IAuditTableWriteRepository<MaterialAudit> auditTableRepository): INotificationHandler<MaterialChangedDomainEvent>
{
    public async Task Handle(MaterialChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        var materialAudit = notification.ToEntityAudit();
        await auditTableRepository.InsertAsync(materialAudit);
    }
}