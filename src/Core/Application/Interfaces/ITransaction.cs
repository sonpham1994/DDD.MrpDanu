using Application.Behaviors.TransactionalBehaviours;
using Domain.SharedKernel.Base;

namespace Application.Interfaces;

public interface ITransaction
{
    Task<TResponse> HandleAsync<TResponse>(TransactionalHandler<TResponse> transactionalHandler) where TResponse : IResult;
}
