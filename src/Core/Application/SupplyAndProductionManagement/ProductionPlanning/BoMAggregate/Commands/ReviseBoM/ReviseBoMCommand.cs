using Application.Interfaces.Messaging;

namespace Application.ProductionPlanning.BoMAggregate.Commands.ReviseBoM;

public sealed record ReviseBoMCommand : ICommand
{
    public IReadOnlyList<BoMMaterialCommand> BoMMaterials = [];
    public string Confirmation { get; init; } = default!;
}

public sealed record BoMMaterialCommand : ICommand
{
    public Guid MaterialId { get; init; }
    public Guid SupplierId { get; init; }
    public decimal Unit { get; init; }
}