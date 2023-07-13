using System;
using Infrastructure.Persistence.Externals;
using Infrastructure.Persistence.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IServiceProvider services, bool isProduction)
    {
        if (!isProduction)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var externalDbContext = scope.ServiceProvider.GetRequiredService<ExternalDbContext>();

            dbContext.Database.Migrate();
            externalDbContext.Database.Migrate();
        }
    }
}
