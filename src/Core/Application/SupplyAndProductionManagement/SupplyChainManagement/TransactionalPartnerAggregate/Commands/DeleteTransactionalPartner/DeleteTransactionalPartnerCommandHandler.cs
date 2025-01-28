using Application.Interfaces.Messaging;
using Application.Interfaces.Writes.TransactionalPartnerWrite;
using SharedKernel;
using SharedKernel.ValueObjects;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;

internal sealed class DeleteTransactionalPartnerCommandHandler(
    ITransactionalPartnerRepository transactionalPartnerRepository,
    ITransactionalPartnerReadonlyRepository transactionalPartnerReadonlyRepository) : ICommandHandler<DeleteTransactionalPartnerCommand>
{
    public async Task<Result> Handle(DeleteTransactionalPartnerCommand request, CancellationToken cancellationToken)
    {
        var exists = await transactionalPartnerReadonlyRepository.ExistTransactionalPartnerByIdAsync(request.Id, cancellationToken);
        if (!exists)
            return DomainErrors.TransactionalPartner.NotFoundId(request.Id);
        
        await transactionalPartnerRepository.DeleteAsync((TransactionalPartnerId)request.Id, cancellationToken);
        return Result.Success();
    }
}