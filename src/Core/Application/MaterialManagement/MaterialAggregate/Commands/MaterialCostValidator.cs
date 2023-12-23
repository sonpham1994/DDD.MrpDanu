using Domain.MaterialManagement;
using FluentValidation;

namespace Application.MaterialManagement.MaterialAggregate.Commands;

internal sealed class MaterialCostsValidator : AbstractValidator<IEnumerable<MaterialSupplierCostCommand>>
{
    public MaterialCostsValidator()
    {
        RuleFor(x => x).ForEach(x =>
        {
            x.NotNull()
            .WithErrorCode(DomainErrors.MaterialCostManagement.NullMaterialCost.Code)
            .WithMessage(DomainErrors.MaterialCostManagement.NullMaterialCost.Message);
        });

        RuleFor(x => x).Must(ContainsSupplierId)
            .WithErrorCode(DomainErrors.MaterialCostManagement.EmptySupplierId.Code)
            .WithMessage(DomainErrors.MaterialCostManagement.EmptySupplierId.Message);
    }

    private bool ContainsSupplierId(IEnumerable<MaterialSupplierCostCommand> materialCost)
    {
        return materialCost.All(x => x.SupplierId != Guid.Empty);
    }
}