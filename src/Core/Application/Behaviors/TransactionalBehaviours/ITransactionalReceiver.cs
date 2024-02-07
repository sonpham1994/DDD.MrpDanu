using Domain.SharedKernel.Base;

namespace Application.Behaviors.TransactionalBehaviours;

internal interface ITransactionalReceiver<TResponse> where TResponse : IResult
{ 
    Task<TResponse> HandleAsync();
}