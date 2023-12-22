using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using Application.Interfaces.Reads;

namespace Application.MaterialManagement.MaterialAggregate.Queries.GetMaterials;

internal sealed class GetMaterialsQueryHandler : IQueryHandler<GetMaterialsQuery, IReadOnlyList<MaterialsResponse>>
{
    private readonly IMaterialQuery _materialQuery;

    public GetMaterialsQueryHandler(IMaterialQuery materialQuery)
        => _materialQuery = materialQuery;
    
    public async Task<Result<IReadOnlyList<MaterialsResponse>>> Handle(GetMaterialsQuery _,
        CancellationToken cancellationToken)
    {
        var result =  await _materialQuery.GetListAsync(cancellationToken);
        
        return Result.Success(result);
    }
}