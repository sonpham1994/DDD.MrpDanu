using Application.Interfaces.Messaging;

namespace Application.ProductManagement.BoMAggregate.Commands.ReviseBoM;

public sealed record ReviseBoMCommand : ICommand
{
    public IReadOnlyList<BoMMaterialCommand> BoMMaterials = new List<BoMMaterialCommand>();
    public string Confirmation { get; init; }
}

public sealed record BoMMaterialCommand : ICommand
{
    public Guid MaterialId { get; init; }
    public Guid SupplierId { get; init; }
    public decimal Unit { get; init; }
}