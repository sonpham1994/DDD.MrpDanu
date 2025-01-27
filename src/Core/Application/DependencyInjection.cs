using Application.Behaviors.Interceptors;
using Application.Behaviors.TransactionalBehaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();

        return services;
    }
    
    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(ApplicationAssembly.Instance)
                .AddOpenBehavior(typeof(RequestLoggingInterceptor<,>))
                .AddOpenBehavior(typeof(ValidatorInterceptor<,>))
                .AddOpenBehavior(typeof(TransactionalBehavior<,>))
                .AddOpenBehavior(typeof(HandlerLoggingInterceptor<,>));
        });
        
        return services;
    }
}