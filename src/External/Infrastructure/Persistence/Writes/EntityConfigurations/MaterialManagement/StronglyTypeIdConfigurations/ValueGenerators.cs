using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Infrastructure.Persistence.Writes.EntityConfigurations.SharedKernel;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.MaterialManagement.StronglyTypeIdConfigurations;

internal sealed class ContactPersonInformationIdValueGenerator : GuidStronglyTypedIdValueGenerator<ContactPersonInformationId>
{
    protected override ContactPersonInformationId Next(Guid id)
    {
        return (ContactPersonInformationId)id;
    }
}

internal sealed class MaterialSupplierCostIdValueGenerator : GuidStronglyTypedIdValueGenerator<MaterialSupplierCostId>
{
    protected override MaterialSupplierCostId Next(Guid id)
    {
        return (MaterialSupplierCostId)id;
    }
}