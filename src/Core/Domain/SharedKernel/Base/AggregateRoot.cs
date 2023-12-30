namespace Domain.SharedKernel.Base;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : struct, IEquatable<TId>
{
    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

    protected AggregateRoot() { }

    protected void RaiseDomainEvent(DomainEvent newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

    protected AggregateRoot() { }

    protected void RaiseDomainEvent(DomainEvent newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public abstract class AggregateRootGuidStronglyTypedId<TId> : EntityGuidStronglyTypedId<TId> 
    where TId : struct, IEquatable<TId>, IGuidStronglyTypedId
{
    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;
    protected AggregateRootGuidStronglyTypedId() { }

    protected void RaiseDomainEvent(DomainEvent newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}