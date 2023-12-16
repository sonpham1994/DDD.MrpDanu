using Application.Extensions;
using Domain.MaterialManagement.MaterialAggregate;
using FluentValidation;
using DomainErrorsShared = Domain.SharedKernel.DomainClasses.DomainErrors;
using DomainErrors = Domain.MaterialManagement.DomainErrors;

namespace Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;

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
            .When(x => x.MaterialCosts.Any());
    }
}