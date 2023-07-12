using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel.Base;
using Infrastructure.Errors;
using Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit;

internal sealed class MaterialAudit : EntityAudit
{
    public override Result Serialize(EntityEntry entityEntry)
    {
        var entity = entityEntry.Entity;
        if (entity is Material material)
        {
            var id = material.Id.ToString();
            var auditData = new
            {
                Id = id,
                material.Code,
                material.CodeUnique,
                material.Attributes.Name,
                material.Attributes.ColorCode,
                material.Attributes.Width,
                material.Attributes.Weight,
                material.Attributes.Unit,
                material.Attributes.Varian,
                material.MaterialType,
                material.RegionalMarket,
                MaterialCostManagements = material.MaterialCostManagements.Select(x => new
                {
                    x.Id,
                    Price = x.Price.Value,
                    x.MinQuantity,
                    Surcharge = x.Surcharge.Value,
                    x.Price.CurrencyType,
                    Supplier = new
                    {
                        x.TransactionalPartner.Id, // this one will cause performance issue due to Lazy loading
                        Name = x.TransactionalPartner.Name.Value
                    }
                }).ToList()
            };
            var json = JsonSerializer.Serialize(auditData);

            Id = id;
            Content = json;
            ObjectName = nameof(Material);

            return Result.Success();
        }

        return DomainErrors.AuditData.Empty;
    }
}
