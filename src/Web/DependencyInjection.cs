using Application;
using Infrastructure;
using Web.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddCustomMiddlewares()
            .DisableDefaultModelValidation()
            .AddValidators();

        return services;
    }

    private static IServiceCollection AddCustomMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<GlobalExceptionHandlingMiddleware>();

        return services;
    }
    
    private static IServiceCollection DisableDefaultModelValidation(this IServiceCollection services)
    {
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IObjectModelValidator));
        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
            services.Add(new ServiceDescriptor(typeof(IObjectModelValidator), _ => new NullObjectModelValidator(), ServiceLifetime.Singleton));
        }
        
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ApplicationAssembly.Instance, ServiceLifetime.Scoped, null, true);

        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        return services;
    }
}