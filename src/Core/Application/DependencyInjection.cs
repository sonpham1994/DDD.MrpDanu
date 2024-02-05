using Application.Behaviors.Interceptors;
using Application.Behaviors.TransactionalBehaviours;
using Application.Behaviors.TransactionalBehaviours.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator()
            .AddHandlers();

        return services;
    }
    
    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(ApplicationAssembly.Instance)
                .AddOpenBehavior(typeof(RequestLoggingInterceptor<,>))
                .AddOpenBehavior(typeof(ValidatorInterceptor<,>))
                .AddOpenBehavior(typeof(ValidatorResponseInterceptor<,>))
                .AddOpenBehavior(typeof(TransactionalBehavior<,>))
                .AddOpenBehavior(typeof(GCHandlerLoggingInterceptor<,>));
        });
        
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<AuditTableHandler>();

        return services;
    }
}