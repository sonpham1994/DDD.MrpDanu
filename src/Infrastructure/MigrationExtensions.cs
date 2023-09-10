using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel;
using Infrastructure.Persistence.Externals;
using Infrastructure.Persistence.Externals.AuditTables;
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

            dbContext.Set<MaterialType>().AddRange(MaterialType.List);
            dbContext.Set<RegionalMarket>().AddRange(RegionalMarket.List);
            dbContext.Set<CurrencyType>().AddRange(CurrencyType.List);
            dbContext.Set<LocationType>().AddRange(LocationType.List);
            dbContext.Set<TransactionalPartnerType>().AddRange(TransactionalPartnerType.List);
            dbContext.Set<Country>().AddRange(Country.List);
            externalDbContext.Set<StateAuditTable>().AddRange(StateAuditTable.List);
            
            dbContext.SaveChanges();
        }
    }
}
