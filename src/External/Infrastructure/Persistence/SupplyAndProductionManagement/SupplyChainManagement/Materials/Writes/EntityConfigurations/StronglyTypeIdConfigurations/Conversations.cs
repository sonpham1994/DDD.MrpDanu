using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.Materials.Writes.EntityConfigurations.StronglyTypeIdConfigurations;

internal sealed class MaterialSupplierCostIdConverter : ValueConverter<MaterialSupplierCostId, Guid>
{
    public MaterialSupplierCostIdConverter()
        : base(
            v => v.Value,
            v => (MaterialSupplierCostId)v)
    {
    }
}