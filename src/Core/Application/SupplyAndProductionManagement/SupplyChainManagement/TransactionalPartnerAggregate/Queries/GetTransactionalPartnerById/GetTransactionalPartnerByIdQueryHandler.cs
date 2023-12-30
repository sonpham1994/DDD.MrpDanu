using Application.Interfaces.Messaging;
using Application.Interfaces.Reads;
using Domain.SupplyChainManagement;
using Domain.SharedKernel.Base;

namespace Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;

internal sealed class GetTransactionalPartnerByIdQueryHandler(
    ITransactionalPartnerQuery _transactionalPartnerQuery) : IQueryHandler<GetTransactionalPartnerByIdQuery, TransactionalPartnerResponse>
{
    public async Task<Result<TransactionalPartnerResponse>> Handle(GetTransactionalPartnerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var transactionalPartner = await _transactionalPartnerQuery.GetTransactionalPartnerByIdAsync(request.Id, cancellationToken);

        if (transactionalPartner is null)
            return DomainErrors.TransactionalPartner.NotFoundId(request.Id);

        return transactionalPartner;
    }
}