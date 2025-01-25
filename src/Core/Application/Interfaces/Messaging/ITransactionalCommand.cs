using MediatR;
using SharedKernel;

namespace Application.Interfaces.Messaging;

public interface ITransactionalCommand;

public interface ITransactionalCommandHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionalCommand
    where TResponse : IResult;