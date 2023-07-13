using Application.Interfaces.Messaging;
using Application.Interfaces.Queries;
using Domain.SharedKernel.Base;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;

internal sealed class GetTransactionalPartnersQueryHandler : IQueryHandler<GetTransactionalPartnersQuery, IReadOnlyList<TransactionalPartnersResponse>>
{
    private readonly ITransactionalPartnerQuery _transactionalPartnerQuery;

    public GetTransactionalPartnersQueryHandler(ITransactionalPartnerQuery transactionalPartnerQuery)
        => _transactionalPartnerQuery = transactionalPartnerQuery;
    
    public async Task<Result<IReadOnlyList<TransactionalPartnersResponse>>> Handle(GetTransactionalPartnersQuery _,
        CancellationToken cancellationToken)
    {
        var result = await _transactionalPartnerQuery.GetTransactionalPartnersAsync(cancellationToken);
        return Result.Success(result);
    }
}