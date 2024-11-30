using Application.Behaviors.TransactionalBehaviours;
using Application.Interfaces;
using SharedKernel;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

internal sealed class DatabaseTransaction(AppDbContext appDbContext, ILogger<DatabaseTransaction> logger)
    : ITransaction
{
    public async Task<Result> HandleAsync(TransactionalHandler transactionalHandler)
    {
        var result = Result.Success();
        try
        {
            //https://learn.microsoft.com/en-gb/ef/core/saving/transactions
            var strategy = appDbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await appDbContext.BeginTransactionAsync();
                result = await transactionalHandler.HandleAsync();

                if (result.IsSuccess)
                    await appDbContext.CommitTransactionAsync(transaction);
                else
                    appDbContext.RollbackTransaction();
            });
        }
        catch (Exception ex)
        {
            appDbContext.RollbackTransaction();
            var message = $"{ex.Message}{Environment.NewLine}{ex.InnerException}";
            logger.LogError(ex, "ERROR Handling transaction: {@Message}", message);
            
            throw;
        }

        return result;
    }

    //public async Task<IResult> HandleAsync(TransactionalHandler transactionalHandler)
    //{
    //    IResult result = null;
    //    try
    //    {
    //        result = await transactionalHandler.HandleAsync();
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
}