using SharedKernel;
using SharedKernel.ValueObjects;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public sealed record MaterialPriceChangedDomainEvent(
    MaterialId MaterialId, 
    SupplierId SupplierId, 
    Money Price) : DomainEventBase;