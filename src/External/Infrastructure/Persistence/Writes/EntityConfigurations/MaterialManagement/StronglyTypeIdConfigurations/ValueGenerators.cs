using Domain.MaterialManagement.MaterialAggregate;
using Infrastructure.Persistence.Writes.EntityConfigurations.SharedKernel;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.MaterialManagement.StronglyTypeIdConfigurations;

internal sealed class MaterialSupplierCostIdValueGenerator : GuidStronglyTypedIdValueGenerator<MaterialSupplierCostId>
{
    protected override MaterialSupplierCostId Next(Guid id)
    {
        return (MaterialSupplierCostId)id;
    }
}