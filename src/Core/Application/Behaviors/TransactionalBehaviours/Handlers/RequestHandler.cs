using SharedKernel;
using MediatR;

namespace Application.Behaviors.TransactionalBehaviours.Handlers;

internal sealed class RequestHandler<TResponse>(RequestHandlerDelegate<TResponse> handler) : ITransactionalReceiver
    where TResponse : IResult
{
    public async Task<IResult> HandleAsync()
    {
        return await handler();
    }
}