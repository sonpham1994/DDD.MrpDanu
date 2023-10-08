using Application.Extensions;
using Domain.Errors;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel;
using FluentValidation;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;

internal sealed class UpdateTransactionalPartnerCommandValidator : AbstractValidator<UpdateTransactionalPartnerCommand>
{
    public UpdateTransactionalPartnerCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithErrorCode(GeneralDomainErrors.NullRequestBodyParameter.Code)
            .WithMessage(GeneralDomainErrors.NullRequestBodyParameter.Message);
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode(MaterialManagementDomainErrors.TransactionalPartner.NotFound.Code)
            .WithMessage(MaterialManagementDomainErrors.TransactionalPartner.NotFound.Message);
        
        RuleFor(x => x.Address).SetValidator(new AddressCommandValidator());

        RuleFor(x => x)
            .MustBeValueObject(x 
                => TaxNo.Create(x.TaxNo, Country.FromId(x.Address.CountryId).Value));

        RuleFor(x => x.Name)
            .MustBeValueObject(CompanyName.Create);
        
        RuleFor(x => x.ContactPersonName)
            .MustBeValueObject(PersonName.Create);
        
        RuleFor(x => x)
            .MustBeValueObject(x
                => ContactInformation.Create(x.TelNo, x.Email));
        
        RuleFor(x => x.LocationTypeId)
            .MustBeEnumeration(x=> LocationType.FromId(x));
        
        RuleFor(x => x.TransactionalPartnerTypeId)
            .MustBeEnumeration(x=> TransactionalPartnerType.FromId(x));
        
        RuleFor(x => x)
            .MustBeEnumeration(x 
                => CurrencyType.FromId(x.CurrencyTypeId));

        RuleFor(x => x.Website).MustBeValueObject(Website.Create);
    }
}