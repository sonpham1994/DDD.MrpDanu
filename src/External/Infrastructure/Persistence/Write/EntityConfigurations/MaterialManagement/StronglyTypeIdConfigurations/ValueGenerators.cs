using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Infrastructure.Persistence.Write.EntityConfigurations.SharedKernel;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement.StronglyTypeIdConfigurations;

internal sealed class ContactPersonInformationIdValueGenerator : GuidStronglyTypedIdValueGenerator<ContactPersonInformationId>
{
    protected override ContactPersonInformationId Next(Guid id)
    {
        return new ContactPersonInformationId(id);
    }
}

internal sealed class MaterialSupplierCostIdValueGenerator : GuidStronglyTypedIdValueGenerator<MaterialSupplierCostId>
{
    protected override MaterialSupplierCostId Next(Guid id)
    {
        return new MaterialSupplierCostId(id);
    }
}