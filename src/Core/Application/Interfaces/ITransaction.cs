using Application.Behaviors.TransactionalBehaviours;
using SharedKernel;

namespace Application.Interfaces;

public interface ITransaction
{
    Task<IResult> HandleAsync(TransactionalHandler transactionalHandler);
}
