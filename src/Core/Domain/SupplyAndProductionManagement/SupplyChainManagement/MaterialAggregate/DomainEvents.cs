using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public sealed record MaterialPriceChangedDomainEvent(
    MaterialId MaterialId, 
    SupplierId SupplierId, 
    Money Price) : DomainEvent;