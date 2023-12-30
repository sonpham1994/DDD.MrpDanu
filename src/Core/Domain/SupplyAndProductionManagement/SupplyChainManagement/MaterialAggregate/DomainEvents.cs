using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Domain.SupplyChainManagement.MaterialAggregate;

public sealed record MaterialPriceChangedDomainEvent(
    MaterialId MaterialId, 
    SupplierId SupplierId, 
    Money Price) : DomainEvent;