using Domain.AuditTrail.SharedKernel;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Domain.AuditTrail.SupplyChainManagement;

public sealed class MaterialAudit : IAuditEntity
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string ColorCode { get; private set; }
    public string Width { get; private set; }
    public string Weight { get; private set; }
    public string Unit { get; private set; }
    public string Varian { get; private set; }
    public MaterialType MaterialType { get; private set; }
    public RegionalMarket RegionalMarket { get; private set; }
    public DateTimeOffset DateOccured { get; private set; }
    public IReadOnlyList<MaterialSupplierCostAudit> MaterialSupplierCosts { get; private set; } = Array.Empty<MaterialSupplierCostAudit>();

    public MaterialAudit(
        Guid id,
        string code,
        string name,
        string colorCode,
        string width,
        string weight,
        string unit,
        string varian,
        MaterialType materialType,
        RegionalMarket regionalMarket,
        IReadOnlyList<MaterialSupplierCostAudit> materialSupplierCosts,
        DateTimeOffset dateOccured)
    {
        Id = id;
        Code = code;
        Name = name;
        ColorCode = colorCode;
        Width = width;
        Weight = weight;
        Varian = varian;
        Unit = unit;
        MaterialType = materialType;
        RegionalMarket = regionalMarket;
        MaterialSupplierCosts = materialSupplierCosts;
        DateOccured = dateOccured;
    }
}