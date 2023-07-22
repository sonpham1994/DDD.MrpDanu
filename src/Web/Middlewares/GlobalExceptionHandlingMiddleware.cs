using System.Net;
using System.Text.Json;
using Application.Helpers;
using Domain.Exceptions;
using Web.ApiModels.BaseResponses;

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
        var jsonOptions = new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        try
        {
            await next(context);
        }
        catch (DomainException e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            string json = JsonSerializer.Serialize(AppResponse.Error(e.Errors), jsonOptions);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        catch (Exception e)
        {
            string traceId = Helper.GetTraceId();
            string messageDetail = $"Exception: {e.Message}{Environment.NewLine}{e.InnerException}";
            string errorMessage = _env.IsProduction() ? "Internal server error" : messageDetail;
            
            _logger.Error(e, "----- TraceId: {TraceId}, ErrorMessageDetail: {ErrorMessageDetail}", traceId, messageDetail);
            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string json = JsonSerializer
                .Serialize(AppResponse.Error(AppErrors.InternalServerError(errorMessage)), jsonOptions);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}