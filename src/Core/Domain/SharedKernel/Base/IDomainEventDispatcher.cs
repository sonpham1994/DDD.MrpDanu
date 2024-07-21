namespace Domain.SharedKernel.Base;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IHaveDomainEvents entityWithEvents);
    Task DispatchAndClearEvents(IReadOnlyList<IHaveDomainEvents> entitiesWithEvents);
    Task Dispatch(IReadOnlyList<DomainEventBase> domainEvents);
}