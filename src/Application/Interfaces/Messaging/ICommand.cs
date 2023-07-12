using Domain.SharedKernel.Base;
using MediatR;

namespace Application.Interfaces.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}