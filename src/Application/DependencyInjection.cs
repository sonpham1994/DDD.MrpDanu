using Application.Behaviors;
using Application.Behaviors.Interceptors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator()
            .AddBehaviours();

        return services;
    }
    
    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssembly.Instance));
        
        return services;
    }

    private static IServiceCollection AddBehaviours(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestLoggingInterceptor<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorInterceptor<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorResponseInterceptor<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionalBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(HandlerLoggingInterceptor<,>));
        
        return services;
    }
}