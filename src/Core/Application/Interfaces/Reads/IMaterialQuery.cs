using Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;

namespace Application.Interfaces.Reads;

public interface IMaterialQuery
{
    Task<MaterialResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaterialsResponse>> GetListAsync(CancellationToken cancellationToken);
}