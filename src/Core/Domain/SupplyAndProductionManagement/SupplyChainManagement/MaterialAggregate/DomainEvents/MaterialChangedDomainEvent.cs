using Domain.AuditTrail.SharedKernel;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Enumerations;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.DomainEvents;

public sealed record MaterialChangedDomainEvent(
    Guid Id,
    string Code,
    string Name,
    string ColorCode,
    string Width,
    string Weight,
    string Unit,
    string Varian,
    MaterialType MaterialType,
    RegionalMarket RegionalMarket,
    IReadOnlyList<MaterialSupplierCostChangedDomainEvent> MaterialSupplierCosts,
    StateAuditEntity StateAuditEntity) : DomainEventBase;

public sealed record MaterialSupplierCostChangedDomainEvent(
    Guid Id,
    decimal Price,
    uint MinQuantity,
    decimal Surcharge,
    CurrencyType CurrencyType,
    Guid SupplierId) : DomainEventBase;
