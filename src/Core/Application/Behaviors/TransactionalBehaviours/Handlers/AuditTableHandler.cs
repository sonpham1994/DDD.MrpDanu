using System.Diagnostics;
using Application.Interfaces.Services;
using Application.LoggingDefinitions;
using Domain.SharedKernel.Base;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.TransactionalBehaviours.Handlers;

internal sealed class AuditTableHandler<TResponse> : ITransactionalReceiver<TResponse>
    where TResponse : IResult
{
    private readonly IAuditTableService _auditTableService;
    private readonly ILogger<AuditTableHandler<TResponse>> _logger;

    public AuditTableHandler(IAuditTableService auditTableService, ILogger<AuditTableHandler<TResponse>> logger)
    {
        _auditTableService = auditTableService;
        _logger = logger;
    }

    public async Task<TResponse> HandleAsync()
    {
        _logger.StartLogAuditTable();
        var start = Stopwatch.GetTimestamp();

        IResult result = await _auditTableService.LogChangesAsync();

        var delta = Stopwatch.GetElapsedTime(start);
        _logger.CompletedLogAuditTable(delta.TotalMicroseconds);

        return (TResponse)result;
    }
}