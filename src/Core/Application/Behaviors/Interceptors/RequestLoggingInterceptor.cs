using Application.LoggingDefinitions;
using SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors.Interceptors;

internal sealed class RequestLoggingInterceptor<TRequest, TResponse>(
    ILogger<RequestLoggingInterceptor<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestTypeName = typeof(TRequest).Name;

        logger.StartRequest(request);
    
        var response = await next();
    
        logger.CompletedRequest(response, requestTypeName);

        return response;
    }
}