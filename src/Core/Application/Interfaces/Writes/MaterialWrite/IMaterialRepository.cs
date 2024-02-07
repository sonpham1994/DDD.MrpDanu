using Domain.SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Application.Interfaces.Writes.MaterialWrite;

public interface IMaterialRepository
{
    Task<Material?> GetByIdAsync(MaterialId id, CancellationToken cancellationToken = default);
    void Save(Material material);
    Task DeleteAsync(MaterialId id, CancellationToken cancellationToken);
}