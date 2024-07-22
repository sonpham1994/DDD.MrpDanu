using MediatR;

namespace SharedKernel;

public abstract record DomainEventBase : INotification
{
  public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}

