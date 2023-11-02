using Application;
using Infrastructure;
using Web;
using Web.Middlewares;
using Serilog;
using Web.Api.MaterialManagement;
using Web.SourceGenerators;

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

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddControllersWithViews().AddJsonOptions(options =>
// {
//     //options.JsonSerializerOptions.PropertyNamingPolicy = null;
//     options.JsonSerializerOptions.AddContext<JsonSourceGeneratorJsonContext>();
//
// });

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// app.MapControllerRoute(name: "materials",
//     pattern: "api/material-management/materials/{*id}",
//     defaults: new { controller = "Material", action = "Delete" });
// app.MapControllerRoute("material_route", "api/material-management/[controller]",
//     defaults: "api/material-management/{controller=Materials}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MaterialManagement}/{action=Index}/{id?}");


app.MapMaterialManagementEndpoints();

app.Run();