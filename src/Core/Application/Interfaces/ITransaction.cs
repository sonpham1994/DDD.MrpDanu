using Application.Behaviors.TransactionalBehaviours;
using Domain.SharedKernel.Base;

namespace Application.Interfaces;

public interface ITransaction
{
    Task<IResult> HandleAsync(TransactionalHandler transactionalHandler);
}
