using Application.Helpers;
using Domain.AuditTrail.SharedKernel;
using Domain.AuditTrail.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.DomainEvents;

namespace Application.AuditTrail.DomainEventHandlers;

internal static class Extensions
{
    public static AuditEntity<MaterialAudit>? ToEntityAudit(this MaterialAuditDomainEvent? domainEvent)
    {
        if (domainEvent is null)
            return null;
            
        var materialAudit = new MaterialAudit(
            domainEvent.Id,
            domainEvent.Code,
            domainEvent.Name,
            domainEvent.ColorCode,
            domainEvent.Width,
            domainEvent.Weight,
            domainEvent.Unit,
            domainEvent.Varian,
            domainEvent.MaterialType,
            domainEvent.RegionalMarket,
            domainEvent.MaterialSupplierCosts.ToEntityAudits(),
            domainEvent.DateOccurred);

        return AuditEntity<MaterialAudit>.Create(
            materialAudit.Id.ToString(), 
            domainEvent.StateAuditEntity, 
            Helper.GetTraceId(),
            materialAudit);
    }

    public static MaterialSupplierCostAudit? ToEntityAudit(
        this MaterialSupplierCostAuditDomainEvent? materialSupplierCost)
    {
        if (materialSupplierCost is null)
            return null;

        return new(
            materialSupplierCost.Id, 
            materialSupplierCost.Price, 
            materialSupplierCost.MinQuantity,
            materialSupplierCost.Surcharge, 
            materialSupplierCost.CurrencyType, 
            materialSupplierCost.SupplierId);
    }
    
    public static IReadOnlyList<MaterialSupplierCostAudit> ToEntityAudits(
        this IReadOnlyList<MaterialSupplierCostAuditDomainEvent>? materialSupplierCosts)
    {
        if (materialSupplierCosts is null)
            return Array.Empty<MaterialSupplierCostAudit>();

        return materialSupplierCosts
            .Select(x => x.ToEntityAudit())
            .Where(x => x is not null)
            .ToArray()!;
    }
}