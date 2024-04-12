using Domain.SharedKernel.ValueObjects;
using Infrastructure.Persistence.Writes.EntityConfigurations;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SharedKernel;

internal sealed class MaterialIdValueGenerator : GuidStronglyTypedIdValueGenerator<MaterialId>
{
    protected override MaterialId Next(Guid id)
    {
        return (MaterialId)id;
    }
}

internal sealed class TransactionalPartnerIdValueGenerator : GuidStronglyTypedIdValueGenerator<TransactionalPartnerId>
{
    protected override TransactionalPartnerId Next(Guid id)
    {
        return (TransactionalPartnerId)id;
    }
}