using Application.Interfaces.Messaging;
using Application.Interfaces.Reads;
using SharedKernel;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;

internal sealed class GetTransactionalPartnersQueryHandler(
    ITransactionalPartnerQuery transactionalPartnerQuery) 
        : IQueryListHandler<GetTransactionalPartnersQuery, TransactionalPartnersResponse>
{
    public async Task<Result<IReadOnlyList<TransactionalPartnersResponse>>> Handle(GetTransactionalPartnersQuery _,
        CancellationToken cancellationToken)
    {
        var result = await transactionalPartnerQuery.GetTransactionalPartnersAsync(cancellationToken);
        return Result.Success(result);
    }
}