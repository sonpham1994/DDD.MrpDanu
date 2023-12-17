using Application.LoggingDefinitions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.Interceptors;

internal sealed class RequestLoggingInterceptor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<RequestLoggingInterceptor<TRequest, TResponse>> _logger;

    public RequestLoggingInterceptor(ILogger<RequestLoggingInterceptor<TRequest, TResponse>> logger)
        => _logger = logger;
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestTypeName = typeof(TRequest).Name;

        _logger.StartRequest(request);
        
        var response = await next();
        
        _logger.CompletedRequest(response, requestTypeName);

        return response;
    }
}