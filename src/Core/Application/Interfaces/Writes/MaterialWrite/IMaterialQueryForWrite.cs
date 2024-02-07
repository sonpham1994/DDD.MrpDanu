using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

namespace Application.Interfaces.Writes.MaterialWrite;

public interface IMaterialQueryForWrite
{
    Task<MaterialIdWithCode> GetByCodeAsync(string code, CancellationToken cancellationToken);
}
