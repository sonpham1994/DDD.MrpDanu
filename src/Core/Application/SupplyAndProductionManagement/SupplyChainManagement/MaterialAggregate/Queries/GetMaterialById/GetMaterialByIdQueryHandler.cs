using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using Domain.SupplyChainManagement;
using Application.Interfaces.Reads;

namespace Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;

internal sealed class GetMaterialByIdQueryHandler(IMaterialQuery _materialQuery) : IQueryHandler<GetMaterialByIdQuery, MaterialResponse>
{
    public async Task<Result<MaterialResponse>> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        var material = await _materialQuery.GetByIdAsync(request.Id, cancellationToken);

        if (material is null)
            return DomainErrors.Material.MaterialIdNotFound(request.Id);
        
        return material;
    }
}