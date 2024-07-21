namespace Domain.SharedKernel.Base;

public interface IHaveDomainEvents
{
    IReadOnlyList<DomainEventBase> DomainEvents { get; }
    void ClearDomainEvents();
}

public abstract class HasDomainEventsBase : IHaveDomainEvents
{
    private readonly List<DomainEventBase> _domainEvents = [];
    public IReadOnlyList<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}