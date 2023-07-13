using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;

namespace Application.MaterialManagement.MaterialAggregate.Commands.DeleteMaterial;

internal sealed class DeleteMaterialCommandHandler : ITransactionalCommandHandler<DeleteMaterialCommand>
{
    private readonly IMaterialRepository _materialRepository;

    public DeleteMaterialCommandHandler(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }

    public async Task<Result> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        await _materialRepository.DeleteAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}