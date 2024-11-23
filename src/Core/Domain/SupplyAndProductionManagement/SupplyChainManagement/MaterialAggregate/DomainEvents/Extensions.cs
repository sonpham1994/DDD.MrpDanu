using Domain.AuditTrail.SharedKernel;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.DomainEvents;

public static class Extensions
{
    public static MaterialChangedDomainEvent ToDomainEvent(this Material material, StateAuditEntity state)
        => new(material.Id.Value,
            material.Code.Value,
            material.Name.Value,
            material.Attributes.ColorCode,
            material.Attributes.Width,
            material.Attributes.Weight,
            material.Attributes.Unit,
            material.Attributes.Varian,
            material.MaterialType,
            material.RegionalMarket,
            material.MaterialSupplierCosts.ToDomainEvents(),
            state);
    
    public static MaterialSupplierCostChangedDomainEvent? ToDomainEvent(this MaterialSupplierCost? materialSupplierCost)
        => materialSupplierCost is null 
            ? null 
            : new(materialSupplierCost.Id.Value,
            materialSupplierCost.Price.Value,
            materialSupplierCost.MinQuantity,
            materialSupplierCost.Surcharge.Value,
            materialSupplierCost.Price.CurrencyType,
            materialSupplierCost.MaterialSupplierIdentity.SupplierId.Value);

    public static IReadOnlyList<MaterialSupplierCostChangedDomainEvent> ToDomainEvents(
        this IReadOnlyList<MaterialSupplierCost?>? materialSupplierCosts)
        => materialSupplierCosts is null 
            ? Array.Empty<MaterialSupplierCostChangedDomainEvent>()
            : materialSupplierCosts.Select(x => x.ToDomainEvent())
                .Where(x=> x is not null)
                .ToList()!;
}