using Application.Errors;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Exceptions;

namespace Application.Behaviors.TransactionalBehaviours;

public sealed class TransactionalHandler<TResponse>
    where TResponse : IResult
{
    private readonly ITransactionalReceiver<TResponse>[] _transactionalReceivers;

    internal TransactionalHandler(params ITransactionalReceiver<TResponse>[] transactionalReceivers)
    {
        _transactionalReceivers = transactionalReceivers ?? throw new DomainException(DomainErrors.NullHandler);
    }

    public async Task<TResponse> HandleAsync()
    {
        foreach (var transactionalReceiver in _transactionalReceivers)
        {
            var result = await transactionalReceiver.HandleAsync();
            if (result.IsFailure)
                return result;
        }

        IResult successResult = Result.Success();
        return (TResponse)successResult;
    }
}