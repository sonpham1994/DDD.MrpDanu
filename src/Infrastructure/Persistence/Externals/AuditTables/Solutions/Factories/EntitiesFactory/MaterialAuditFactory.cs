using System;
using System.Linq;
using Infrastructure.Errors;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Extensions;
using Domain.SharedKernel.Base;
using Domain.MaterialManagement.MaterialAggregate;
using System.Text.Json;
using Infrastructure.Persistence.Externals.AuditTables.Solutions.Entities;

namespace Infrastructure.Persistence.Externals.AuditTables.Solutions.Factories.EntitiesFactory;

internal sealed class MaterialAuditFactory
{
    public static Result<AuditTable> CreateForSolution1(EntityEntry entityEntry)
    {
        AuditTable? auditTable = null;
        var state = StateAuditTable.FromEntityState(entityEntry.State);
        if (state.IsFailure)
            return state.Error;

        if (entityEntry.Entity is Material material)
        {
            var id = material.Id.ToString();
            var auditData = new
            {
                material.Id,
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
                        Name = x.TransactionalPartner.Name.Value,
                    }

                }).ToList()
            };
            var json = JsonSerializer.Serialize(auditData);

            auditTable = new AuditTable
            (
               json,
               id,
               nameof(Material),
               state.Value,
               Guid.Empty
            );
        }
        else
        {
            return DomainErrors.AuditData.Empty;
        }

        return auditTable;
    }

    public static Result<AuditTable> CreateForSolution2(EntityEntry entityEntry)
    {
        var auditType = typeof(MaterialAudit);
        var isMaterialAuditType = auditType.IsSubclassOf(entityEntry.Entity.GetUnproxiedType());
        if (!isMaterialAuditType)
            return DomainErrors.AuditData.Empty;

        AuditTable auditTable = null;
        var state = StateAuditTable.FromEntityState(entityEntry.State);
        if (state.IsFailure)
            return state.Error;

        if (entityEntry.Entity is Material material)
        {
            var materialAudit = new MaterialAudit(material);
            var (id, obj) = materialAudit.Serialize();

            auditTable = new AuditTable
           (
               obj,
               id,
               nameof(Material),
               state.Value,
               Guid.Empty
           );
        }
        else
        {
            return DomainErrors.AuditData.Empty;
        }

        return auditTable;
    }

    public static Result<AuditTable> CreateForSolution3(EntityEntry entityEntry)
    {
        AuditTable auditTable = null;
        var state = StateAuditTable.FromEntityState(entityEntry.State);
        if (state.IsFailure)
            return state.Error;

        if (entityEntry.Entity is MaterialForLutionAudit material)
        {
            var (id, obj) = material.Serialize();

            auditTable = new AuditTable
            (
               obj,
               id,
               nameof(Material),
               state.Value,
               Guid.Empty
            );
        }
        else
        {
            return DomainErrors.AuditData.Empty;
        }

        return auditTable;
    }
}
