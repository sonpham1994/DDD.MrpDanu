using Application.Interfaces.Messaging;
using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;

internal sealed class DeleteTransactionalPartnerCommandHandler(
    ITransactionalPartnerRepository _transactionalPartnerRepository) : ICommandHandler<DeleteTransactionalPartnerCommand>
{
    public async Task<Result> Handle(DeleteTransactionalPartnerCommand request, CancellationToken cancellationToken)
    {
        await _transactionalPartnerRepository.DeleteAsync((TransactionalPartnerId)request.Id, cancellationToken);
        return Result.Success();
    }
}