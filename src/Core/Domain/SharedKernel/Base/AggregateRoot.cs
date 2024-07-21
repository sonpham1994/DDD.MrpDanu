namespace Domain.SharedKernel.Base;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot, IHaveDomainEvents
    where TId : struct, IEquatable<TId>
{
    private readonly List<DomainEventBase> _domainEvents = new();
    public IReadOnlyList<DomainEventBase> DomainEvents => _domainEvents;

    protected AggregateRoot() { }

    protected void RaiseDomainEvent(DomainEventBase newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    private readonly List<DomainEventBase> _domainEvents = new();
    public IReadOnlyList<DomainEventBase> DomainEvents => _domainEvents;

    protected AggregateRoot() { }

    protected void RaiseDomainEvent(DomainEventBase newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public abstract class AggregateRootGuidStronglyTypedId<TId> : EntityGuidStronglyTypedId<TId> , IAggregateRoot
    where TId : struct, IEquatable<TId>, IGuidStronglyTypedId
{
    private readonly List<DomainEventBase> _domainEvents = new();
    public IReadOnlyList<DomainEventBase> DomainEvents => _domainEvents;
    protected AggregateRootGuidStronglyTypedId() { }

    protected void RaiseDomainEvent(DomainEventBase newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public interface IAggregateRoot;