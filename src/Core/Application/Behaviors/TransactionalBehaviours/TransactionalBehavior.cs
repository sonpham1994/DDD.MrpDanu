using Application.Interfaces;
using SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Interfaces.Messaging;
using Application.Extensions;
using Application.Interfaces.Services;
using Application.LoggingDefinitions;

namespace Application.Behaviors.TransactionalBehaviours;

internal sealed class TransactionalBehavior<TRequest, TResponse>(
    ILogger<TransactionalBehavior<TRequest, TResponse>> logger,
    ITransaction transaction,
    IAuditTableService auditTableService)
    : ITransactionalCommandHandler<TRequest, TResponse>
    where TRequest : ITransactionalCommand
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetGenericTypeName();
        logger.StartTransactionalBehavior(requestName);
        IResult result = Result.Success();
        
        await transaction.HandleAsync(async () =>
        {
            result = await next();
            await auditTableService.InsertChangesAsync();
            
            return Result.Success();
        });

        //the next handler is DomainEvent or publish message to message broker

        logger.CompletedTransactionalBehavior(requestName);

        return (TResponse)result;
    }
}