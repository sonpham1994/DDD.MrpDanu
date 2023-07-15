using Domain.SharedKernel.Base;

namespace Application.Behaviors.TransactionalBehaviours;

public interface ITransactionalReceiver
{ 
    Task<IResult> HandleAsync();
}