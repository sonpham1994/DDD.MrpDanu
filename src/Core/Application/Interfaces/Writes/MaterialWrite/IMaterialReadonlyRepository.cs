using Domain.Services.UniqueMaterialCodeServices;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Application.Interfaces.Writes.MaterialWrite;

public interface IMaterialReadonlyRepository : IReadonlyRepository
{
    Task<MaterialIdWithCode?> GetByCodeAsync(MaterialCode code, CancellationToken cancellationToken);
}
