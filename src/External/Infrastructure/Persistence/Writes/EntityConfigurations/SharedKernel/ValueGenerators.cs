using Domain.SharedKernel.ValueObjects;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.SharedKernel;

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