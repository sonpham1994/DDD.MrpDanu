using System.Diagnostics;
using Application.Interfaces.Services;
using Application.LoggingDefinitions;
using Domain.SharedKernel.Base;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.TransactionalBehaviours.Handlers;

internal sealed class AuditTableHandler(IAuditTableService auditTableService, ILogger<AuditTableHandler> logger)
    : ITransactionalReceiver
{
    public async Task<IResult> HandleAsync()
    {
        logger.StartLogAuditTable();
        var stopWatch = Stopwatch.StartNew();

        var result = await auditTableService.InsertChangesAsync();
        stopWatch.Stop();
        logger.CompletedLogAuditTable(stopWatch.ElapsedMilliseconds);

        return result;
    }
}