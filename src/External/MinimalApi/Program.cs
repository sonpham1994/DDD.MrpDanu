using System.Text.Json.Serialization.Metadata;
using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using MinimalApi;
using MinimalApi.Endpoints;
using MinimalApi.Middlewares;
using MinimalApi.SourceGenerators;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.Configure<HostOptions>(x =>
{
    x.ServicesStartConcurrently = true;
    x.ServicesStopConcurrently = true;
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolver = JsonTypeInfoResolver
        .Combine(ApiRequestJsonSourceGenerator.Default, ApiResponseJsonSourceGenerator.Default);
    
    //obsolete
    //options.SerializerOptions.AddContext<MinimalApiJsonSourceGenerator>();
});

builder.Services.AddCors();

var isProduction = builder.Environment.IsProduction();

builder.Services
    .AddApplication()
    .AddInfrastructure(isProduction)
    .AddMinimalApi(isProduction, builder.Configuration);

string allowedOrigin = builder.Configuration["AllowedOrigin"]!;


#if PRODUCTION
    Console.WriteLine("Run on Production mode");
#else
Console.WriteLine("Run on Development mode");
#endif

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSerilogRequestLogging();

app.UseCors(cors =>
{
    cors.WithOrigins(allowedOrigin)
        .AllowAnyHeader() //Allows any the headers that the client want to send
        .AllowAnyMethod(); //Allows any the methods the client side want to use including GET, POST, PUT and all other verbs
});

//app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapSupplyChainManagementEndpoints();

app.UseHttpsRedirection();

/*
 * TODO: build a configuration api instead of using ACC, when the configurations change, we publish an event and
 * the subscriber we receive that event and update on memory themself. consider using Redis, and check the price
 * when build api configuration yourself and ACC on Azure or Azure Key Vault Secret.
 * sample here: https://dogabariscakmak.medium.com/net-core-ioptions-with-redis-backplane-1ee7f9c26619
 */

/*
 * TODO: Add log for module (or controller/minimal api) to check that whether it's used a lot or not. If it's hardly
 * ever used by user, consider moving it to Azure Function for saving expense. If you build microservice and
 * each service is traditional host, consider all of them for using a lot or not and move it to Azure Function
 * can save client's money. Also we can scale the module that uses a lot efficiently.
 */

app.Run();