using Domain.MaterialManagement.MaterialAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.MaterialManagement.StronglyTypeIdConfigurations;

internal sealed class MaterialSupplierCostIdConverter : ValueConverter<MaterialSupplierCostId, Guid>
{
    public MaterialSupplierCostIdConverter()
        : base(
            v => v.Value,
            v => (MaterialSupplierCostId)v)
    {
    }
}