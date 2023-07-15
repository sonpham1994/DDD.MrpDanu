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
    private readonly IAuditTableService _auditTableService;

    public TransactionalBehavior(ILogger<TransactionalBehavior<TRequest, TResponse>> logger,
        ITransaction transaction,
        IAuditTableService auditTableService)
    {
        _logger = logger;
        _transaction = transaction;
        _auditTableService = auditTableService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetGenericTypeName();
        _logger.StartTransactionalBehavior(requestName);

        var transactionalHandler = new TransactionalHandler(
            new RequestHandler<TResponse>(next), 
            new AuditTableHandler(_auditTableService));
        var response = await _transaction.HandleAsync(transactionalHandler);

        _logger.CompletedTransactionalBehavior(requestName);

        return (TResponse)response;
    }
}