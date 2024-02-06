using Application.Interfaces.Messaging;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.DeleteMaterial;

public sealed record DeleteMaterialCommand(Guid Id) : ICommand, ITransactionalCommand;
