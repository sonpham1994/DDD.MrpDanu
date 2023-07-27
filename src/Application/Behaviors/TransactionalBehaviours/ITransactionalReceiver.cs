using Domain.SharedKernel.Base;

namespace Application.Behaviors.TransactionalBehaviours;

internal interface ITransactionalReceiver
{ 
    Task<IResult> HandleAsync();
}