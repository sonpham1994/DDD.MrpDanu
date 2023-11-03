using Application;
using Infrastructure;
using Api;
using Api.Middlewares;
using Serilog;
using Api.SourceGenerators;
using Api.MaterialManagement;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;

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

//we should use this approach instead of .AddJsonOptions for minimal api
// please check https://github.com/dotnet/aspnetcore/issues/38621
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.AddContext<JsonSourceGeneratorJsonContext>();
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
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
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
