using Application.Interfaces.Messaging;
using Application.Interfaces.Reads;
using SharedKernel;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;

internal sealed class GetTransactionalPartnerByIdQueryHandler(
    ITransactionalPartnerQuery transactionalPartnerQuery) : IQueryHandler<GetTransactionalPartnerByIdQuery, TransactionalPartnerResponse?>
{
    public async Task<Result<TransactionalPartnerResponse?>> Handle(GetTransactionalPartnerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var transactionalPartner = await transactionalPartnerQuery.GetTransactionalPartnerByIdAsync(request.Id, cancellationToken);

        if (transactionalPartner is null)
            return DomainErrors.TransactionalPartner.NotFoundId(request.Id);

        return transactionalPartner;
    }
}