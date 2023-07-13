using Application.Interfaces.Messaging;

namespace Application.MaterialManagement.MaterialAggregate.Commands.DeleteMaterial;

public sealed record DeleteMaterialCommand(Guid Id) : ITransactionalCommand;
