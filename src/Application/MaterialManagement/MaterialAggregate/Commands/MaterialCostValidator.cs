using Domain.Errors;
using FluentValidation;

namespace Application.MaterialManagement.MaterialAggregate.Commands;

internal sealed class MaterialCostsValidator : AbstractValidator<IEnumerable<MaterialCostCommand>>
{
    public MaterialCostsValidator()
    {
        RuleFor(x => x).ForEach(x =>
        {
            x.NotNull()
            .WithErrorCode(MaterialManagementDomainErrors.MaterialCostManagement.NullMaterialCost.Code)
            .WithMessage(MaterialManagementDomainErrors.MaterialCostManagement.NullMaterialCost.Message);
        });

        RuleFor(x => x).Must(ContainsSupplierId)
            .WithErrorCode(MaterialManagementDomainErrors.MaterialCostManagement.EmptySupplierId.Code)
            .WithMessage(MaterialManagementDomainErrors.MaterialCostManagement.EmptySupplierId.Message);
    }

    private bool ContainsSupplierId(IEnumerable<MaterialCostCommand> materialCost)
    {
        return materialCost.All(x => x.SupplierId != Guid.Empty);
    }
}