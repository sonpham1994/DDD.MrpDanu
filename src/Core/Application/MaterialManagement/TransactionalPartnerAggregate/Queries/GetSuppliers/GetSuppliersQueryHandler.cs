using Application.Interfaces.Messaging;
using Application.Interfaces.Queries;
using Application.MaterialManagement.Shared;
using Domain.SharedKernel.Base;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;

internal sealed class GetSuppliersQueryHandler : IQueryHandler<GetSuppliersQuery, IReadOnlyList<SuppliersResponse>>
{
    private readonly ITransactionalPartnerQuery _transactionalPartnerQuery;

    public GetSuppliersQueryHandler(ITransactionalPartnerQuery transactionalPartnerQuery)
       => _transactionalPartnerQuery = transactionalPartnerQuery;
    
    public async Task<Result<IReadOnlyList<SuppliersResponse>>> Handle(GetSuppliersQuery _,
        CancellationToken cancellationToken)
    {
        var result = await _transactionalPartnerQuery.GetSuppliersAsync(cancellationToken);

        return Result.Success(result);
    }
}