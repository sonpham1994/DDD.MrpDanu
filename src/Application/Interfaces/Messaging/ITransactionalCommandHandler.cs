using Domain.SharedKernel.Base;
using MediatR;

namespace Application.Interfaces.Messaging;

public interface ITransactionalCommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ITransactionalCommand
{
}
