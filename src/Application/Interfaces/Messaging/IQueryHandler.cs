using Domain.SharedKernel.Base;
using MediatR;

namespace Application.Interfaces.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}