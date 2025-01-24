using Domain.AuditTrail.SharedKernel;
using SharedKernel;
using SharedKernel.Enumerations;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.DomainEvents;

public sealed record MaterialAuditDomainEvent(
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
    IReadOnlyList<MaterialSupplierCostAuditDomainEvent> MaterialSupplierCosts,
    StateAuditEntity StateAuditEntity) : DomainEventBase;

public sealed record MaterialSupplierCostAuditDomainEvent(
    Guid Id,
    decimal Price,
    uint MinQuantity,
    decimal Surcharge,
    CurrencyType CurrencyType,
    Guid SupplierId) : DomainEventBase;
