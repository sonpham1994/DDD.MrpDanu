using Application.Helpers;
using Domain.AuditTrail.SupplyChainManagement;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.DomainEvents;

namespace Application.AuditTrail.DomainEventHandlers;

internal static class Extensions
{
    public static MaterialAudit ToEntityAudit(this MaterialChangedDomainEvent domainEvent)
    {
        return new(
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
            domainEvent.StateAuditEntity,
            Helper.GetTraceId(),
            domainEvent.DateOccurred);
    }

    public static MaterialSupplierCostAudit? ToEntityAudit(
        this MaterialSupplierCostChangedDomainEvent? materialSupplierCost)
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
    
    public static List<MaterialSupplierCostAudit> ToEntityAudits(
        this IReadOnlyList<MaterialSupplierCostChangedDomainEvent>? materialSupplierCosts)
    {
        if (materialSupplierCosts is null)
            return new List<MaterialSupplierCostAudit>();

        return materialSupplierCosts
            .Select(x => x.ToEntityAudit())
            .Where(x=> x is not null)
            .ToList()!;
    }
}