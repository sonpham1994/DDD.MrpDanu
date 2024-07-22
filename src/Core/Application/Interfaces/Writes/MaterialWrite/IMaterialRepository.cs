using SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Application.Interfaces.Writes.MaterialWrite;

public interface IMaterialRepository : IRepository
{
    ValueTask<Material?> GetByIdAsync(MaterialId id, CancellationToken cancellationToken = default);
    void Save(Material material);
    Task DeleteAsync(MaterialId id, CancellationToken cancellationToken);
}