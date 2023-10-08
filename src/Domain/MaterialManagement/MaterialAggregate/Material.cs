using Domain.Errors;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;
using Domain.Extensions;
using System.Text.Json;

namespace Domain.MaterialManagement.MaterialAggregate;

public class Material : AggregateRoot
{
    // this largest number of materials depends on domain, it doesn't make sense if we have the infinite number.
    // The number 20 is the number you should discuss with domain experts, if the domain experts say that 
    // the number 20 is the maximum, you put it here. (Write UT for this case)
    private const byte MaxNumberOfMaterialCosts = 20;
    private readonly List<MaterialCostManagement> _materialCostManagements = new(MaxNumberOfMaterialCosts);

    public string Code { get; private set; }
    public string CodeUnique { get; }
    public MaterialAttributes Attributes { get; private set; }
    public virtual MaterialType MaterialType { get; private set; }
    public virtual RegionalMarket RegionalMarket { get; private set; }

    public virtual IReadOnlyList<MaterialCostManagement> MaterialCostManagements =>
        _materialCostManagements.ToList();

    //required EF Proxies
    protected Material() { }

    private Material(string code, MaterialAttributes attributes, MaterialType materialType, RegionalMarket regionalMarket)
    : this()
    {
        Code = code;
        CodeUnique = attributes.ToUniqueCode();
        Attributes = attributes;
        MaterialType = materialType;
        RegionalMarket = regionalMarket;
    }

    public static Result<Material> Create(string code, MaterialAttributes attributes, MaterialType materialType,
        RegionalMarket regionalMarket)
    {
        var result = CanCreateOrUpdateMaterial(code, materialType, regionalMarket);
        if (result.IsFailure)
            return result.Error;

        return new Material(code.Trim(), attributes, materialType, regionalMarket);
    }

    public Result UpdateMaterial(string code, MaterialAttributes attributes, MaterialType materialType,
        RegionalMarket regionalMarket)
    {
        var result = CanCreateOrUpdateMaterial(code, materialType, regionalMarket);
        if (result.IsFailure)
            return result;

        Code = code.Trim();
        Attributes = attributes;
        MaterialType = materialType;
        RegionalMarket = regionalMarket;

        return Result.Success();
    }

    //need to use ubiquitous language for this method
    public Result UpdateCost(IEnumerable<MaterialCostManagement> materialCosts)
    {
        var supplierDuplication = materialCosts.ItemDuplication(x => x.TransactionalPartner);
        if (supplierDuplication is not null)
            return MaterialManagementDomainErrors.MaterialCostManagement.DuplicationSupplierId(supplierDuplication.Id);

        foreach (var materialCost in materialCosts)
        {
            var materialCostManagement = _materialCostManagements
                .FirstOrDefault(x => x.TransactionalPartner == materialCost.TransactionalPartner);

            if (materialCostManagement is not null)
            {
                var setMaterialCostResult = materialCostManagement.SetMaterialCost(materialCost.Price, materialCost.MinQuantity, materialCost.Surcharge);
                if (setMaterialCostResult.IsFailure)
                    return setMaterialCostResult;
            }
            else if (materialCostManagement is null)
            {
                if (_materialCostManagements.Count + 1 > MaxNumberOfMaterialCosts)
                    return MaterialManagementDomainErrors.Material.ExceedsMaxNumberOfMaterialCosts;
                
                _materialCostManagements.Add(materialCost);
            }
        }

        _materialCostManagements
            .RemoveAll(x =>
                !materialCosts.Any(j => j.TransactionalPartner == x.TransactionalPartner));

        return Result.Success();
    }

    public Result<MaterialCostManagement?> GetMaterialCost(TransactionalPartner supplier)
    {
        var materialCost = _materialCostManagements.FirstOrDefault(x => x.TransactionalPartner == supplier);

        if (materialCost is null)
            return MaterialManagementDomainErrors.MaterialCostManagement.NotExistSupplier(supplier.Id, Id);

        return materialCost;
    }

    private static Result CanCreateOrUpdateMaterial(string code, MaterialType materialType, RegionalMarket regionalMarket)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code))
            return MaterialManagementDomainErrors.Material.EmptyCode;

        if (materialType == MaterialType.Material && regionalMarket != RegionalMarket.None)
            return MaterialManagementDomainErrors.Material.InvalidMaterialType;

        if (materialType == MaterialType.Subassemblies && regionalMarket == RegionalMarket.None)
            return MaterialManagementDomainErrors.Material.InvalidSubassembliesType;

        return Result.Success();
    }
}

public class MaterialForLutionAudit : AggregateRoot, IAuditTableForSolution1, IAuditTableForSolution3
{
    private readonly List<MaterialCostManagement> _materialCostManagements = new();

    public string Code { get; private set; }
    public string CodeUnique { get; }
    public MaterialAttributes Attributes { get; private set; }
    public virtual MaterialType MaterialType { get; private set; }
    public virtual RegionalMarket RegionalMarket { get; private set; }

    public virtual IReadOnlyList<MaterialCostManagement> MaterialCostManagements =>
        _materialCostManagements;

    //required EF Proxies
    protected MaterialForLutionAudit() { }

    private MaterialForLutionAudit(string code, MaterialAttributes attributes, MaterialType materialType, RegionalMarket regionalMarket)
    : this()
    {
        Code = code;
        CodeUnique = attributes.ToUniqueCode();
        Attributes = attributes;
        MaterialType = materialType;
        RegionalMarket = regionalMarket;
    }

    public static Result<MaterialForLutionAudit> Create(string code, MaterialAttributes attributes, MaterialType materialType,
        RegionalMarket regionalMarket)
    {
        var result = CanCreateOrUpdateMaterial(code, materialType, regionalMarket);
        if (result.IsFailure)
            return result.Error;

        return new MaterialForLutionAudit(code.Trim(), attributes, materialType, regionalMarket);
    }

    public Result UpdateMaterial(string code, MaterialAttributes attributes, MaterialType materialType,
        RegionalMarket regionalMarket)
    {
        var result = CanCreateOrUpdateMaterial(code, materialType, regionalMarket);
        if (result.IsFailure)
            return result;

        Code = code.Trim();
        Attributes = attributes;
        MaterialType = materialType;
        RegionalMarket = regionalMarket;

        return Result.Success();
    }

    //need to use ubiquitous language for this method
    public Result UpdateCost(IEnumerable<MaterialCostManagement> materialCosts)
    {
        var supplierDuplication = materialCosts.ItemDuplication(x => x.TransactionalPartner);
        if (supplierDuplication is not null)
            return MaterialManagementDomainErrors.MaterialCostManagement.DuplicationSupplierId(supplierDuplication.Id);

        foreach (var materialCost in materialCosts)
        {
            var materialCostManagement = _materialCostManagements
                .FirstOrDefault(x => x.TransactionalPartner == materialCost.TransactionalPartner);

            if (materialCostManagement is not null)
            {
                var setMaterialCostResult = materialCostManagement.SetMaterialCost(materialCost.Price, materialCost.MinQuantity, materialCost.Surcharge);
                if (setMaterialCostResult.IsFailure)
                    return setMaterialCostResult;
            }
            else if (materialCostManagement is null)
            {
                _materialCostManagements.Add(materialCost);
            }
        }

        _materialCostManagements
            .RemoveAll(x =>
                !materialCosts.Any(j => j.TransactionalPartner == x.TransactionalPartner));

        return Result.Success();
    }

    public Result<MaterialCostManagement?> GetMaterialCost(TransactionalPartner supplier)
    {
        var materialCost = _materialCostManagements.FirstOrDefault(x => x.TransactionalPartner == supplier);

        if (materialCost is null)
            return MaterialManagementDomainErrors.MaterialCostManagement.NotExistSupplier(supplier.Id, Id);

        return materialCost;
    }

    private static Result CanCreateOrUpdateMaterial(string code, MaterialType materialType, RegionalMarket regionalMarket)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code))
            return MaterialManagementDomainErrors.Material.EmptyCode;

        if (materialType == MaterialType.Material && regionalMarket != RegionalMarket.None)
            return MaterialManagementDomainErrors.Material.InvalidMaterialType;

        if (materialType == MaterialType.Subassemblies && regionalMarket == RegionalMarket.None)
            return MaterialManagementDomainErrors.Material.InvalidSubassembliesType;

        return Result.Success();
    }

    public (string, string) Serialize()
    {
        var id = Id.ToString();
        var auditData = new
        {
            Id = id,
            Code = Code,
            CodeUnique = CodeUnique,
            Name = Attributes.Name,
            ColorCode = Attributes.ColorCode,
            Width = Attributes.Width,
            Weight = Attributes.Weight,
            Unit = Attributes.Unit,
            Varian = Attributes.Varian,
            MaterialType = MaterialType,
            RegionalMarket = RegionalMarket,
            MaterialCostManagements = _materialCostManagements.Select(x => new
            {
                Id = x.Id,
                Price = x.Price.Value,
                x.MinQuantity,
                Surcharge = x.Surcharge.Value,
                CurrencyType = x.Price.CurrencyType,
                Supplier = new
                {
                    Id = x.TransactionalPartner.Id, // this one will cause performance issue due to Lazy loading
                    Name = x.TransactionalPartner.Name.Value
                }

            }).ToList()
        };
        var json = JsonSerializer.Serialize(auditData);

        return (id, json);
    }
}