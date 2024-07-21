using MediatR;

namespace Domain.SharedKernel.Base;

public sealed class MediatRDomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task DispatchAndClearEvents(IHaveDomainEvents entityWithEvents)
    {
        var events = entityWithEvents.DomainEvents.ToArray();
        entityWithEvents.ClearDomainEvents();
        await Dispatch(events);
    }
    
    public async Task DispatchAndClearEvents(IReadOnlyList<IHaveDomainEvents> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
        {
            await DispatchAndClearEvents(entity);
        }
    }

    public async Task Dispatch(IReadOnlyList<DomainEventBase> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}
