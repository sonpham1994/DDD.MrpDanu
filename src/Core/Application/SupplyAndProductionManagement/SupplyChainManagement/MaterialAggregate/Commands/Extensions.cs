
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.Models;
using Domain.SharedKernel.ValueObjects;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands;

internal static class Extensions
{
    public static (SupplierId supplierId, byte currencyTypeId) ToTuple(this SupplierIdWithCurrencyTypeId input)
        => ((SupplierId)input.Id, input.CurrencyTypeId);
    public static IReadOnlyList<(SupplierId supplierId, byte currencyTypeId)> ToTuple(this IReadOnlyList<SupplierIdWithCurrencyTypeId> inputs)
    {
        if (inputs is null)
        {
            return Array.Empty<(SupplierId supplierId, byte currencyTypeId)>();
        }

        return inputs
        .Select(x => x.ToTuple())
        .ToList();
    }
       

    public static (decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId) ToTuple(this MaterialSupplierCostCommand materialSupplierCost)
    {
        if (materialSupplierCost is null)
        {
            return default;
        }

        return (materialSupplierCost.Price, materialSupplierCost.MinQuantity, materialSupplierCost.Surcharge, (SupplierId)materialSupplierCost.SupplierId);
    }
        
    public static IReadOnlyList<(decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId)> ToTuple(this IReadOnlyList<MaterialSupplierCostCommand> materialSupplierCosts)
    {
        if (materialSupplierCosts is null)
            return Array.Empty<(decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId)>();

        return materialSupplierCosts
        .Select(x => x.ToTuple())
        .ToList();
    }
       
}