using Application.Interfaces.Messaging;
using Application.Interfaces.Queries;
using Domain.MaterialManagement;
using Domain.SharedKernel.Base;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;

internal sealed class GetTransactionalPartnerByIdQueryHandler : IQueryHandler<GetTransactionalPartnerByIdQuery, TransactionalPartnerResponse>
{
    private readonly ITransactionalPartnerQuery _transactionalPartnerQuery;

    public GetTransactionalPartnerByIdQueryHandler(ITransactionalPartnerQuery transactionalPartnerQuery)
        => _transactionalPartnerQuery = transactionalPartnerQuery;
    
    public async Task<Result<TransactionalPartnerResponse>> Handle(GetTransactionalPartnerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var transactionalPartner = await _transactionalPartnerQuery.GetTransactionalPartnerByIdAsync(request.Id, cancellationToken);

        if (transactionalPartner is null)
            return DomainErrors.TransactionalPartner.NotFoundId(request.Id);

        return transactionalPartner;
    }
}