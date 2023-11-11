using Application;
using Infrastructure;
using Api;
using Api.Middlewares;
using Serilog;
using Api.SourceGenerators;
using Api.Controllers.MaterialManagement;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization.Metadata;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
// Serilog.ILogger logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
// builder.Logging.AddSerilog(logger);
// builder.Services.AddSingleton(logger);

// Log.Logger = new LoggerConfiguration()  
//     .ReadFrom.Configuration(builder.Configuration)  
//     .CreateLogger();
//builder.Host.UseSerilog();

builder.Host.UseSerilog((context, services, configuration) =>
{
    //read from configuration, in dev, staging environment. We need
    //to log a information minimum level. But in production environment
    //we may just need to log a warning minimum level. So we should configure
    // our logging from appsettings.json

    //if we want to log the machine name and thread id etc on each log. Please
    //check Install-Package Serilog.Enrichers.Thread, and Install-Package Serilog.Enrichers.Environment

    //check this article and video 
    //https://www.milanjovanovic.tech/blog/structured-logging-in-asp-net-core-with-serilog
    //https://www.youtube.com/watch?v=nVAkSBpsuTk&ab_channel=MilanJovanovi%C4%87

    //configuration.Enrich.FromLogContext();
    //configuration.Enrich.WithMachineName();
    //configuration.WriteTo.Console();
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.Configure<HostOptions>(x =>
{
    //wait for .NET 8 to use HostedService as background task which is concurrently.
    //https://www.youtube.com/watch?v=XA_3CZmD9y0&ab_channel=NickChapsas
});

//we should use this approach for minimal api, because internally call WriteAsJsonAsync, so you can configure JsonOptions 
// globally
// please check https://github.com/dotnet/aspnetcore/issues/38621
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.AddContext<MinimalApiJsonSourceGenerator>();
});

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer()));

    //disable validation in .Net core 7: there are two ways:
    // - https://stackoverflow.com/questions/46374994/correct-way-to-disable-model-validation-in-asp-net-core-2-mvc
    // - https://alexlvovich.com/blog/how-to-disable-object-model-validation-net-7
    //options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    options.ModelValidatorProviders.Clear();

    //disable removing Aync for api
    options.SuppressAsyncSuffixInActionNames = false;
})
.AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.AddContext<ApiJsonSourceGenerator>();

    /*
    * For the Create, Update with request model, we will run into the problem 
    *   "Metadata for type 'Microsoft.AspNetCore.Mvc.ValidationProblemDetails' was not provided by TypeInfoResolver of type. If 
    *    using source generation, ensure that all root types passed to the serializer have been indicated with 
    *   'JsonSerializableAttribute', along with any types that might be serialized polymorphically."
    * We need to add ValidationProblemDetails for Create/Update/Delete actions for source generator but it seems like complicated.
    * Check issue here: https://github.com/dotnet/aspnetcore/issues/43236

    * And since we added ValidationProblemDetails in Source Generator and add it here, we run into another problem: 
    *   "'ValidationProblemDetailsJsonConverter' is inaccessible due to its protection level" so in this case we use 
    * "JsonTypeInfoResolver.Combine" to combine our Source Generator, and if it's not, it goes back the reflection-based "new DefaultJsonTypeInfoResolver()"
    * this issue will be fixed on .Net 8.
    * Check the issue here: https://github.com/dotnet/runtime/issues/83815
    *
    * Please noticed that: when we fall back to reflection-based, we don't see the error of register source generator. So some
    * api use reflection-based but you expect it should be source generator.
    */
    options.JsonSerializerOptions.TypeInfoResolver = JsonTypeInfoResolver.Combine(ApiJsonSourceGenerator.Default, new DefaultJsonTypeInfoResolver());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MrpDanu.Api", Version = "v1" });
});

var isProduction = builder.Environment.IsProduction();

builder.Services
    .AddApplication()
    .AddInfrastructure(isProduction)
    .AddApi(isProduction);

string allowedOrigin = builder.Configuration["AllowedOrigin"]!;

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors =>
{
    cors.WithOrigins(allowedOrigin)
        .AllowAnyHeader() //Allows any the headers that the client want to send
        .AllowAnyMethod(); //Allows any the methods the client side want to use including GET, POST, PUT and all other verbs
});

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.MapMaterialManagementEndpoints();

app.Run();
