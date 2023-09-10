using Application.Behaviors.TransactionalBehaviours;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.SharedKernel.Base;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

internal sealed class DatabaseTransactionWithMongo : ITransaction
{
    private readonly ILogger<DatabaseTransaction> _logger;
    private readonly IUndoRepository _undoRepositories;

    public DatabaseTransactionWithMongo(ILogger<DatabaseTransaction> logger, IUndoRepository undoRepositories)
    {
        _logger = logger;
        _undoRepositories = undoRepositories;
    }

    public async Task<IResult> HandleAsync(TransactionalHandler transactionalHandler)
    {
        IResult result = null;
        try
        {
            //we can apply db transaction from EF if externalDbContext fail, we call RollBack from AppDbContext.
            //But if externalDbContext is successful, and then we call commit, and it in turn causes AppDbContext failed,
            //How do we rollback ExternalDbContext? -> we can do this by using correlationId
            // if the request handler is complicated and we cannot apply memento to roll back data exactly, we can
            //take advantage of database transaction from EF. For mongo, we just use correlationId to roll back
            result = await transactionalHandler.HandleAsync();
            
            if (result.IsFailure)
                await _undoRepositories.RestoreAsync();
        }
        catch (Exception ex)
        {
            string traceId = Helper.GetTraceId();
            var message = $"{ex.Message}{Environment.NewLine}{ex.InnerException}";
            _logger.LogError(ex, "----- TraceId: {TraceId}, ERROR Handling transaction: {@Message}", traceId, message);

            await _undoRepositories.RestoreAsync();
            
            throw;
        }

        return result;
    }
}