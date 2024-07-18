using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using Application.Interfaces.Reads;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;

internal sealed class GetMaterialByIdQueryHandler(IMaterialQuery materialQuery) : IQueryHandler<GetMaterialByIdQuery, MaterialResponse>
{
    public async Task<Result<MaterialResponse?>> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        var material = await materialQuery.GetByIdAsync(request.Id, cancellationToken);

        if (material is null)
            return DomainErrors.Material.NotFoundId(request.Id);

        return material;
    }
}