using Application.Interfaces.Messaging;
using Domain.Errors;
using Domain.SharedKernel.Base;
using Application.Interfaces.Queries;

namespace Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;

internal sealed class GetMaterialByIdQueryHandler : IQueryHandler<GetMaterialByIdQuery, MaterialResponse>
{
    private readonly IMaterialQuery _materialQuery;

    public GetMaterialByIdQueryHandler(IMaterialQuery materialQuery)
        => _materialQuery = materialQuery;

    public async Task<Result<MaterialResponse>> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        var material = await _materialQuery.GetByIdAsync(request.Id, cancellationToken);

        if (material is null)
            return DomainErrors.Material.MaterialIdNotFound(request.Id);
        
        return material;
    }
}