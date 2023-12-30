using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using Application.Interfaces.Reads;

namespace Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;

internal sealed class GetMaterialsQueryHandler(IMaterialQuery _materialQuery) : IQueryHandler<GetMaterialsQuery, IReadOnlyList<MaterialsResponse>>
{
    public async Task<Result<IReadOnlyList<MaterialsResponse>>> Handle(GetMaterialsQuery _,
        CancellationToken cancellationToken)
    {
        var result =  await _materialQuery.GetListAsync(cancellationToken);
        
        return Result.Success(result);
    }
}