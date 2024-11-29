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

app.MapGet("/test-api", (IOptionsMonitor<DatabaseSettings> test) =>
{
    var a = test;
});

app.UseHttpsRedirection();

using (var a = app.Services.CreateScope())
{
    var b = a.ServiceProvider.GetRequiredService<TestingOptions>();
}


app.Run();