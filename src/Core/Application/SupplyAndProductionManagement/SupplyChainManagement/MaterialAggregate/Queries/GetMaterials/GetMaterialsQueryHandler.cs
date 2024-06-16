using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using Application.Interfaces.Reads;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;

internal sealed class GetMaterialsQueryHandler(IMaterialQuery materialQuery) : IQueryListHandler<GetMaterialsQuery, MaterialsResponse>
{
    public async Task<Result<IReadOnlyList<MaterialsResponse>>> Handle(GetMaterialsQuery _,
        CancellationToken cancellationToken)
    {
        var result = await materialQuery.GetListAsync(cancellationToken);

        return Result.Success(result);
    }
}