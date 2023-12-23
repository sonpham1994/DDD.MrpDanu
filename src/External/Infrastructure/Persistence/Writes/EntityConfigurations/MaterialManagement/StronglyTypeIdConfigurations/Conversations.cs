using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
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

internal sealed class ContactPersonInformationIdConverter : ValueConverter<ContactPersonInformationId, Guid>
{
    public ContactPersonInformationIdConverter()
        : base(
            v => v.Value,
            v => (ContactPersonInformationId)v)
    {
    }
}