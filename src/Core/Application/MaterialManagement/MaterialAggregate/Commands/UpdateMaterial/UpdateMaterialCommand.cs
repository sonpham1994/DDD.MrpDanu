using Application.Interfaces.Messaging;

namespace Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;

public sealed record UpdateMaterialCommand : ICommand, ITransactionalCommand
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public string Name { get; init; }
    public string ColorCode { get; init; }
    public string Width { get; init; }
    public string Weight { get; init; }
    public string Unit { get; init; }
    public string Varian { get; init; }
    public byte MaterialTypeId { get; init; }
    public byte RegionalMarketId { get; init; }
    public IReadOnlyList<MaterialSupplierCostCommand> MaterialCosts { get; init; } = new List<MaterialSupplierCostCommand>();
}