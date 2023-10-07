using Application.Extensions;
using Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;
using Domain.Errors;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel.Base;
using FluentValidation;

namespace Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;

internal sealed class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
{
    public CreateMaterialCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithErrorCode(GeneralDomainErrors.NullRequestBodyParameter.Code)
            .WithMessage(GeneralDomainErrors.NullRequestBodyParameter.Message);
        RuleFor(x => x.MaterialCosts)
            .SetValidator(new MaterialCostsValidator())
            .When(x => x.MaterialCosts.Any());

        RuleFor(x => x).MustBeEntity(x =>
        {
            var result = ResultCombine.Create
            (
                MaterialAttributes.Create(x.Name, x.ColorCode, x.Width, x.Weight, x.Unit, x.Varian),
                RegionalMarket.FromId(x.RegionalMarketId),
                MaterialType.FromId(x.MaterialTypeId)
            );

            if (result.IsFailure)
                return result.Error;
            
            var (materialAttributes, regionalMarket, materialType) = result.Value;

            return Material.Create(x.Code,
                materialAttributes,
                materialType,
                regionalMarket);
        });
    }
}