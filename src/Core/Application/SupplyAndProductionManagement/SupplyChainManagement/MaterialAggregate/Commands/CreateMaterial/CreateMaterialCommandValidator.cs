using Application.Extensions;
using Domain.SupplyChainManagement.MaterialAggregate;
using Domain.SharedKernel;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;
using FluentValidation;

namespace Application.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;

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
        
        // RuleFor(x => x)
        //     .MustBeEntityGuidStronglyTypedId
        //         <CreateMaterialCommand, 
        //             CreateMaterialCommand, 
        //             Material, 
        //             MaterialId>(x =>
        // {
        //     var result = ResultCombine.Create
        //     (
        //         MaterialAttributes.Create(x.ColorCode, x.Width, x.Weight, x.Unit, x.Varian),
        //         RegionalMarket.FromId(x.RegionalMarketId),
        //         MaterialType.FromId(x.MaterialTypeId)
        //     );
        //
        //     if (result.IsFailure)
        //         return result.Error;
        //
        //     return Result.Success();
        // var (materialAttributes, regionalMarket, materialType) = result.Value;
        //
        // return Material.Create(x.Code,
        //     x.Name,
        //     materialAttributes,
        //     materialType,
        //     regionalMarket);
        // });
    }
}