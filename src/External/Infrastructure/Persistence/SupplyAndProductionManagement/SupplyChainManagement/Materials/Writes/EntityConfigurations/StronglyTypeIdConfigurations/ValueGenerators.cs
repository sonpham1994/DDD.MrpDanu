using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Infrastructure.Persistence.Writes.EntityConfigurations;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.Materials.Writes.EntityConfigurations.StronglyTypeIdConfigurations;

internal sealed class MaterialSupplierCostIdValueGenerator : GuidStronglyTypedIdValueGenerator<MaterialSupplierCostId>
{
    protected override MaterialSupplierCostId Next(Guid id)
    {
        return (MaterialSupplierCostId)id;
    }
}