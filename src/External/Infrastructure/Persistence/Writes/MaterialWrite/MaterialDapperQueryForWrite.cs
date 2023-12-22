
using Application.Interfaces.Writes.MaterialWrite;
using Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using System.Data;

namespace Infrastructure.Persistence.Writes.MaterialWrite;

// this is for getting data in order to perform write action, the data from repository will be responsible for
// writting data. This method intend to check the code exist in db or not, or some similar business.
// By doing this, we separte read and write side, instead of putting these methods or logics inside the read side
// But the problem is that, if you want to introduce Read db (or Read model), this should be existed at Write db.
// If not, the sync data latency between write and read db would be a problem.
// And another thing is that, by sperating this logic from query side, the write just use methods that need to 
// perform write operation, we can say we achieve Interface Segregation principle and Single Responsibility principle
internal sealed class MaterialDapperQueryForWrite : IMaterialQueryForWrite
{
    private readonly IDbConnection _dbConnection;
    public MaterialDapperQueryForWrite(IDbConnection dbConnection)
        => _dbConnection = dbConnection;
    public async Task<IReadOnlyList<MaterialIdWithCode>> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        var material = await _dbConnection.GetByCodeAsync(code, cancellationToken);

        return material;
    }
}
