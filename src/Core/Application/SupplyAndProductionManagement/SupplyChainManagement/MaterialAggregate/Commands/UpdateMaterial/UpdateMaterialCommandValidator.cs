using Application.Extensions;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using FluentValidation;
using DomainErrorsShared = Domain.SharedKernel.DomainErrors;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;

internal sealed class UpdateMaterialCommandValidator : AbstractValidator<UpdateMaterialCommand>
{
    public UpdateMaterialCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithErrorCode(DomainErrorsShared.NullRequestBodyParameter.Code)
            .WithMessage(DomainErrorsShared.NullRequestBodyParameter.Message);
        RuleFor(x => x.Id).NotEmpty()
            .WithErrorCode(DomainErrors.Material.EmptyId.Code)
            .WithMessage(DomainErrors.Material.EmptyId.Message);

        RuleFor(x => x.Code)
            .MustBeValueObject(
                x => MaterialCode.Create(x));
        RuleFor(x => x.Name)
            .MustBeValueObject(
                x => MaterialName.Create(x));
        
        RuleFor(x => x)
            .MustBeValueObject(
                x => MaterialAttributes.Create(
                    x.ColorCode,
                    x.Width,
                    x.Weight,
                    x.Unit,
                    x.Varian));

        RuleFor(x => x)
            .MustBeEnumeration(x => RegionalMarket.FromId(x.RegionalMarketId));
        RuleFor(x => x)
            .MustBeEnumeration(x => MaterialType.FromId(x.MaterialTypeId));

        RuleFor(x => x.MaterialCosts)
            .SetValidator(new MaterialCostsValidator())
            .When(x => x.MaterialCosts is not null && x.MaterialCosts.Count > 0);
    }
}