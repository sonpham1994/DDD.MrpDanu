using Application.Interfaces.Messaging;
using Application.Interfaces.Repositories;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;

internal sealed class DeleteTransactionalPartnerCommandHandler : ICommandHandler<DeleteTransactionalPartnerCommand>
{
    private readonly ITransactionalPartnerRepository _transactionalPartnerRepository;

    public DeleteTransactionalPartnerCommandHandler(ITransactionalPartnerRepository transactionalPartnerRepository)
        => _transactionalPartnerRepository = transactionalPartnerRepository;
    
    public async Task<Result> Handle(DeleteTransactionalPartnerCommand request, CancellationToken cancellationToken)
    {
        await _transactionalPartnerRepository.DeleteAsync(new TransactionalPartnerId(request.Id), cancellationToken);
        return Result.Success();
    }
}