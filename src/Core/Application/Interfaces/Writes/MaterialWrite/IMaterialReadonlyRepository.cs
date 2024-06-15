using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

namespace Application.Interfaces.Writes.MaterialWrite;

public interface IMaterialReadonlyRepository : IReadonlyRepository
{
    Task<MaterialIdWithCode?> GetByCodeAsync(MaterialCode code, CancellationToken cancellationToken);
}
