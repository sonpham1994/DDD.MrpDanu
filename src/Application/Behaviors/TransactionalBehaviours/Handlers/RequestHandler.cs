using Application.Interfaces.Messaging;
using Domain.SharedKernel.Base;
using MediatR;

namespace Application.Behaviors.TransactionalBehaviours.Handlers;

internal sealed class RequestHandler<TRequest, TResponse> : ITransactionalReceiver
    where TResponse : IResult
{
    private readonly RequestHandlerDelegate<TResponse> _handler;
    public TRequest Request { get; }

    public RequestHandler(TRequest request, RequestHandlerDelegate<TResponse> handler)
    {
        Request = request;
        _handler = handler;
    }

    public async Task<IResult> HandleAsync()
    {
        return await _handler();
    }
}