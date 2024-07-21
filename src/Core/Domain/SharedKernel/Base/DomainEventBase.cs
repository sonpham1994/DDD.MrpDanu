using MediatR;

namespace Domain.SharedKernel.Base;

public abstract record DomainEventBase : INotification
{
  public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}

