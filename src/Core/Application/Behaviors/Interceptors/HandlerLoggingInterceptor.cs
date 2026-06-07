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
        var handlerName = requestHandler.GetType().Name;
        var requestName = typeof(TRequest).Name;

        logger.StartHandler(handlerName, requestName);

        var stopWatch = Stopwatch.StartNew();
        var response = await next();
        stopWatch.Stop();
        
        logger.CompletedHandler(handlerName, requestName, response, stopWatch.ElapsedMilliseconds);

        return response;
    }
}