using Application.Interfaces.Messaging;
using SharedKernel;
using Application.Interfaces.Reads;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

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