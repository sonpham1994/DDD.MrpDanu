﻿using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel.Base;
using Infrastructure.Errors;
using Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Extensions;
using Domain.SharedKernel.ValueObjects;

namespace Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit;

internal sealed class MaterialAudit : EntityAudit
{
    public override Result Serialize(EntityEntry entityEntry)
    {
        var entity = entityEntry.Entity;
        if (entity is not Material material)
            return InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity
                .GetUnproxiedType().Name);
        
        var id = material.Id.ToString();
        var auditData = new
        {
            Id = id,
            material.Code,
            material.Name,
            //material.CodeUnique,
            //material.Attributes.Name,
            material.Attributes.ColorCode,
            material.Attributes.Width,
            material.Attributes.Weight,
            material.Attributes.Unit,
            material.Attributes.Varian,
            material.MaterialType,
            material.RegionalMarket,
            MaterialSupplierCosts = material.MaterialSupplierCosts.Select(x => new
            {
                x.Id,
                Price = x.MaterialCost.Price.Value,
                x.MinQuantity,
                Surcharge = x.Surcharge.Value,
                x.MaterialCost.Price.CurrencyType,
                SupplierId = x.MaterialCost.SupplierId.Value
            }).ToList()
        };
        var json = JsonSerializer.Serialize(auditData);

        Id = id;
        Content = json;
        ObjectName = nameof(Material);

        return Result.Success();

    }
}
