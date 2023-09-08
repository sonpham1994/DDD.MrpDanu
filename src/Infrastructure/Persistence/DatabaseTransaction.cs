using Application.Behaviors.TransactionalBehaviours;
using Application.Helpers;
using Application.Interfaces;
using Domain.SharedKernel.Base;
using Infrastructure.Persistence.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

internal sealed class DatabaseTransaction : ITransaction
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<DatabaseTransaction> _logger;

    public DatabaseTransaction(AppDbContext appDbContext, ILogger<DatabaseTransaction> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }
    
    //public async Task<IResult> HandleAsync(TransactionalHandler transactionalHandler)
    //{
    //    IResult result = null;
    //    try
    //    {
    //        //https://learn.microsoft.com/en-gb/ef/core/saving/transactions
    //        var strategy = _appDbContext.Database.CreateExecutionStrategy();

    //        await strategy.ExecuteAsync(async () =>
    //        {
    //            await using var transaction = await _appDbContext.BeginTransactionAsync();
    //            result = await transactionalHandler.HandleAsync();
                
    //            if (result.IsSuccess)
    //                await _appDbContext.CommitTransactionAsync(transaction);
    //            else 
    //                _appDbContext.RollbackTransaction();
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        string traceId = Helper.GetTraceId();
    //        _appDbContext.RollbackTransaction();
    //        var message = $"{ex.Message}{Environment.NewLine}{ex.InnerException}";
    //        _logger.LogError(ex, "----- TraceId: {TraceId}, ERROR Handling transaction: {@Message}", traceId, message);

    //        throw;
    //    }

    //    return result;
    //}

    public async Task<IResult> HandleAsync(TransactionalHandler transactionalHandler)
    {
        IResult result = null;
        try
        {
            result = await transactionalHandler.HandleAsync();
        }
        catch (Exception ex)
        {
            string traceId = Helper.GetTraceId();
            _appDbContext.RollbackTransaction();
            var message = $"{ex.Message}{Environment.NewLine}{ex.InnerException}";
            _logger.LogError(ex, "----- TraceId: {TraceId}, ERROR Handling transaction: {@Message}", traceId, message);

            throw;
        }

        return result;
    }
}