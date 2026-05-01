using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MinimalApi.Endpoints;

public static class Extension
{
    public static void AddEndpoints(this IServiceCollection services)
    {
        // Assembly assembly = Assembly.GetExecutingAssembly();
        //
        // ServiceDescriptor[] endpointServiceDescriptors = assembly
        //     .DefinedTypes
        //     .Where(type => type.IsClass && !type.IsAbstract && !type.IsInterface &&
        //                    typeof(IEndpoint).IsAssignableFrom(type))
        //     .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
        //     .ToArray();
        //
        // services.TryAddEnumerable(endpointServiceDescriptors);
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        //IEnumerable<IEndpoint> endpoints = app.Services.GetServices<IEndpoint>();

        // using this if you make sure endpoints use DI method instead of DI constructor
        var currentAssembly = Assembly.GetExecutingAssembly();
        var endpoints = currentAssembly.GetTypes()
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t) 
                        && t != typeof(IEndpoint) 
                        && t is { IsPublic: true, IsAbstract: false })
            .Select(t => (IEndpoint)Activator.CreateInstance(t)!)
            .ToArray();
        
        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}