using Application.Helpers;
using Application.Interfaces.Services;
using Domain.SharedKernel.Base;
using Infrastructure.Persistence.LoggingDefinitions;
using Infrastructure.Persistence.Write;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Externals.AuditTables.Services;

internal sealed class AuditTableWithMongoDbService : IAuditTableService
{
    private readonly ExternalDbContext _externalDbContext;
    private readonly AppDbContext _appDbContext;

    public AuditTableWithMongoDbService(ExternalDbContext externalDbContext, AppDbContext appDbContext)
    {
        _externalDbContext = externalDbContext;
        _appDbContext = appDbContext;
        
    }

    public async Task<Result> LogChangesAsync()
    {
        try
        {
            var auditTables = _externalDbContext.GetAuditTables();

            if (_appDbContext.HasActiveTransaction && auditTables.Count > 0)
            {
                await _externalDbContext.AuditTables.AddRangeAsync(auditTables);
                await _externalDbContext.SaveChangesAsync();
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            var correlationId = Helper.GetTraceId();
            //delete records in audit by correlationId
            throw;
        }

        
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