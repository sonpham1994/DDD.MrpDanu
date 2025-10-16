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
    private const string MessageLogging = LoggingDefinition.Prefix + " - RequestName: {RequestName}";
    
    private static readonly Action<ILogger, string, string, DateTime, Exception?> StartRequestLoggingDefinition =
        LoggerMessage.Define<string, string, DateTime>(LogLevel.Information, 0,
            MessageLogging + LoggingDefinition.Start + LoggingDefinition.Suffix);
    
    private static readonly Action<ILogger, string, string, double, DateTime, Exception?> CompletedRequestLoggingDefinition =
        LoggerMessage.Define<string, string, double, DateTime>(LogLevel.Information, 0,
            MessageLogging + LoggingDefinition.Completed + LoggingDefinition.SuffixWithElapsedSecond);
    
    public static void StartRequest(this ILogger logger, string requestName)
    {
        StartRequestLoggingDefinition(logger, LoggingTypes.Request, requestName, DateTime.UtcNow, null);
    }
    
    public static void CompletedRequest(this ILogger logger, string requestName, double totalMilliseconds)
    {
        CompletedRequestLoggingDefinition(logger, LoggingTypes.Request, requestName, totalMilliseconds, DateTime.UtcNow, null);
    }
}