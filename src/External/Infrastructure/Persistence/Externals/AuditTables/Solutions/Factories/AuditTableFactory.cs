// using System;
// using System.Linq;
// using Domain.SharedKernel.Exceptions;
// using Domain.Extensions;
// using Domain.SupplyChainManagement.MaterialAggregate;
// using Domain.SharedKernel.Base;
// using Infrastructure.Errors;
// using Infrastructure.Persistence.Externals.AuditTables.Solutions.Entities;
// using Infrastructure.Persistence.Externals.AuditTables.Solutions.Factories.EntitiesFactory;
// using Microsoft.EntityFrameworkCore.ChangeTracking;
// using Microsoft.IdentityModel.Tokens;
//
// namespace Infrastructure.Persistence.Externals.AuditTables.Solutions.Factories;
//
// internal sealed class AuditTableFactory
// {
//     public static readonly Type[] AuditTypesForSolution2 =
//     {
//         typeof(MaterialAudit)
//     };
//
//     public static readonly Type[] AuditTypesForSolution2_1 =
//     {
//         typeof(Material)
//     };
//
//
//     public static AuditTable CreateForSolution1(EntityEntry entityEntry)
//     {
//         AuditTable auditTable;
//         var auditType = typeof(IAuditTableForSolution1);
//
//         if (auditType.IsAssignableFrom(entityEntry.Entity.GetType()!))
//         {
//             var result = MaterialAuditFactory.CreateForSolution1(entityEntry);
//             if (result.IsFailure)
//                 throw new DomainException(InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity.GetUnproxiedType().Name));
//
//             auditTable = result.Value;
//         }
//         else
//         {
//             throw new DomainException(InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity.GetUnproxiedType().Name));
//         }
//
//         return auditTable;
//     }
//
//     public static AuditTable? CreateForSolution2(EntityEntry entityEntry)
//     {
//         var result = MaterialAuditFactory.CreateForSolution2(entityEntry);
//         if (result.IsFailure)
//             throw new DomainException(InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity.GetUnproxiedType().Name));
//
//         return result.Value;
//     }
//
//     private readonly static Type[] AuditTableTypes = CreateAuditTableTypes();
//
//     private static Type[] CreateAuditTableTypes()
//     {
//         var type = typeof(AuditTableForSolution2_1);
//         var auditTableTypes = InfrastructureAssembly.Instance
//             .GetTypes()
//             .Where(x => x.BaseType is not null && x.BaseType == type)
//             .ToArray();
//
//         return auditTableTypes;
//     }
//
//     public static AuditTable CreateForSolution2_1(EntityEntry entityEntry)
//     {
//         AuditTable? result = null;
//         var state = StateAuditTable.FromEntityState(entityEntry);
//         if (state.IsFailure)
//             throw new DomainException(state.Error);
//
//         foreach (var auditTableType in AuditTableTypes)
//         {
//             var typeName = auditTableType.Name;
//             AuditTableForSolution2_1 audit = typeName switch
//             {
//                 nameof(MaterialAuditForSolution2_1) => new MaterialAuditForSolution2_1(),
//                 _ => throw new DomainException(InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity.GetUnproxiedType().Name))
//             };
//
//             var materialAudit = audit.Serialize(entityEntry.Entity);
//
//             if (materialAudit.IsSuccess)
//                 result = new AuditTable
//                 (
//                     materialAudit.Value.Content,
//                     materialAudit.Value.Id,
//                     materialAudit.Value.ObjectName,
//                     state.Value,
//                     Guid.Empty
//                 );
//         }
//
//         if (result is null)
//         {
//             throw new DomainException(InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity.GetUnproxiedType().Name));
//         }
//
//
//         return result;
//     }
//
//     public static AuditTable? CreateForSolution3(EntityEntry entityEntry)
//     {
//         var result = MaterialAuditFactory.CreateForSolution3(entityEntry);
//         if (result.IsFailure)
//             throw new DomainException(InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity.GetUnproxiedType().Name));
//
//         return result.Value;
//     }
// }