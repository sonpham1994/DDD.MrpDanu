using Application.Interfaces.Messaging;
using SharedKernel.ValueObjects;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;

public sealed record CreateMaterialCommand : ICommand<MaterialId>, ITransactionalCommand
{
    public string Code { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string ColorCode { get; init; } = default!;
    public string Width { get; init; } = default!;
    public string Weight { get; init; } = default!;
    public string Unit { get; init; } = default!;
    public string Varian { get; init; } = default!;
    public byte MaterialTypeId { get; init; }
    public byte RegionalMarketId { get; init; }
    public IReadOnlyList<MaterialSupplierCostCommand> MaterialCosts { get; init; } = new List<MaterialSupplierCostCommand>();
}