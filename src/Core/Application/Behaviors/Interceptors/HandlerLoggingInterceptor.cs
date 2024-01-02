using Application.Extensions;
using Application.LoggingDefinitions;
using Domain.SharedKernel.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Behaviors.Interceptors;

//https://www.youtube.com/watch?v=HRt7KIkdIaw
internal sealed class HandlerLoggingInterceptor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    private readonly ILogger<HandlerLoggingInterceptor<TRequest, TResponse>> _logger;
    private readonly ILogger<GCEventListener> _loggerGCEventListener;
    private readonly IRequestHandler<TRequest, TResponse> _requestHandler;

    public HandlerLoggingInterceptor(IRequestHandler<TRequest, TResponse> requestHandler,
        ILogger<HandlerLoggingInterceptor<TRequest, TResponse>> logger,
        ILogger<GCEventListener> loggerGCEventListener)
    {
        _requestHandler = requestHandler;
        _logger = logger;
        _loggerGCEventListener = loggerGCEventListener;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using var gcEventListener = new GCEventListener(_loggerGCEventListener);
        var handlerName = _requestHandler.GetType().Name;
        var requestTypeName = typeof(TRequest).Name;
        
        _logger.StartHandler(handlerName, requestTypeName);
        var start = Stopwatch.GetTimestamp();
        var response = await next();
        var delta = Stopwatch.GetElapsedTime(start);
        if (response.IsFailure)
        {
            _logger.HandleFailure(handlerName, requestTypeName, response.Error, delta.TotalMicroseconds);
        }
        else
        {
            _logger.CompletedHandler(handlerName, requestTypeName, delta.TotalMicroseconds);
        }

        return response;
    }
}