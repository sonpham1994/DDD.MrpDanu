using Domain.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.SharedKernel;

internal sealed class MaterialIdConverter : ValueConverter<MaterialId, Guid>
{
    public MaterialIdConverter()
        : base(
            v => v.Value,
            v => (MaterialId)v)
    {
    }
}

internal sealed class TransactionalPartnerIdConverter : ValueConverter<TransactionalPartnerId, Guid>
{
    public TransactionalPartnerIdConverter()
        : base(
            v => v.Value,
            v => (TransactionalPartnerId)v)
    {
    }
}

internal sealed class SupplierIdConverter : ValueConverter<SupplierId, Guid>
{
    public SupplierIdConverter()
        : base(
            v => v.Value,
            v => (SupplierId)v)
    {
    }
}

internal sealed class SupplierIdFromTransactionalPartnerIdConverter : ValueConverter<SupplierId, TransactionalPartnerId>
{
    public SupplierIdFromTransactionalPartnerIdConverter()
        : base(
            v => (TransactionalPartnerId)v,
            v => new (v.Value))
    {
    }
}