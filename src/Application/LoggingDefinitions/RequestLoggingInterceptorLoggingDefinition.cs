using Application.Extensions;
using Application.Helpers;
using Domain.SharedKernel.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.LoggingDefinitions;

//https://www.youtube.com/watch?v=bnVfrd3lRv8&t=1113s&ab_channel=NickChapsas
//https://www.youtube.com/watch?v=a26zu-pyEyg&t=609s&ab_channel=NickChapsas
//https://www.youtube.com/watch?v=6zoMd_FwSwQ&t=5s&ab_channel=NickChapsas

/*the comments from @jsfelipearaujo in https://www.youtube.com/watch?v=a26zu-pyEyg&t=609s&ab_channel=NickChapsas
 *   @jsfelipearaujo: Hi Nick, first of all this was an awesome video! But I've a question: Do I need do something
 *  like that when using Serilog?
 *   Nick: Depends on how you are using it; if you’re using it through the Microsoft logging then yeah. If you’re
 *  doing raw Serilog ILogger then no
 * We're using Serilog through Microsoft logging, so we need to apply LoggingDefinitions.
*/
internal static class RequestLoggingInterceptorLoggingDefinition
{
    private static readonly Action<ILogger, string, string, object, DateTime, Exception?> StartRequestLoggingDefinition =
        LoggerMessage.Define<string, string, object, DateTime>(LogLevel.Information, 0,
            "----- TraceId: {TraceId} - Starting request: {RequestName} - Request: {@Request} - Time: {@DateTimeUtc}");
    
    private static readonly Action<ILogger, string, string, object, DateTime, Exception?> CompletedRequestLoggingDefinition =
        LoggerMessage.Define<string, string, object, DateTime>(LogLevel.Information, 0,
            "----- TraceId: {TraceId} - Completed request: {RequestName} - Response: {@Response} - Time: {@DateTimeUtc}");
    
    private static readonly Action<ILogger, string, string, Result, DateTime, Exception?> CompletedRequestWithResultLoggingDefinition =
        LoggerMessage.Define<string, string, Result, DateTime>(LogLevel.Information, 0,
            "----- TraceId: {TraceId} - Completed request: {RequestName} - Response: {@Response} - Time: {@DateTimeUtc}");

    public static void StartRequest<TRequest>(this ILogger logger, TRequest request)
    {
        var requestTypeName = typeof(TRequest).Name;
        string traceId = Helper.GetTraceId();
        
        StartRequestLoggingDefinition(logger, traceId, requestTypeName, request, DateTime.UtcNow, null);
    }
    
    public static void CompletedRequest<T>(this ILogger logger, in T response, string requestTypeName)
    {
        string traceId = Helper.GetTraceId();
        //Result is struct, so we need to prevent boxing
        if (response is Result result)
        {
            CompletedRequestWithResultLoggingDefinition(logger, traceId, requestTypeName, result, DateTime.UtcNow, null);
        }
        //for case Result<T> we cannot prevent boxing, because we need to know the exact which T type is
        else
        {
            CompletedRequestLoggingDefinition(logger, traceId, requestTypeName, response, DateTime.UtcNow, null);
        }
        
    }
}