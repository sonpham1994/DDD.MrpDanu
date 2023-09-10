using Application.Behaviors.TransactionalBehaviours;
using Domain.SharedKernel.Base;

namespace Application.Interfaces;

public interface ITransaction<TRequest, TResponse> where TResponse : IResult
{
    Task<IResult> HandleAsync(TransactionalHandler<TRequest, TResponse> transactionalHandler);
}
