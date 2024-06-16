using Domain.SharedKernel.Base;
using MediatR;

namespace Application.Interfaces.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse?>>
    where TQuery : IQuery<TResponse?>;
    
public interface IQueryListHandler<TQueryList, TResponse> : IRequestHandler<TQueryList, Result<IReadOnlyList<TResponse>>>
    where TQueryList : IQueryList<TResponse>;