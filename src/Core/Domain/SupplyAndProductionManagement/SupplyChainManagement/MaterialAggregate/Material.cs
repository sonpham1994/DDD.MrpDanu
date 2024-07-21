using Domain.SharedKernel.Extensions.LinqExtensions;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public class Material : EntityGuidStronglyTypedId<MaterialId>, IAuditTable, IAggregateRoot, IHaveDomainEvents
{
    private readonly List<DomainEventBase> _domainEvent = [];
    public IReadOnlyList<DomainEventBase> DomainEvents => _domainEvent.AsReadOnly();
    private void RaiseDomainEvent(DomainEventBase domainEvent) => _domainEvent.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvent.Clear();
    
    // this largest number of materials depends on domain, it doesn't make sense if we have the infinite number.
    // The number 20 is the number you should discuss with domain experts, if the domain experts say that 
    // the number 20 is the maximum, you put it here. (Write UT for this case)
    // we use expression body to reduce memory allocation on heap, instead of using "private const byte MaxNumberOfMaterialCosts = 20"
    private static byte MaxNumberOfMaterialCosts => 20;
    private readonly List<MaterialSupplierCost> _materialSupplierCosts = new(MaxNumberOfMaterialCosts);

    public MaterialCode Code { get; private set; } = default!;
    public MaterialName Name { get; private set; } = default!;
    public MaterialAttributes Attributes { get; private set; } = default!;
    public MaterialType MaterialType { get; private set; } = default!;
    public RegionalMarket RegionalMarket { get; private set; } = default!;

    public virtual IReadOnlyList<MaterialSupplierCost> MaterialSupplierCosts =>
        _materialSupplierCosts.ToList();

    //required EF Proxies
    protected Material() { }

    private Material(MaterialCode code, MaterialName name, MaterialAttributes attributes, MaterialType materialType, RegionalMarket regionalMarket)
        : this()
    {
        Code = code;
        Name = name;
        Attributes = attributes;
        MaterialType = materialType;
        RegionalMarket = regionalMarket;
    }

    public static Result<Material?> Create(MaterialCode code, MaterialName name, MaterialAttributes attributes, MaterialType materialType,
        RegionalMarket regionalMarket, UniqueMaterialCodeResult uniqueCodeResult)
    {
        if (!uniqueCodeResult.IsSuccess)
            return uniqueCodeResult.Error;
        var result = TypeAndRegionalMarketValidation(materialType, regionalMarket);
        if (!result.IsSuccess)
            return result.Error;

        return new Material(code, name, attributes, materialType, regionalMarket);
    }

    public Result UpdateMaterial(MaterialCode code, MaterialName name, MaterialAttributes attributes, MaterialType materialType,
        RegionalMarket regionalMarket, UniqueMaterialCodeResult uniqueCodeResult)
    {
        if (!uniqueCodeResult.IsSuccess)
            return uniqueCodeResult.Error;

        var result = TypeAndRegionalMarketValidation(materialType, regionalMarket);
        if (!result.IsSuccess)
            return result;
        
        Code = code;
        Name = name;
        Attributes = attributes;
        MaterialType = materialType;
        RegionalMarket = regionalMarket;

        return Result.Success();
    }

    //need to use ubiquitous language for this method
    public Result UpdateCost(IReadOnlyList<MaterialSupplierCost> materialSupplierCosts)
    {
        var existByAnotherMaterialId = materialSupplierCosts
            .FindOrFirstDefault(x => x.MaterialSupplierIdentity.MaterialId != Id);
        if (existByAnotherMaterialId is not null)
            return DomainErrors.Material.IdsAreNotTheSame(Id, existByAnotherMaterialId.MaterialSupplierIdentity.MaterialId);

        foreach (var materialSupplierCost in materialSupplierCosts)
        {
            var materialSupplierCostExisted = GetMaterialSupplierCost(materialSupplierCost);

            if (materialSupplierCostExisted is not null)
            {
                if (materialSupplierCostExisted.Price != materialSupplierCost.Price)
                {
                    MaterialPriceChangedDomainEvent materialPriceChangedEvent = new(Id, materialSupplierCost.MaterialSupplierIdentity.SupplierId, materialSupplierCost.Price);
                    RaiseDomainEvent(materialPriceChangedEvent);
                }
                var setMaterialCostResult = materialSupplierCostExisted.SetMaterialCost(materialSupplierCost.Price, materialSupplierCost.MinQuantity, materialSupplierCost.Surcharge);
                if (!setMaterialCostResult.IsSuccess)
                    return setMaterialCostResult;
            }
            else
            {
                if (_materialSupplierCosts.Count + 1 > MaxNumberOfMaterialCosts)
                    return DomainErrors.Material.ExceedsMaxNumberOfMaterialCosts;

                _materialSupplierCosts.Add(materialSupplierCost);
            }
        }

        _materialSupplierCosts
            .RemoveAll(x =>
                !materialSupplierCosts.ExistOrAny(j => j.MaterialSupplierIdentity == x.MaterialSupplierIdentity));

        return Result.Success();
    }


    private MaterialSupplierCost? GetMaterialSupplierCost(MaterialSupplierCost materialSupplierCost)
    {
        var result = _materialSupplierCosts
            .Find(x => x.MaterialSupplierIdentity == materialSupplierCost.MaterialSupplierIdentity);

        return result;
    }

    private static Result TypeAndRegionalMarketValidation(MaterialType materialType, RegionalMarket regionalMarket)
    {
        if (materialType == MaterialType.Material && regionalMarket != RegionalMarket.None)
            return DomainErrors.Material.InvalidMaterialType;

        if (materialType == MaterialType.Subassemblies && regionalMarket == RegionalMarket.None)
            return DomainErrors.Material.InvalidSubassembliesType;

        return Result.Success();
    }

    
}


// //not use this, this is just for analyze each solution for audit data
// public class MaterialForLutionAudit : AggregateRoot, IAuditTableForSolution1, IAuditTableForSolution3
// {
//     private readonly List<MaterialSupplierCost> _materialCostManagements = new();
//
//     public string Code { get; private set; }
//     public string CodeUnique { get; }
//     public MaterialAttributes Attributes { get; private set; }
//     public virtual MaterialType MaterialType { get; private set; }
//     public virtual RegionalMarket RegionalMarket { get; private set; }
//
//     public virtual IReadOnlyList<MaterialSupplierCost> MaterialCostManagements =>
//         _materialCostManagements;
//
//     //required EF Proxies
//     protected MaterialForLutionAudit() { }
//
//     private MaterialForLutionAudit(string code, MaterialAttributes attributes, MaterialType materialType, RegionalMarket regionalMarket)
//     : this()
//     {
//         Code = code;
//         //CodeUnique = attributes.ToUniqueCode();
//         Attributes = attributes;
//         MaterialType = materialType;
//         RegionalMarket = regionalMarket;
//     }
//
//     public static Result<MaterialForLutionAudit> Create(string code, MaterialAttributes attributes, MaterialType materialType,
//         RegionalMarket regionalMarket)
//     {
//         var result = CanCreateOrUpdateMaterial(code, materialType, regionalMarket);
//         if (result.IsFailure)
//             return result.Error;
//
//         return new MaterialForLutionAudit(code.Trim(), attributes, materialType, regionalMarket);
//     }
//
//     public Result UpdateMaterial(string code, MaterialAttributes attributes, MaterialType materialType,
//         RegionalMarket regionalMarket)
//     {
//         var result = CanCreateOrUpdateMaterial(code, materialType, regionalMarket);
//         if (result.IsFailure)
//             return result;
//
//         Code = code.Trim();
//         Attributes = attributes;
//         MaterialType = materialType;
//         RegionalMarket = regionalMarket;
//
//         return Result.Success();
//     }
//
//     //need to use ubiquitous language for this method
//     public Result UpdateCost(IReadOnlyList<MaterialSupplierCost> materialCosts)
//     {
//         var supplierDuplication = materialCosts.ItemDuplication(x => x.TransactionalPartner);
//         if (supplierDuplication is not null)
//             return DomainErrors.MaterialCostManagement.DuplicationSupplierId(supplierDuplication.Id);
//
//         foreach (var materialCost in materialCosts)
//         {
//             var materialCostManagement = _materialCostManagements
//                 .Find(x => x.TransactionalPartner == materialCost.TransactionalPartner);
//
//             if (materialCostManagement is not null)
//             {
//                 var setMaterialCostResult = materialCostManagement.SetMaterialCost(materialCost.Price, materialCost.MinQuantity, materialCost.Surcharge);
//                 if (setMaterialCostResult.IsFailure)
//                     return setMaterialCostResult;
//             }
//             else if (materialCostManagement is null)
//             {
//                 _materialCostManagements.Add(materialCost);
//             }
//         }
//
//         _materialCostManagements
//             .RemoveAll(x =>
//                 !materialCosts.Any(j => j.TransactionalPartner == x.TransactionalPartner));
//
//         return Result.Success();
//     }
//
//     public Result<MaterialCostManagement?> GetMaterialCost(TransactionalPartner supplier)
//     {
//         var materialCost = _materialCostManagements.Find(x => x.TransactionalPartner == supplier);
//
//         if (materialCost is null)
//             return DomainErrors.MaterialCostManagement.NotExistSupplier(supplier.Id, Id);
//
//         return materialCost;
//     }
//
//     private static Result CanCreateOrUpdateMaterial(string code, MaterialType materialType, RegionalMarket regionalMarket)
//     {
//         if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code))
//             return DomainErrors.Material.EmptyCode;
//
//         if (materialType == MaterialType.Material && regionalMarket != RegionalMarket.None)
//             return DomainErrors.Material.InvalidMaterialType;
//
//         if (materialType == MaterialType.Subassemblies && regionalMarket == RegionalMarket.None)
//             return DomainErrors.Material.InvalidSubassembliesType;
//
//         return Result.Success();
//     }
//
//     public (string, string) Serialize()
//     {
//         var id = Id.ToString();
//         var auditData = new
//         {
//             Id = id,
//             Code = Code,
//             CodeUnique = CodeUnique,
//             //Name = Attributes.Name,
//             ColorCode = Attributes.ColorCode,
//             Width = Attributes.Width,
//             Weight = Attributes.Weight,
//             Unit = Attributes.Unit,
//             Varian = Attributes.Varian,
//             MaterialType = MaterialType,
//             RegionalMarket = RegionalMarket,
//             MaterialCostManagements = _materialCostManagements.Select(x => new
//             {
//                 Id = x.Id,
//                 Price = x.Price.Value,
//                 x.MinQuantity,
//                 Surcharge = x.Surcharge.Value,
//                 CurrencyType = x.Price.CurrencyType,
//                 Supplier = new
//                 {
//                     Id = x.TransactionalPartner.Id, // this one will cause performance issue due to Lazy loading
//                     Name = x.TransactionalPartner.Name.Value
//                 }
//
//             }).ToList()
//         };
//         var json = JsonSerializer.Serialize(auditData);
//
//         return (id, json);
//     }
// }