using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Application.MaterialManagement.MaterialAggregate.Commands.DeleteMaterial;

internal sealed class DeleteMaterialCommandHandler : ICommandHandler<DeleteMaterialCommand>, ITransactionalCommandHandler
{
    private readonly IMaterialRepository _materialRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMaterialCommandHandler(IMaterialRepository materialRepository, IUnitOfWork unitOfWork)
    {
        _materialRepository = materialRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        await _materialRepository.DeleteAsync((MaterialId)request.Id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}