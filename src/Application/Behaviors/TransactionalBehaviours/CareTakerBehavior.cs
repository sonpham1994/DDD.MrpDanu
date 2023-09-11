using Application.Behaviors.TransactionalBehaviours.Handlers;
using Application.Interfaces;
using Domain.SharedKernel.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Interfaces.Messaging;
using Application.Extensions;
using Application.LoggingDefinitions;
using Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;

namespace Application.Behaviors.TransactionalBehaviours;

internal sealed class CareTakerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : CreateMaterialCommand
    where TResponse : IResult
{
    private readonly ILogger<CareTakerBehavior<TRequest, TResponse>> _logger;
    private readonly ITransaction _transaction;
    private readonly AuditTableHandler _auditTableHandler;

    public CareTakerBehavior(ILogger<CareTakerBehavior<TRequest, TResponse>> logger,
        ITransaction transaction,
        AuditTableHandler auditTableHandler)
    {
        _logger = logger;
        _transaction = transaction;
        _auditTableHandler = auditTableHandler;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var result = await next();
        if (result is Result<Guid> response)
        {
            
        }

        return (TResponse)response;
    }
}