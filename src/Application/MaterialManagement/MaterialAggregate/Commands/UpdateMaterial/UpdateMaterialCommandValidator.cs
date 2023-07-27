using Application.Extensions;
using Application.Interfaces;
using Domain.Errors;
using Domain.MaterialManagement.MaterialAggregate;
using FluentValidation;

namespace Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;

internal sealed class UpdateMaterialCommandValidator : AbstractValidator<UpdateMaterialCommand>
{
    public UpdateMaterialCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithErrorCode(GeneralDomainErrors.NullRequestBodyParameter.Code)
            .WithMessage(GeneralDomainErrors.NullRequestBodyParameter.Message);
        RuleFor(x => x.Id).NotEmpty()
            .WithErrorCode(MaterialManagementDomainErrors.Material.EmptyId.Code)
            .WithMessage(MaterialManagementDomainErrors.Material.EmptyId.Message);
        
        RuleFor(x => x)
            .MustBeValueObject(
                x => MaterialAttributes.Create(x.Name, 
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