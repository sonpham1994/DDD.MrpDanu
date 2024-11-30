using Application.Behaviors.TransactionalBehaviours;
using SharedKernel;

namespace Application.Interfaces;

public interface ITransaction
{
    Task<Result> HandleAsync(TransactionalHandler transactionalHandler);
}
