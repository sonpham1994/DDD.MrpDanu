using Domain.AuditTrail.SharedKernel;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Domain.AuditTrail.SupplyChainManagement;

public sealed class MaterialAudit : AuditEntity
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; }= default!;
    public string ColorCode { get; private set; }= default!;
    public string Width { get; private set; }= default!;
    public string Weight { get; private set; }= default!;
    public string Unit { get; private set; }= default!;
    public string Varian { get; private set; }= default!;
    public MaterialType MaterialType { get; private set; }= default!;
    public RegionalMarket RegionalMarket { get; private set; }= default!;
    public List<MaterialSupplierCostAudit> MaterialSupplierCosts { get; private set; } = default!;

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
        List<MaterialSupplierCostAudit> materialSupplierCosts,
        StateAuditEntity state,
        string? correlationId) : base(state, correlationId)
    {
        ObjectId = id.ToString();
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
    }
}