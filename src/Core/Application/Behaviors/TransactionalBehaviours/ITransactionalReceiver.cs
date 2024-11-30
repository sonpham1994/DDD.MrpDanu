using SharedKernel;

namespace Application.Behaviors.TransactionalBehaviours;

internal interface ITransactionalReceiver
{
    Task<Result> HandleAsync();
}