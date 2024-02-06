using Application.Interfaces.Messaging;
using Application.Interfaces.Reads;
using Application.SupplyAndProductionManagement.SupplyChainManagement.Shared;
using Domain.SharedKernel.Base;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;

internal sealed class GetSuppliersQueryHandler(
    ITransactionalPartnerQuery _transactionalPartnerQuery) : IQueryHandler<GetSuppliersQuery, IReadOnlyList<SuppliersResponse>>
{
    public async Task<Result<IReadOnlyList<SuppliersResponse>>> Handle(GetSuppliersQuery _,
        CancellationToken cancellationToken)
    {
        var result = await _transactionalPartnerQuery.GetSuppliersAsync(cancellationToken);

        return Result.Success(result);
    }
}