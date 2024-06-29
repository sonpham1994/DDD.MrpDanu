using System.Net;
using System.Text.Json;
using Domain.SharedKernel.Exceptions;
using Api.ApiResponses;
using Api.SourceGenerators;

namespace Api.Middlewares;

public class GlobalExceptionHandlingMiddleware(IWebHostEnvironment env, Serilog.ILogger logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (DomainException e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            string json = JsonSerializer.Serialize(ApiResponseExtensions.Failure(e.Errors), ApiResponseJsonSourceGenerator.Default.ApiResponse);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        catch (Exception e)
        {
            string messageDetail = $"Exception: {e.Message}{Environment.NewLine}{e.InnerException}";
            var error = env.IsProduction() ? ApiErrors.InternalServerErrorOnProduction : ApiErrors.InternalServerError(messageDetail);

            logger.Error(e, "ErrorMessageDetail: {ErrorMessageDetail}", messageDetail);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string json = JsonSerializer.Serialize(ApiResponseExtensions.Failure(error), ApiResponseJsonSourceGenerator.Default.ApiResponse);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}