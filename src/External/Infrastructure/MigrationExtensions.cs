using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;
using Infrastructure.Persistence.Externals;
using Infrastructure.Persistence.Externals.AuditTables;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Text;
using Domain.SharedKernel.Enumerations;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync(this IServiceScopeFactory services)
    {
        using var scope = services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await using var externalDbContext = scope.ServiceProvider.GetRequiredService<ExternalDbContext>();

        //should be noticed with the code first approach. The ProductVersion in __EFMigrationsHistory talbe in the version of your EF
        // for example if your EF currently uses the version 7.0.12, the ProductVersion would be 7.0.12. After you migrate EF version
        // to 8.0, you should delete all records in __EFMigrationsHistory table and then initialize it from start, now the ProductVersion
        // column would be 8.0. With different ProductVersions the migration may throws some exceptions
        await dbContext.Database.MigrateAsync();
        await externalDbContext.Database.MigrateAsync();

        //Due to ignoring all changes of Enumeration on DbInterceptor, we cannot use dbContext, we use sql raw instead.

        if (!await dbContext.Set<MaterialType>().AnyAsync())
        {
            var list = MaterialType.List.Where(x => x.Id > 0).ToList();
            // create a string with char for not be interned. Because we just use this at once when we start program, hence we
            // don't need to allocate this value string in high-frequency heap. And when Gc 2 is perform collection process.
            // These value on high-frequency heap are never collected. By creating string as char[], these value will not live
            // in High-Frequency heap and then they are eligible for collection of GC. Please check TestGcForString project
            // Please check Benchmark/StringInternOrHighFrequencyHeap
            char[] materialTypeChars = ['M', 'a', 't', 'e', 'r', 'i', 'a', 'l', 'T', 'y', 'p', 'e'];
            await InsertEnumerationAsync(list, new string(materialTypeChars), dbContext);
        }

        if (!await dbContext.Set<RegionalMarket>().AnyAsync())
        {
            var list = RegionalMarket.List.Where(x => x.Id > 0).ToList();
            char[] regionalMarketChars = ['R', 'e', 'g', 'i', 'o', 'n', 'a', 'l', 'M', 'a', 'r', 'k', 'e', 't'];
            await InsertEnumerationAsyncWithCode(list, new string(regionalMarketChars), dbContext);
        }

        if (!await dbContext.Set<CurrencyType>().AnyAsync())
        {
            var list = CurrencyType.List.Where(x => x.Id > 0).ToList();
            char[] currencyTypeChars = ['C', 'u', 'r', 'r', 'e', 'n', 'c', 'y', 'T', 'y', 'p', 'e'];
            await InsertEnumerationAsync(list, new string(currencyTypeChars), dbContext);
        }

        if (!await dbContext.Set<LocationType>().AnyAsync())
        {
            var list = LocationType.List.Where(x => x.Id > 0).ToList();
            char[] locationTypeChars = ['L', 'o', 'c', 'a', 't', 'i', 'o', 'n', 'T', 'y', 'p', 'e'];
            await InsertEnumerationAsync(list, new string(locationTypeChars), dbContext);
        }

        if (!await dbContext.Set<TransactionalPartnerType>().AnyAsync())
        {
            var list = TransactionalPartnerType.List.Where(x => x.Id > 0).ToList();
            char[] transactionalPartnerTypeChars =
            [
                'T', 'r', 'a', 'n', 's', 'a', 'c', 't', 'i', 'o', 'n', 'a', 'l', 'P', 'a', 'r', 't', 'n', 'e', 'r', 'T', 'y', 'p', 'e'
            ];
            await InsertEnumerationAsync(list, new string(transactionalPartnerTypeChars), dbContext);
        }

        if (!await dbContext.Set<Country>().AnyAsync())
        {
            var list = Country.List.Where(x => x.Id > 0).ToList();
            char[] countryChars = ['C', 'o', 'u', 'n', 't', 'r', 'y'];
            await InsertEnumerationAsyncWithCode(list, new string(countryChars), dbContext);
        }

        if (!await externalDbContext.Set<StateAuditTable>().AnyAsync())
        {
            var list = StateAuditTable.List.Where(x => x.Id > 0).ToList();
            char[] stateAuditTableChars = ['S', 't', 'a', 't', 'e', 'A', 'u', 'd', 'i', 't', 'T', 'a', 'b', 'l', 'e'];
            await InsertEnumerationAsync(list, new string(stateAuditTableChars), externalDbContext);
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
