using SharedKernel.Enumerations;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Domain.AuditTrail.SupplyChainManagement;

public sealed class MaterialSupplierCostAudit
{
    public Guid Id { get; private set; }
    public decimal Price { get; private set; }
    public uint MinQuantity { get; private set; }
    public decimal Surcharge { get; private set; }
    public CurrencyType CurrencyType { get; private set; }
    public Guid SupplierId { get; private set; }
    
    public MaterialSupplierCostAudit(
        Guid id, 
        decimal price, 
        uint minQuantity, 
        decimal surcharge, 
        CurrencyType currencyType, 
        Guid supplierId)
    {
        Id = id;
        Price = price;
        MinQuantity = minQuantity;
        Surcharge = surcharge;
        CurrencyType = currencyType;
        SupplierId = supplierId;
    }
}