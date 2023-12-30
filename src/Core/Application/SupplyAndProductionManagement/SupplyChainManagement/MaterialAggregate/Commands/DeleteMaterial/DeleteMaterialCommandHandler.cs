using Application.Interfaces;
using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;
using Application.Interfaces.Writes.MaterialWrite;

namespace Application.SupplyChainManagement.MaterialAggregate.Commands.DeleteMaterial;

internal sealed class DeleteMaterialCommandHandler(
    IMaterialRepository _materialRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<DeleteMaterialCommand>, ITransactionalCommandHandler
{
    public async Task<Result> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        await _materialRepository.DeleteAsync((MaterialId)request.Id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}