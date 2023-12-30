using Domain.SupplyChainManagement.MaterialAggregate;
using Domain.SharedKernel.Base;
using Infrastructure.Errors;
using Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Extensions;
using Domain.SharedKernel.Enumerations;
using Infrastructure.JsonSourceGenerators;

namespace Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit;

public sealed class MaterialAudit : EntityAudit
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string ColorCode { get; private set; }
    public string Width { get; private set; }
    public string Weight { get; private set; }
    public string Unit { get; private set; }
    public string Varian { get; private set; }
    public MaterialType MaterialType { get; private set; }
    public RegionalMarket RegionalMarket { get; private set; }
    public List<MaterialSupplierCostAudit> MaterialSupplierCosts { get; private set; }
    public override Result Serialize(EntityEntry entityEntry)
    {
        var entity = entityEntry.Entity;
        if (entity is not Material material)
            return InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity
                .GetUnproxiedType().Name);

        var id = material.Id.Value.ToString();
        var auditData = new MaterialAudit
        {
            Id = id,
            Code = material.Code,
            Name = material.Name,
            ColorCode = material.Attributes.ColorCode,
            Width = material.Attributes.Width,
            Weight = material.Attributes.Weight,
            Unit = material.Attributes.Unit,
            Varian = material.Attributes.Varian,
            MaterialType = material.MaterialType,
            RegionalMarket = material.RegionalMarket,
            MaterialSupplierCosts = material.MaterialSupplierCosts.Select(x => new MaterialSupplierCostAudit(x)).ToList()
        };
        var json = JsonSerializer.Serialize(auditData, EntityAuditJsonSourceGenerator.Default.MaterialAudit);

        Id = id;
        Content = json;
        ObjectName = nameof(Material);

        return Result.Success();

    }
}

public sealed class MaterialSupplierCostAudit
{
    public Guid Id { get; private set; }
    public decimal Price { get; private set; }
    public uint MinQuantity { get; private set; }
    public decimal Surcharge { get; private set; }
    public CurrencyType CurrencyType { get; private set; }
    public Guid SupplierId { get; private set; }
    public MaterialSupplierCostAudit(MaterialSupplierCost materialSupplierCost)
    {
        Id = materialSupplierCost.Id.Value;
        Price = materialSupplierCost.Price.Value;
        MinQuantity = materialSupplierCost.MinQuantity;
        Surcharge = materialSupplierCost.Surcharge.Value;
        CurrencyType = materialSupplierCost.Price.CurrencyType;
        SupplierId = materialSupplierCost.MaterialSupplierIdentity.SupplierId.Value;
    }
}
