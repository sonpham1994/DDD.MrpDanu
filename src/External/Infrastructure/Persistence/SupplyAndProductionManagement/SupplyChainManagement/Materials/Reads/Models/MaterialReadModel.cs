using Infrastructure.Persistence.Reads;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.Materials.Reads.Models;

internal sealed record MaterialReadModel : IReadModel
{
    public Guid Id { get; init; }
    public string Code { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string ColorCode { get; init; } = default!;
    public string Width { get; init; } = default!;
    public string Weight { get; init; } = default!;
    public string Unit { get; init; } = default!;
    public string Varian { get; init; } = default!;
    public byte MaterialTypeId { get; init; }
    public byte RegionalMarketId { get; init; }
    public List<MaterialCostReadModel> MaterialCosts { get; set; }  = default!;
}