using Application.Errors;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Exceptions;
using MediatR;

namespace Application.Behaviors.TransactionalBehaviours.Handlers;

internal sealed class RequestHandler<TResponse> : ITransactionalReceiver<TResponse>
    where TResponse : IResult
{
    private readonly RequestHandlerDelegate<TResponse> _handler;

    public RequestHandler(RequestHandlerDelegate<TResponse> handler)
    {
        _handler = handler ?? throw new DomainException(DomainErrors.NullHandler);
    }

    public async Task<TResponse> HandleAsync()
    {
        return await _handler();
    }
}