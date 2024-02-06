using Domain.SharedKernel.Base;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public record struct MaterialSupplierCostId(Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator MaterialSupplierCostId(Guid value)
        => new(value);
}