using Domain.SupplyAndProductionManagement.SupplyChainManagement;
using FluentValidation;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands;

internal sealed class MaterialCostsValidator : AbstractValidator<IEnumerable<MaterialSupplierCostCommand>>
{
    public MaterialCostsValidator()
    {
        RuleFor(x => x).ForEach(x =>
        {
            x.NotNull()
            .WithErrorCode(DomainErrors.MaterialSupplierCost.NullMaterialCost.Code)
            .WithMessage(DomainErrors.MaterialSupplierCost.NullMaterialCost.Message);
        });

        RuleFor(x => x).Must(ContainsSupplierId)
            .WithErrorCode(DomainErrors.MaterialSupplierCost.EmptySupplierId.Code)
            .WithMessage(DomainErrors.MaterialSupplierCost.EmptySupplierId.Message);
    }

    private bool ContainsSupplierId(IEnumerable<MaterialSupplierCostCommand> materialCost)
    {
        return materialCost.All(x => x.SupplierId != Guid.Empty);
    }
}