using Application.MaterialManagement.MaterialAggregate.Commands.Models;
using Domain.SharedKernel.ValueObjects;

namespace Application.MaterialManagement.MaterialAggregate.Commands;

public static class Extensions
{
    public static (SupplierId supplierId, byte currencyTypeId) ToTuple(this SupplierIdWithCurrencyTypeId input)
        => ((SupplierId)input.Id, input.CurrencyTypeId);
    public static IReadOnlyList<(SupplierId supplierId, byte currencyTypeId)> ToTuple(this IReadOnlyList<SupplierIdWithCurrencyTypeId> inputs)
        => inputs
        .Select(x => x.ToTuple())
        .ToList();

    public static (decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId) ToTuple(this MaterialCostCommand materialCost)
        => (materialCost.Price, materialCost.MinQuantity, materialCost.Surcharge, (SupplierId)materialCost.SupplierId);
    public static IReadOnlyList<(decimal price, uint minQuantity, decimal surcharge, SupplierId supplierId)> ToTuple(this IReadOnlyList<MaterialCostCommand> materialCosts)
       => materialCosts
        .Select(x => x.ToTuple())
        .ToList();
}