using Application.Interfaces.Messaging;
using Application.Interfaces.Reads;
using Domain.SharedKernel.Base;

namespace Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;

internal sealed class GetTransactionalPartnersQueryHandler(
    ITransactionalPartnerQuery _transactionalPartnerQuery) : IQueryHandler<GetTransactionalPartnersQuery, IReadOnlyList<TransactionalPartnersResponse>>
{
    public async Task<Result<IReadOnlyList<TransactionalPartnersResponse>>> Handle(GetTransactionalPartnersQuery _,
        CancellationToken cancellationToken)
    {
        var result = await _transactionalPartnerQuery.GetTransactionalPartnersAsync(cancellationToken);
        return Result.Success(result);
    }
}