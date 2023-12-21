using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Domain.MaterialManagement.MaterialAggregate;

public class Material : AggregateRootGuidStronglyTypedId<MaterialId>
{
    // this largest number of materials depends on domain, it doesn't make sense if we have the infinite number.
    // The number 20 is the number you should discuss with domain experts, if the domain experts say that 
    // the number 20 is the maximum, you put it here. (Write UT for this case)
    // we use expression body to reduce memory allocation on heap, instead of using "private const byte MaxNumberOfMaterialCosts = 20"
    private static byte MaxNumberOfMaterialCosts => 20;
    private readonly List<MaterialSupplierCost> _materialSupplierCosts = new(MaxNumberOfMaterialCosts);

    public string Code { get; private set; }
    public string Name { get; private set; }
    public MaterialAttributes Attributes { get; private set; }
    public virtual MaterialType MaterialType { get; private set; }
    public virtual RegionalMarket RegionalMarket { get; private set; }

    public virtual IReadOnlyList<MaterialSupplierCost> MaterialSupplierCosts =>
        _materialSupplierCosts.ToList();

    //required EF Proxies
    protected Material() { }

    private Material(string code, string name, MaterialAttributes attributes, MaterialType materialType, RegionalMarket regionalMarket)
        : this()
    {
        Code = code;
        Name = name;
        Attributes = attributes;
        MaterialType = materialType;
        RegionalMarket = regionalMarket;
    }

    public static Result<Material> Create(string code, string name, MaterialAttributes attributes, MaterialType materialType,
        RegionalMarket regionalMarket)
    {
        var result = CanCreateOrUpdateMaterial(code, name, materialType, regionalMarket);
        if (result.IsFailure)
            return result.Error;

        return new Material(code.Trim(), name.Trim(), attributes, materialType, regionalMarket);
    }

    public Result UpdateMaterial(string code, string name, MaterialAttributes attributes, MaterialType materialType,
        RegionalMarket regionalMarket)
    {
        var result = CanCreateOrUpdateMaterial(code, name, materialType, regionalMarket);
        if (result.IsFailure)
            return result;

        Code = code.Trim();
        Name = name.Trim();
        Attributes = attributes;
        MaterialType = materialType;
        RegionalMarket = regionalMarket;

        return Result.Success();
    }

    //need to use ubiquitous language for this method
    public Result UpdateCost(IReadOnlyList<MaterialSupplierCost> materialSupplierCosts)
    {
        var notTheSameMaterialId = materialSupplierCosts.FirstOrDefault(x => x.MaterialCost.MaterialId != Id);
        if (notTheSameMaterialId is not null)
            return DomainErrors.Material.MaterialIdIsNotTheSame(notTheSameMaterialId.MaterialCost.MaterialId);
        
        foreach (var materialSupplierCost in materialSupplierCosts)
        {
            var materialSupplierCostExisted = _materialSupplierCosts
                .Find(x => x.MaterialCost.SupplierId == materialSupplierCost.MaterialCost.SupplierId);

            if (materialSupplierCostExisted is not null)
            {
                var setMaterialCostResult = materialSupplierCost.SetMaterialCost(materialSupplierCostExisted.MaterialCost.Price, materialSupplierCostExisted.MinQuantity, materialSupplierCostExisted.Surcharge);
                if (setMaterialCostResult.IsFailure)
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
                !materialSupplierCosts.Any(j => j.MaterialCost.SupplierId == x.MaterialCost.SupplierId));

        return Result.Success();
    }

    public Result<MaterialCost?> GetMaterialCost(SupplierId supplierId)
    {
        var materialSupplierCost = _materialSupplierCosts
            .Find(x => x.MaterialCost.SupplierId == supplierId);

        if (materialSupplierCost is null)
            return DomainErrors.MaterialCostManagement.NotExistSupplier(supplierId, Id);

        return materialSupplierCost.MaterialCost;
    }

    private static Result CanCreateOrUpdateMaterial(string code, string name, MaterialType materialType, RegionalMarket regionalMarket)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code))
            return DomainErrors.Material.EmptyCode;
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            return DomainErrors.Material.EmptyName;

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