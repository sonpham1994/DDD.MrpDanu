
using Application.Interfaces.Writes.MaterialWrite;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistence.Writes.MaterialWrite;

// this is for getting data in order to perform write action, the data from repository will be responsible for
// writting data. This method intend to check the code exist in db or not, or some similar business.
// By doing this, we separte read and write side, instead of putting these methods or logics inside the read side
// But the problem is that, if you want to introduce Read db (or Read model), this should be existed at Write db.
// If not, the sync data latency between write and read db would be a problem.
// And another thing is that, by sperating this logic from query side, the write just use methods that need to 
// perform write operation, we can say we achieve Interface Segregation principle and Single Responsibility principle
// And because in Write side, we don't use dapper, so if we use dapper similiar to read side, one day if we separate
// write and read sides on separate layers or assemblies, we need to install dapper nuger package on write side, which 
// we really don't need. So instead of using dapper, we use built-in SqlQuery from EF Core.
internal sealed class MaterialEFQueryForWrite(AppDbContext _context) : IMaterialQueryForWrite
{
    public async Task<MaterialIdWithCode> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        var material = await _context.Database
               .SqlQuery<MaterialIdWithCode>($"""
                    SELECT material.Id, material.Code 
                    FROM Material 
                    WHERE Code = {code}
                    """)
               .FirstOrDefaultAsync(cancellationToken);

        return material;
    }
}
