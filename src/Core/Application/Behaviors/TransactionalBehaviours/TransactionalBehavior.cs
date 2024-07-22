using Application.Behaviors.TransactionalBehaviours.Handlers;
using Application.Interfaces;
using SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Interfaces.Messaging;
using Application.Extensions;
using Application.LoggingDefinitions;

namespace Application.Behaviors.TransactionalBehaviours;

internal sealed class TransactionalBehavior<TRequest, TResponse>(
    ILogger<TransactionalBehavior<TRequest, TResponse>> logger,
    ITransaction transaction,
    AuditTableHandler auditTableHandler)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionalCommand
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetGenericTypeName();
        logger.StartTransactionalBehavior(requestName);

        var transactionalHandler = new TransactionalHandler(
            new RequestHandler<TResponse>(next),
            auditTableHandler);

        //the next handler is DomainEvent or publish message to message broker

        var response = await transaction.HandleAsync(transactionalHandler);

        logger.CompletedTransactionalBehavior(requestName);

        return (TResponse)response;
    }
}