using Application.Interfaces.Services;
using Domain.SharedKernel.Base;
using Infrastructure.Persistence.Writes;

namespace Infrastructure.Persistence.Externals.AuditTables.Services;

internal sealed class AuditTableService(
    ExternalDbContext externalDbContext, 
    AppDbContext appDbContext)
    : IAuditTableService
{
    public async Task<Result> InsertChangesAsync()
    {
        var auditTables = externalDbContext.GetAuditTables();

        if (appDbContext.HasActiveTransaction && auditTables.Count > 0)
        {
            await externalDbContext.AuditTables.AddRangeAsync(auditTables);
            await externalDbContext.SaveChangesAsync();
        }

        return Result.Success();
    }

    //public async Task LogChangesForSolution1Async()
    //{
    //    var auditTables = _externalDbContext.AuditDataForSolution1;

    //    if (_appDbContext.HasActiveTransaction && auditTables.Count > 0)
    //    {
    //        await _externalDbContext.AuditTables.AddRangeAsync(auditTables);
    //        await _externalDbContext.SaveChangesAsync();
    //    }
    //}


    //public async Task LogChangesForSolution2_1Async()
    //{
    //    var auditTables = _externalDbContext.AuditDataForSolution2_1;

    //    if (_appDbContext.HasActiveTransaction && auditTables.Count > 0)
    //    {
    //        await _externalDbContext.AuditTables.AddRangeAsync(auditTables);
    //        await _externalDbContext.SaveChangesAsync();
    //    }
    //}

    //public async Task LogChangesForSolution2Async()
    //{
    //    var auditTables = _externalDbContext.AuditDataForSolution2;

    //    if (_appDbContext.HasActiveTransaction && auditTables.Count > 0)
    //    {
    //        await _externalDbContext.AuditTables.AddRangeAsync(auditTables);
    //        await _externalDbContext.SaveChangesAsync();
    //    }
    //}

    //public async Task LogChangesForSolution3Async()
    //{
    //    var auditTables = _externalDbContext.AuditDataForSolution3;

    //    if (_appDbContext.HasActiveTransaction && auditTables.Count > 0)
    //    {
    //        await _externalDbContext.AuditTables.AddRangeAsync(auditTables);
    //        await _externalDbContext.SaveChangesAsync();
    //    }
    //}
}