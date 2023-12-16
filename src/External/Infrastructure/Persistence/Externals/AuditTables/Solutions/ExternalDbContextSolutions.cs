// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Domain.Extensions;
// using Domain.SharedKernel.Base;
// using Infrastructure.Persistence.Externals.AuditTables;
// using Infrastructure.Persistence.Externals.AuditTables.Solutions.Factories;
// using Infrastructure.Persistence.Interceptors;
// using Infrastructure.Persistence.Write;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.EntityFrameworkCore.Diagnostics;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
//
// namespace Infrastructure.Persistence.Externals.AuditTables.Entities.Solutions;
//
// internal sealed class ExternalDbContextSolutions : DbContext
// {
//     private readonly string _connectionString;
//     private readonly bool _isProduction;
//     private readonly LoggingDbCommandInterceptor _loggingDbCommandInterceptor;
//     private IReadOnlyList<AuditTable> _auditDataForSolution1 = Array.Empty<AuditTable>();
//     private IReadOnlyList<AuditTable> _auditDataForSolution2 = Array.Empty<AuditTable>();
//     private IReadOnlyList<AuditTable> _auditDataForSolution2_1 = Array.Empty<AuditTable>();
//     private IReadOnlyList<AuditTable> _auditDataForSolution3 = Array.Empty<AuditTable>();
//     private readonly IInterceptor[] _interceptors;
//
//     public IReadOnlyList<AuditTable> AuditDataForSolution1 => _auditDataForSolution1;
//     public IReadOnlyList<AuditTable> AuditDataForSolution2 => _auditDataForSolution2;
//     public IReadOnlyList<AuditTable> AuditDataForSolution2_1 => _auditDataForSolution2_1;
//     public IReadOnlyList<AuditTable> AuditDataForSolution3 => _auditDataForSolution3;
//
//     internal ExternalDbContextSolutions(IOptions<DatabaseSettings> databaseSettings
//         , bool isProduction
//         , LoggingDbCommandInterceptor loggingDbCommandInterceptor
//         , params IInterceptor[] interceptors)
//     {
//         _connectionString = databaseSettings.Value.ConnectionString;
//         _loggingDbCommandInterceptor = loggingDbCommandInterceptor;
//         _isProduction = isProduction;
//         _interceptors = interceptors;
//     }
//
//     public DbSet<AuditTable> AuditTables => Set<AuditTable>();
//     public DbSet<StateAuditTable> StateAuditTables => Set<StateAuditTable>();
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder
//             .UseSqlServer(_connectionString)
//             .AddInterceptors(_interceptors);
//
//         if (!_isProduction)
//         {
//             optionsBuilder
//                 .EnableSensitiveDataLogging()
//                 .AddInterceptors(_loggingDbCommandInterceptor);
//         }
//     }
//
//     //Solution 1
//     public void SetAuditDataForSolution1(AppDbContext appDbContext)
//     {
//         var auditType = typeof(IAuditTableForSolution1);
//
//         //due to the fact that EF Core cannot mark owned entity type as Modified state, so we remove the condition
//         //of x.State != EntityState.Modified and get all data with implementing IAuditTable. For example modifying
//         //Surcharge or Price in MaterialCostManagement
//         //https://stackoverflow.com/questions/46872428/owned-type-property-not-persisting-for-a-modified-entity-in-ef-core?fbclid=IwAR3wWOXGo6d_A_ETasDDvJ12i1rJ2rRY_LnGurAZuR5Avht_gAa6qmi2MZ8
//         //Because EF core treats owned entity type as a splitting table, so it works as navigation properties.
//         //When we update owned entity type, depending on FK (which is shadow property Id) of owned entity type
//         //  - Added: If your owned entity type is detached mode (or shadow property Id is empty)
//         //  - Deleted: If the navigation property to the owned entity "is set to null", then this removes the reference to it from the parent entity, so there is no reference to it from the parent EntityEntry.
//         //https://github.com/dotnet/efcore/issues/16513
//         //https://github.com/dotnet/efcore/issues/27848
//         //https://github.com/dotnet/efcore/pull/21259
//         //This is behaviour is of navigation properties, as well as owned entity type
//         //So if don't change anything and call savechange, they always mark as IsModified due to Shadow PropertyId is empty
//         //we can work around by checking if they are different, take a look at SetMaterialCost in MaterialCostManagement, for example 
//
//         /* if you want to take advantage of set ModifiedDate by Interceptors, you can face the same problem with this if you modify 
//          *  the owned entity type https://github.com/dotnet/efcore/issues/29848
//          */
//
//         /*
//          * Some scenarios can work around for Value Object when working with EF Core from Julie Lerman
//          * https://learn.microsoft.com/en-us/archive/msdn-magazine/2018/april/data-points-ef-core-2-owned-entities-and-temporary-work-arounds
//          */
//         /*
//          * another problem with this aprroach is that, now it record data as Domain Model, these data just reserve for tracing and reading purpose,
//          *  so maybe we need to write pure entity for that read operation. Or we can use Domian model for checking whether our domain model will be
//          *  invalid state or not, but this situation is rarely to happen (I think so).
//          * And also, these data perist with LazyLoader coming from Proxy - Lazy Loading
//          */
//         _auditDataForSolution1 = appDbContext.ChangeTracker
//             .Entries()
//             .Where(x => auditType.IsAssignableFrom(x.Entity.GetType()!)
//                 && (x.State != EntityState.Unchanged
//                     || (x.State == EntityState.Unchanged // for ValueObject with Owned Entity Type
//                         && x.References
//                             .Any(j => j.IsModified))))
//             .Select(AuditTableFactory.CreateForSolution1)
//             .Where(x => x is not null)
//             .ToList();
//     }
//
//     public void SetAuditDataForSolution2(AppDbContext appDbContext)
//     {
//         _auditDataForSolution2 = appDbContext.ChangeTracker
//             .Entries()
//             .Where(x => AuditTableFactory.AuditTypesForSolution2.Any(j => j.IsSubclassOf(x.Entity.GetUnproxiedType()!))
//                         && (x.State != EntityState.Unchanged
//                             || (x.State == EntityState.Unchanged // for ValueObject with Owned Entity Type
//                                 && x.References
//                                     .Any(j => j.IsModified))))
//             .Select(AuditTableFactory.CreateForSolution2)
//             .Where(x => x is not null)
//             .ToList();
//     }
//
//     public void SetAuditDataForSolution2_1(AppDbContext appDbContext)
//     {
//         _auditDataForSolution2_1 = appDbContext.ChangeTracker
//             .Entries()
//             .Where(x => AuditTableFactory.AuditTypesForSolution2_1.Any(j => j == x.Entity.GetUnproxiedType()!)
//                         && (x.State != EntityState.Unchanged
//                             || (x.State == EntityState.Unchanged // for ValueObject with Owned Entity Type
//                                 && x.References
//                                     .Any(j => j.IsModified))))
//             .Select(AuditTableFactory.CreateForSolution2_1)
//             .Where(x => x is not null)
//             .ToList();
//     }
//
//     public void SetAuditDataForSolution3(AppDbContext appDbContext)
//     {
//         var auditType = typeof(IAuditTableForSolution3);
//         _auditDataForSolution3 = appDbContext.ChangeTracker
//             .Entries()
//             .Where(x => auditType.IsAssignableFrom(x.Entity.GetType()!)
//                         && (x.State != EntityState.Unchanged
//                             || (x.State == EntityState.Unchanged // for ValueObject with Owned Entity Type
//                                 && x.References
//                                     .Any(j => j.IsModified))))
//             .Select(AuditTableFactory.CreateForSolution3)
//             .Where(x => x is not null)
//             .ToList();
//     }
// }
//
// //internal class ExternalDbContextDesignFactory : IDesignTimeDbContextFactory<ExternalDbContextSolutions>
// //{
// //    public ExternalDbContextSolutions CreateDbContext(string[] args)
// //    {
// //        string connectionString = "Server=LAPTOP-IHQC4RF9,1433;Database=DDD.MrpDanu;User Id=sa;Password=Vsa*12345#;TrustServerCertificate=true";
// //        //string connectionString = "Server=localhost,1433;Database=DDD.MrpDanu;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=true";
// //        ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
// //        {
// //        });
// //        IOptions<DatabaseSettings> options = Options.Create(new DatabaseSettings
// //        {
// //            ConnectionString = connectionString
// //        });
// //        return new ExternalDbContext(
// //            options,
// //            false,
// //            new LoggingDbCommandInterceptor(loggerFactory.CreateLogger<LoggingDbCommandInterceptor>()));
// //    }
// //}