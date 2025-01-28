using SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using SharedKernel;

namespace Application.Interfaces.Writes.MaterialWrite;

public interface IMaterialRepository : IRepository
{
    ValueTask<Material?> GetByIdAsync(MaterialId id, CancellationToken cancellationToken = default);
    void Save(Material material);
    Task<Result> DeleteAsync(MaterialId id, CancellationToken cancellationToken);
}