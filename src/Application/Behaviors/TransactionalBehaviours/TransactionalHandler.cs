using Domain.SharedKernel.Base;

namespace Application.Behaviors.TransactionalBehaviours;

public sealed class TransactionalHandler
{
    private readonly ITransactionalReceiver[] _transactionalReceivers;

    public TransactionalHandler(params ITransactionalReceiver[] transactionalReceivers)
    {
        _transactionalReceivers = transactionalReceivers;
    }

    public async Task<IResult> HandleAsync()
    {
        foreach (var transactionalReceiver in _transactionalReceivers)
        {
            var result = await transactionalReceiver.HandleAsync();
            if (result.IsFailure)
                return result;
        }

        return Result.Success();
    }
}