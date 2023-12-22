using Domain.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Write.EntityConfigurations.SharedKernel;

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