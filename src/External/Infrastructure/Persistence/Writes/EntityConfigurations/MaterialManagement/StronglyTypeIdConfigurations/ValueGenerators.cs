using Domain.SupplyChainManagement.MaterialAggregate;
using Infrastructure.Persistence.Writes.EntityConfigurations.SharedKernel;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.SupplyChainManagement.StronglyTypeIdConfigurations;

internal sealed class MaterialSupplierCostIdValueGenerator : GuidStronglyTypedIdValueGenerator<MaterialSupplierCostId>
{
    protected override MaterialSupplierCostId Next(Guid id)
    {
        return (MaterialSupplierCostId)id;
    }
}