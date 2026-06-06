using SharedKernel;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public readonly record struct MaterialSupplierCostId(in Guid Value) : IGuidStronglyTypedId
{
    public static explicit operator MaterialSupplierCostId(in Guid value)
        => new(value);
}