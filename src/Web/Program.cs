using Application;
using Infrastructure;
using Web;
using Web.Middlewares;
using Serilog;
using Web.JsonSourceGenerator;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Web.Api.MaterialManagement;

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

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.AddContext<JsonSourceGeneratorJsonContext>();

});;

var isProduction = builder.Environment.IsProduction();

builder.Services
    .AddApplication()
    .AddInfrastructure(isProduction)
    .AddWeb(isProduction);

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MaterialManagement}/{action=Index}/{id?}");

app.MapMaterialManagementEndpoints();

app.Run();