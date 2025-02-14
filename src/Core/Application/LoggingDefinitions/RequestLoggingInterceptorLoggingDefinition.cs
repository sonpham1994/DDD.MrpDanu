using SharedKernel;
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
    private const string MessageLogging = SharedLoggingDefinition.Prefix + " - RequestName: {RequestName}";
    
    private static readonly Action<ILogger, string, string, DateTime, Exception?> StartRequestLoggingDefinition =
        LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
            MessageLogging + SharedLoggingDefinition.Start + SharedLoggingDefinition.Suffix);
    
    private static readonly Action<ILogger, string, string, bool, DomainError, DateTime, Exception?> CompletedRequestLoggingDefinition =
        LoggerMessage.Define<string, string, bool, DomainError, DateTime>(LogLevel.Information, 0,
            MessageLogging + SharedLoggingDefinition.Completed + " - ResponseIsSuccess: {ResponseIsSuccess} - Error: {@ResponseError}" + SharedLoggingDefinition.Suffix);
    
    public static void StartRequest<TRequest>(this ILogger logger, TRequest _)
    {
        var requestTypeName = typeof(TRequest).Name;
        
        StartRequestLoggingDefinition(logger, LoggingTypes.Request, requestTypeName, DateTime.UtcNow, null);
    }
    
    public static void CompletedRequest<T>(this ILogger logger, T response, string requestTypeName)
        where T : IResult
    {
        CompletedRequestLoggingDefinition(logger, LoggingTypes.Request, requestTypeName, response.IsSuccess, response.Error, DateTime.UtcNow, null);
    }
}