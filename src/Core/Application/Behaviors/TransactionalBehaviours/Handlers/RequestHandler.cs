using SharedKernel;
using MediatR;

namespace Application.Behaviors.TransactionalBehaviours.Handlers;

internal sealed class RequestHandler(RequestHandlerDelegate<Result> handler) : ITransactionalReceiver
{
    public async Task<Result> HandleAsync()
    {
        return await handler();
    }
}