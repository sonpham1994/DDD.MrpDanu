using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Domain.SupplyChainManagement.MaterialAggregate;
using Domain.SharedKernel.Base;
using Infrastructure.Errors;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Externals.AuditTables.Solutions.Entities;

// internal sealed class MaterialAudit : MaterialForLutionAudit, IAuditTableForSolution2
// {
//     private Guid _id;
//     private readonly string _code;
//     private readonly string _codeUnique;
//     private readonly MaterialAttributes _attributes;
//     private readonly MaterialType _materialType;
//     private readonly RegionalMarket _regionalMarket;
//     private readonly IReadOnlyList<MaterialCostManagement> _materialCostManagements;
//
//     public MaterialAudit(Material material)
//     {
//         _id = material.Id;
//         _code = material.Code;
//         _codeUnique = material.CodeUnique;
//         _attributes = material.Attributes;
//         _materialType = material.MaterialType;
//         _regionalMarket = material.RegionalMarket;
//         _materialCostManagements = material.MaterialCostManagements;
//     }
//
//     public new (string, string) Serialize()
//     {
//         string id = _id.ToString();
//         var auditData = new
//         {
//             Id = id,
//             Code = _code,
//             CodeUnique = _codeUnique,
//             _attributes.Name,
//             _attributes.ColorCode,
//             _attributes.Width,
//             _attributes.Weight,
//             _attributes.Unit,
//             _attributes.Varian,
//             MaterialType = _materialType,
//             RegionalMarket = _regionalMarket,
//             MaterialCostManagements = _materialCostManagements.Select(x => new
//             {
//                 x.Id,
//                 Price = x.Price.Value,
//                 x.MinQuantity,
//                 Surcharge = x.Surcharge.Value,
//                 x.Price.CurrencyType,
//                 Supplier = new
//                 {
//                     x.TransactionalPartner.Id, // this one will cause performance issue due to Lazy loading
//                     Name = x.TransactionalPartner.Name.Value
//                 }
//             }).ToList()
//         };
//         var json = JsonSerializer.Serialize(auditData);
//
//         return (id, json);
//     }
//
//     public Result<(string, string)> Serialize<T>(T obj)
//     {
//         if (obj is Material material)
//         {
//             var id = material.Id.ToString();
//             var auditData = new
//             {
//                 Id = id,
//                 material.Code,
//                 material.CodeUnique,
//                 material.Attributes.Name,
//                 material.Attributes.ColorCode,
//                 material.Attributes.Width,
//                 material.Attributes.Weight,
//                 material.Attributes.Unit,
//                 material.Attributes.Varian,
//                 material.MaterialType,
//                 material.RegionalMarket,
//                 MaterialCostManagements = material.MaterialCostManagements.Select(x => new
//                 {
//                     x.Id,
//                     Price = x.Price.Value,
//                     x.MinQuantity,
//                     Surcharge = x.Surcharge.Value,
//                     x.Price.CurrencyType,
//                     Supplier = new
//                     {
//                         x.TransactionalPartner.Id, // this one will cause performance issue due to Lazy loading
//                         Name = x.TransactionalPartner.Name.Value
//                     }
//                 }).ToList()
//             };
//             var json = JsonSerializer.Serialize(auditData);
//
//             return (id, json);
//         }
//
//         return Result.Failure(InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(obj.GetType().Name));
//     }
// }
//
// internal sealed class MaterialAuditForSolution2_1 : AuditTableForSolution2_1
// {
//     public override Result<AuditTableForSolution2_1> Serialize<T>(T obj)
//     {
//         if (obj is Material material)
//         {
//             var id = material.Id.ToString();
//             var auditData = new
//             {
//                 Id = id,
//                 material.Code,
//                 material.CodeUnique,
//                 material.Attributes.Name,
//                 material.Attributes.ColorCode,
//                 material.Attributes.Width,
//                 material.Attributes.Weight,
//                 material.Attributes.Unit,
//                 material.Attributes.Varian,
//                 material.MaterialType,
//                 material.RegionalMarket,
//                 MaterialCostManagements = material.MaterialCostManagements.Select(x => new
//                 {
//                     x.Id,
//                     Price = x.Price.Value,
//                     x.MinQuantity,
//                     Surcharge = x.Surcharge.Value,
//                     x.Price.CurrencyType,
//                     Supplier = new
//                     {
//                         x.TransactionalPartner.Id, // this one will cause performance issue due to Lazy loading
//                         Name = x.TransactionalPartner.Name.Value
//                     }
//                 }).ToList()
//             };
//             var json = JsonSerializer.Serialize(auditData);
//
//             Id = id;
//             Content = json;
//             ObjectName = nameof(Material);
//             return this;
//         }
//
//         return InfrastructureDomainErrors.AuditData.NotSupportEntityAuditTypeYet(obj.GetType().Name);
//     }
// }