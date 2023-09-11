using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using MediatR;

namespace Application.Behaviors.TransactionalBehaviours.Handlers;

internal sealed class RequestHandler<TResponse> : ITransactionalReceiver
    where TResponse : IResult
{
    private readonly RequestHandlerDelegate<TResponse> _handler;

    public RequestHandler(RequestHandlerDelegate<TResponse> handler)
    {
        _handler = handler;
    }

    public async Task<IResult> HandleAsync()
    {
        return await _handler();
    }
}