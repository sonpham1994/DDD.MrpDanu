using System.Net;
using System.Text.Json;
using Application.Helpers;
using Domain.Exceptions;
using Web.ApiModels.BaseResponses;
using Web.SourceGenerators;

namespace Web.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly Serilog.ILogger _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionHandlingMiddleware(IWebHostEnvironment env, Serilog.ILogger logger) 
    {
        _env = env;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (DomainException e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            string json = JsonSerializer.Serialize(AppResponse.Failure(e.Errors), JsonSourceGeneratorJsonContext.Default.AppResponse);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        catch (Exception e)
        {
            string traceId = Helper.GetTraceId();
            string messageDetail = $"Exception: {e.Message}{Environment.NewLine}{e.InnerException}";
            var error = _env.IsProduction() ? AppErrors.InternalServerErrorOnProduction : AppErrors.InternalServerError(messageDetail);
            
            _logger.Error(e, "----- TraceId: {TraceId}, ErrorMessageDetail: {ErrorMessageDetail}", traceId, messageDetail);
            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string json = JsonSerializer.Serialize(AppResponse.Failure(error), JsonSourceGeneratorJsonContext.Default.AppResponse);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}