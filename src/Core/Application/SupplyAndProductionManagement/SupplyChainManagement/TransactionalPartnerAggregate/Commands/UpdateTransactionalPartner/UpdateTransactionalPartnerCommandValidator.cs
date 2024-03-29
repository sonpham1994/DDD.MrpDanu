using Application.Extensions;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Enumerations;
using FluentValidation;
using DomainErrorsShared = Domain.SharedKernel.DomainErrors;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;


namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;

internal sealed class UpdateTransactionalPartnerCommandValidator : AbstractValidator<UpdateTransactionalPartnerCommand>
{
    public UpdateTransactionalPartnerCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithErrorCode(DomainErrorsShared.NullRequestBodyParameter.Code)
            .WithMessage(DomainErrorsShared.NullRequestBodyParameter.Message);
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode(DomainErrors.TransactionalPartner.NotFound.Code)
            .WithMessage(DomainErrors.TransactionalPartner.NotFound.Message);

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
            .MustBeEnumeration(x => LocationType.FromId(x));

        RuleFor(x => x.TransactionalPartnerTypeId)
            .MustBeEnumeration(x => TransactionalPartnerType.FromId(x));

        RuleFor(x => x)
            .MustBeEnumeration(x
                => CurrencyType.FromId(x.CurrencyTypeId));

        RuleFor(x => x.Website).MustBeValueObject(Website.Create);
    }
}