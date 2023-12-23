using Application.Interfaces.Services;
using Domain.SharedKernel.Base;
using Infrastructure.Persistence.LoggingDefinitions;
using Infrastructure.Persistence.Writes;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Externals.AuditTables.Services;

internal sealed class AuditTableService : IAuditTableService
{
    private readonly ExternalDbContext _externalDbContext;
    private readonly AppDbContext _appDbContext;

    public AuditTableService(ExternalDbContext externalDbContext, AppDbContext appDbContext)
    {
        _externalDbContext = externalDbContext;
        _appDbContext = appDbContext;
        
    }

    public async Task<Result> LogChangesAsync()
    {
        var auditTables = _externalDbContext.GetAuditTables();

        if (_appDbContext.HasActiveTransaction && auditTables.Count > 0)
        {
            await _externalDbContext.AuditTables.AddRangeAsync(auditTables);
            await _externalDbContext.SaveChangesAsync();
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