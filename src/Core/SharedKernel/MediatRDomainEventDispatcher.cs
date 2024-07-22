using MediatR;

namespace SharedKernel;

public sealed class MediatRDomainEventDispatcher(IPublisher publisher) : IDomainEventDispatcher
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
            await publisher.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}
