using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement;



internal sealed class MaterialSupplierCostIdConverter : ValueConverter<MaterialSupplierCostId, Guid>
{
    public MaterialSupplierCostIdConverter()
        : base(
            v => v.Value,
            v => new MaterialSupplierCostId(v))
    {
    }
}

internal sealed class ContactPersonInformationIdConverter : ValueConverter<ContactPersonInformationId, Guid>
{
    public ContactPersonInformationIdConverter()
        : base(
            v => v.Value,
            v => new ContactPersonInformationId(v))
    {
    }
}