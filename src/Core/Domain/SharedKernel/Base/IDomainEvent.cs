namespace Domain.SharedKernel.Base;

public abstract record DomainEvent
{
    public DateTime DateOccured { get; set; } = DateTime.UtcNow;
}
