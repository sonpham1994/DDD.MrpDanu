using Application.Behaviors.TransactionalBehaviours.Handlers;
using Application.Interfaces;
using SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Interfaces.Messaging;
using Application.Extensions;
using Application.LoggingDefinitions;

namespace Application.Behaviors.TransactionalBehaviours;

internal sealed class TransactionalBehavior<TRequest>(
    ILogger<TransactionalBehavior<TRequest>> logger,
    ITransaction transaction,
    AuditTableHandler auditTableHandler)
    : IPipelineBehavior<TRequest, Result>
    where TRequest : ITransactionalCommand
{
    public async Task<Result> Handle(TRequest request, RequestHandlerDelegate<Result> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetGenericTypeName();
        logger.StartTransactionalBehavior(requestName);

        var transactionalHandler = new TransactionalHandler(
            new RequestHandler(next),
            auditTableHandler);

        //the next handler is DomainEvent or publish message to message broker

        var response = await transaction.HandleAsync(transactionalHandler);

        logger.CompletedTransactionalBehavior(requestName);

        return response;
    }
}