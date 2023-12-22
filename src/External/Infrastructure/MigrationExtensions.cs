using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;
using Infrastructure.Persistence.Externals;
using Infrastructure.Persistence.Externals.AuditTables;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Text;
using Domain.SharedKernel.Enumerations;

namespace Infrastructure;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync(this IServiceScopeFactory services)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var externalDbContext = scope.ServiceProvider.GetRequiredService<ExternalDbContext>();

        await dbContext.Database.MigrateAsync();
        await externalDbContext.Database.MigrateAsync();

        //Due to ignoring all changes of Enumeration on DbInterceptor, we cannot use dbContext, we use sql raw instead.
        
        if (!(await dbContext.Set<MaterialType>().AnyAsync()))
        {
            var list = MaterialType.List.Where(x => x.Id > 0).ToList();
            await InsertEnumerationAsync(list, nameof(MaterialType) , dbContext);
        }
        
        if (!(await dbContext.Set<RegionalMarket>().AnyAsync()))
        {
            var list = RegionalMarket.List.Where(x => x.Id > 0).ToList();
            await InsertEnumerationAsyncWithCode(list, nameof(RegionalMarket), dbContext);
        }

        if (!(await dbContext.Set<CurrencyType>().AnyAsync()))
        {
            var list = CurrencyType.List.Where(x => x.Id > 0).ToList();
            await InsertEnumerationAsync(list, nameof(CurrencyType), dbContext);
        }

        if (!(await dbContext.Set<LocationType>().AnyAsync()))
        {
            var list = LocationType.List.Where(x => x.Id > 0).ToList();
            await InsertEnumerationAsync(list, nameof(LocationType), dbContext);
        }

        if (!(await dbContext.Set<TransactionalPartnerType>().AnyAsync()))
        {
            var list = TransactionalPartnerType.List.Where(x => x.Id > 0).ToList();
            await InsertEnumerationAsync(list, nameof(TransactionalPartnerType), dbContext);
        }

        if (!(await dbContext.Set<Country>().AnyAsync()))
        {
            var list = Country.List.Where(x => x.Id > 0).ToList();
            await InsertEnumerationAsyncWithCode(list, nameof(Country), dbContext);
        }

        if (!(await externalDbContext.Set<StateAuditTable>().AnyAsync()))
        {
            var list = StateAuditTable.List.Where(x => x.Id > 0).ToList();
            await InsertEnumerationAsync(list, nameof(StateAuditTable), externalDbContext);
        }
    }

    private static async Task InsertEnumerationAsync<T>(List<T> list, string tableName, DbContext dbContext)
        where T : Enumeration<T>
    {
        var commandBuilder = new StringBuilder();
        foreach (var i in list)
        {
            commandBuilder.AppendLine($"INSERT INTO {tableName} (Id, Name) VALUES ({i.Id}, '{i.Name}');");
        }

        var command = FormattableStringFactory.Create(commandBuilder.ToString());

        await dbContext.Database.ExecuteSqlAsync(command);
    }

    private static async Task InsertEnumerationAsyncWithCode<T>(List<T> list, string tableName, AppDbContext dbContext)
        where T : Enumeration<T>
    {
        var commandBuilder = new StringBuilder();
        foreach (var i in list)
        {
            var code = typeof(T).GetProperty("Code")!.GetValue(i);
            commandBuilder.AppendLine($"INSERT INTO {tableName} (Id, Code, Name) VALUES ({i.Id}, '{code}', '{i.Name}');");
        }

        var command = FormattableStringFactory.Create(commandBuilder.ToString());

        await dbContext.Database.ExecuteSqlAsync(command);
    }
}
