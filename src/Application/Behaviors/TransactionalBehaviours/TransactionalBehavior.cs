using Application.Behaviors.TransactionalBehaviours.Handlers;
using Application.Interfaces;
using Domain.SharedKernel.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Interfaces.Messaging;
using Application.Interfaces.Services;
using Application.Extensions;
using Application.LoggingDefinitions;

namespace Application.Behaviors.TransactionalBehaviours;

internal sealed class TransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionalCommand
    where TResponse : IResult
{
    private readonly ILogger<TransactionalBehavior<TRequest, TResponse>> _logger;
    private readonly ITransaction _transaction;
    private readonly AuditTableHandler _auditTableHandler;

    public TransactionalBehavior(ILogger<TransactionalBehavior<TRequest, TResponse>> logger,
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
        var requestName = request.GetGenericTypeName();
        _logger.StartTransactionalBehavior(requestName);

        var transactionalHandler = new TransactionalHandler(
            new RequestHandler<TResponse>(next), 
            _auditTableHandler);

        //the next handler is DomainEvent or publish message to message broker
        
        var response = await _transaction.HandleAsync(transactionalHandler);

        _logger.CompletedTransactionalBehavior(requestName);

        return (TResponse)response;
    }
}