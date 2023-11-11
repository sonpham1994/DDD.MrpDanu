using Application.Interfaces.Messaging;
using Application.Interfaces.Repositories;
using Domain.SharedKernel.Base;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;

internal sealed class DeleteTransactionalPartnerCommandHandler : ICommandHandler<DeleteTransactionalPartnerCommand>
{
    private readonly ITransactionalPartnerRepository _transactionalPartnerRepository;

    public DeleteTransactionalPartnerCommandHandler(ITransactionalPartnerRepository transactionalPartnerRepository)
        => _transactionalPartnerRepository = transactionalPartnerRepository;
    
    public async Task<Result> Handle(DeleteTransactionalPartnerCommand request, CancellationToken cancellationToken)
    {
        await _transactionalPartnerRepository.DeleteAsync(request.Id, cancellationToken);
        return Result.Success();
    }
}