using Application.Behaviors.TransactionalBehaviours;
using Application.Interfaces;
using SharedKernel;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

internal sealed class DatabaseTransaction(
    AppDbContext appDbContext, 
    ILogger<DatabaseTransaction> logger)
    : ITransaction
{
    public async Task<Result> HandleAsync(Func<Task<IResult>> handler)
    {
        try
        {
            //https://learn.microsoft.com/en-gb/ef/core/saving/transactions
            var strategy = appDbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await appDbContext.BeginTransactionAsync();
                var result = await handler.Invoke();

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

        return Result.Success();
    }
}