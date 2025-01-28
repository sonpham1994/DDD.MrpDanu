using Application.Interfaces;
using Application.Interfaces.Messaging;
using SharedKernel;
using SharedKernel.ValueObjects;
using Application.Interfaces.Writes.MaterialWrite;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.DeleteMaterial;

internal sealed class DeleteMaterialCommandHandler(
    IMaterialRepository materialRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteMaterialCommand>
{
    public async Task<Result> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        var result = await materialRepository.DeleteAsync((MaterialId)request.Id, cancellationToken);

        if (!result.IsSuccess)
            return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}