using Application.LoggingDefinitions;
using Domain.SharedKernel.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Application.EventListeners;

namespace Application.Behaviors.Interceptors;

//https://www.youtube.com/watch?v=HRt7KIkdIaw
internal sealed class GCHandlerLoggingInterceptor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    private readonly ILogger<GCHandlerLoggingInterceptor<TRequest, TResponse>> _logger;
    private readonly ILogger<GCEventListener> _loggerGCEventListener;
    private readonly IRequestHandler<TRequest, TResponse> _requestHandler;

    public GCHandlerLoggingInterceptor(IRequestHandler<TRequest, TResponse> requestHandler,
        ILogger<GCHandlerLoggingInterceptor<TRequest, TResponse>> logger,
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

        var previousGen0Count = GC.CollectionCount(0);
        var previousGen1Count = GC.CollectionCount(1);
        var previousGen2Count = GC.CollectionCount(2);

        var response = await next();

        _logger.GCGenerationLogging(handlerName, requestTypeName, previousGen0Count, previousGen1Count,
            previousGen2Count);

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