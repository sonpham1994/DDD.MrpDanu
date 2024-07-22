using Application.Interfaces.Messaging;
using Application.Interfaces.Reads;
using Application.SupplyAndProductionManagement.SupplyChainManagement.Shared;
using SharedKernel;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;

internal sealed class GetSuppliersQueryHandler(
    ITransactionalPartnerQuery transactionalPartnerQuery) : IQueryListHandler<GetSuppliersQuery, SuppliersResponse>
{
    public async Task<Result<IReadOnlyList<SuppliersResponse>>> Handle(GetSuppliersQuery _,
        CancellationToken cancellationToken)
    {
        var result = await transactionalPartnerQuery.GetSuppliersAsync(cancellationToken);

        return Result.Success(result);
    }
}