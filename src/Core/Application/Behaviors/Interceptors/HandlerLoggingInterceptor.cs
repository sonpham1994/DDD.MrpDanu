using Application.LoggingDefinitions;
using SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Behaviors.Interceptors;

//https://www.youtube.com/watch?v=HRt7KIkdIaw
internal sealed class HandlerLoggingInterceptor<TRequest, TResponse>(
    IRequestHandler<TRequest, TResponse> requestHandler,
    ILogger<HandlerLoggingInterceptor<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // using var gcEventListener = new GcEventListener(_loggerGCEventListener);
        var handlerName = requestHandler.GetType().Name;
        var requestTypeName = typeof(TRequest).Name;

        logger.StartHandler(handlerName, requestTypeName);
        var previousGen0Count = GC.CollectionCount(0);
        var previousGen1Count = GC.CollectionCount(1);
        var previousGen2Count = GC.CollectionCount(2);

        var stopWatch = Stopwatch.StartNew();
        var response = await next();

        logger.GcGenerationLogging(handlerName, requestTypeName, previousGen0Count, previousGen1Count,
            previousGen2Count);
        stopWatch.Stop();
        logger.CompletedHandler(handlerName, requestTypeName, response, stopWatch.ElapsedMilliseconds);

        return response;
    }
}