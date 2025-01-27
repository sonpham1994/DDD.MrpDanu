using SharedKernel;
using MediatR;

namespace Application.Interfaces.Messaging;

public interface ICommand : IRequest<Result>, IValidationCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IValidationCommand;

public interface IValidationCommand;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;