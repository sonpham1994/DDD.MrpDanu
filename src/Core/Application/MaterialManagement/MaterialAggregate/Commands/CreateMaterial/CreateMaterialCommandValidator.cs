using Application.Extensions;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel;
using Domain.SharedKernel.Base;
using FluentValidation;

namespace Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;

internal sealed class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
{
    public CreateMaterialCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithErrorCode(DomainErrors.NullRequestBodyParameter.Code)
            .WithMessage(DomainErrors.NullRequestBodyParameter.Message);
        RuleFor(x => x.MaterialCosts)
            .SetValidator(new MaterialCostsValidator())
            .When(x => x.MaterialCosts.Any());

        RuleFor(x => x).MustBeEntityGuidStronglyTypedId<>(x =>
        {
            var result = ResultCombine.Create
            (
                MaterialAttributes.Create(x.ColorCode, x.Width, x.Weight, x.Unit, x.Varian),
                RegionalMarket.FromId(x.RegionalMarketId),
                MaterialType.FromId(x.MaterialTypeId)
            );

            if (result.IsFailure)
                return result.Error;
            
            var (materialAttributes, regionalMarket, materialType) = result.Value;

            return Material.Create(x.Code,
                x.Name,
                materialAttributes,
                materialType,
                regionalMarket);
        });
    }
}