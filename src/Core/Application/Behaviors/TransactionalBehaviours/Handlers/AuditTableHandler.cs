using System.Diagnostics;
using Application.Interfaces.Services;
using Application.LoggingDefinitions;
using Domain.SharedKernel.Base;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.TransactionalBehaviours.Handlers;

internal sealed class AuditTableHandler : ITransactionalReceiver
{
    private readonly IAuditTableService _auditTableService;
    private readonly ILogger<AuditTableHandler> _logger;

    public AuditTableHandler(IAuditTableService auditTableService, ILogger<AuditTableHandler> logger)
    {
        _auditTableService = auditTableService;
        _logger = logger;
    }

    public async Task<IResult> HandleAsync()
    {
        _logger.StartLogAuditTable();
        var start = Stopwatch.GetTimestamp();

        var result = await _auditTableService.LogChangesAsync();

        var delta = Stopwatch.GetElapsedTime(start);
        _logger.CompletedLogAuditTable(delta.TotalMicroseconds);

        return result;
    }
}