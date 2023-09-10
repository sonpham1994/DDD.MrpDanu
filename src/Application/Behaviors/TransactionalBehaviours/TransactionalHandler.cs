using Application.Behaviors.TransactionalBehaviours.Handlers;
using Domain.SharedKernel.Base;

namespace Application.Behaviors.TransactionalBehaviours;

public sealed class TransactionalHandler<TRequest, TResponse>
    where TResponse : IResult
{
    private readonly ITransactionalReceiver[] _transactionalReceivers;

    internal TransactionalHandler(params ITransactionalReceiver[] transactionalReceivers)
    {
        _transactionalReceivers = transactionalReceivers;
    }

    public async Task<IResult> HandleAsync()
    {
        for (int i = 0; i < _transactionalReceivers.Length; i++)
        {
            var transactionalReceiver = _transactionalReceivers[i];
            if (_transactionalReceivers[i] is RequestHandler<TRequest, TResponse> request)
            {
            }
            var result = await transactionalReceiver.HandleAsync();
            if (result.IsFailure)
            {
                await HandleRollBackAsync(i);
                return result;
            }
                
        }
            
        return Result.Success();
    }

    private async Task HandleRollBackAsync(int i)
    {
        for (; i >= 0; i--)
        {
            var type = _transactionalReceivers[i].GetType();
            if (_transactionalReceivers[i] is RequestHandler<TRequest,TResponse> request)
            {
                var commandRequest = request.Request;
            }
        }
    }
}