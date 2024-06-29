using Application.Helpers;
using Serilog.Context;

namespace Api.Middlewares;

public sealed class LoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var traceId = Helper.GetTraceId();
        using (LogContext.PushProperty("ApplicationTraceId", traceId))
        {
            await next(context);
        }
    }
}