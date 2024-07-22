using MediatR;

namespace SharedKernel;

public abstract record DomainEventBase : INotification
{
  public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}

