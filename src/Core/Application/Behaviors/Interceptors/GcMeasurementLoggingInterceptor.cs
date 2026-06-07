using System.Diagnostics;
using Application.Interfaces;
using Application.LoggingDefinitions;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Behaviors.Interceptors;

internal sealed class GcMeasurementLoggingInterceptor<TRequest, TResponse>(
    IRequestHandler<TRequest, TResponse> requestHandler,
    ILogger<HandlerLoggingInterceptor<TRequest, TResponse>> logger,
    IFeatureFlagService featureFlagService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!await featureFlagService.IsGcMeasureEnabledAsync())
            return await next();
        
        // using var gcEventListener = new GcEventListener(_loggerGCEventListener);
        var handlerName = requestHandler.GetType().Name;
        var requestName = typeof(TRequest).Name;

        var previousGen0Count = GC.CollectionCount(0);
        var previousGen1Count = GC.CollectionCount(1);
        var previousGen2Count = GC.CollectionCount(2);

        var response = await next();
        
        logger.GcGenerationLogging(handlerName, requestName, previousGen0Count, previousGen1Count,
            previousGen2Count);

        return response;
    }
}