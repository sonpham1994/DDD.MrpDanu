
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.Models;
using Domain.SharedKernel.ValueObjects;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands;

internal static class Extensions
{
    public static (SupplierId supplierId, byte currencyTypeId) ToTuple(this SupplierIdWithCurrencyTypeId input)
        => ((SupplierId)input.Id, input.CurrencyTypeId);
    public static IReadOnlyList<(SupplierId supplierId, byte currencyTypeId)> ToTuple(this IReadOnlyList<SupplierIdWithCurrencyTypeId> inputs)
        => inputs
        .Select(x => x.ToTuple())
        .ToList();

    public static (decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId) ToTuple(this MaterialSupplierCostCommand materialSupplierCost)
        => (materialSupplierCost.Price, materialSupplierCost.MinQuantity, materialSupplierCost.Surcharge, (SupplierId)materialSupplierCost.SupplierId);
    public static IReadOnlyList<(decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId)> ToTuple(this IReadOnlyList<MaterialSupplierCostCommand> materialSupplierCosts)
       => materialSupplierCosts
        .Select(x => x.ToTuple())
        .ToList();
}