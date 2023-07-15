using Application.Interfaces.Services;
using Domain.SharedKernel.Base;

namespace Application.Behaviors.TransactionalBehaviours.Handlers;

internal sealed class AuditTableHandler : ITransactionalReceiver
{
    private readonly IAuditTableService _auditTableService;

    public AuditTableHandler(IAuditTableService auditTableService)
    {
        _auditTableService = auditTableService;
    }

    public async Task<IResult> HandleAsync()
    {
        return await _auditTableService.LogChangesAsync();
    }
}