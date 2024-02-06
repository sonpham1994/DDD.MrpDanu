using Application.Extensions;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;
using FluentValidation;
using DomainErrorsShared = Domain.SharedKernel.DomainErrors;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;

internal sealed class CreateTransactionalPartnerCommandValidator : AbstractValidator<CreateTransactionalPartnerCommand>
{
    public CreateTransactionalPartnerCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithErrorCode(DomainErrorsShared.NullRequestBodyParameter.Code)
            .WithMessage(DomainErrorsShared.NullRequestBodyParameter.Message);

        RuleFor(x => x)
            .MustBeEntityGuidStronglyTypedId<CreateTransactionalPartnerCommand,
                CreateTransactionalPartnerCommand,
                TransactionalPartner, TransactionalPartnerId>(x =>
        {
            if (x.Address is null)
                return DomainErrors.TransactionalPartner.NullAddress;

            var result1 = ResultCombine.Create
            (
                Country.FromId(x.Address.CountryId),
                CompanyName.Create(x.Name),
                PersonName.Create(x.ContactPersonName),
                ContactInformation.Create(x.TelNo, x.Email),
                LocationType.FromId(x.LocationTypeId),
                TransactionalPartnerType.FromId(x.TransactionalPartnerTypeId),
                CurrencyType.FromId(x.CurrencyTypeId),
                Website.Create(x.Website)
            );

            if (result1.IsFailure)
                return result1.Error;
            var (country, companyName, personName, contactInfo, locationType, transactionalPartnerType, currencyType, website) = result1.Value;

            var result2 = ResultCombine.Create
            (
                Address.Create(x.Address.Street, x.Address.City, x.Address.District, x.Address.Ward, x.Address.ZipCode, country),
                TaxNo.Create(x.TaxNo, country)
            );

            if (result2.IsFailure)
                return result2.Error;

            var (address, taxNo) = result2.Value;

            return TransactionalPartner.Create(companyName, taxNo, website,
                personName, contactInfo, address,
                transactionalPartnerType, currencyType, locationType);
        });
    }

    // public CreateTransactionalPartnerCommandValidator()
    // {
    //     RuleFor(x => x)
    //         .NotNull()
    //         .WithErrorCode(DomainErrors.General.NullRequestBodyParameter.Code)
    //         .WithMessage(DomainErrors.General.NullRequestBodyParameter.Message);
    //
    //     RuleFor(x => x).MustBeEntity(x =>
    //     {
    //         if (x.Address is null)
    //             return DomainErrors.TransactionalPartner.NullAddress;
    //
    //         var country = Country.FromId(x.Address.CountryId);
    //         if (country.IsFailure)
    //             return country.Error;
    //         
    //         var companyName = CompanyName.Create(x.Name);
    //         if (companyName.IsFailure)
    //             return companyName.Error;
    //         
    //         var personName = PersonName.Create(x.ContactPersonName);
    //         if (personName.IsFailure)
    //             return personName.Error;
    //         
    //         var contactInfo = ContactInformation.Create(x.TelNo, x.Email);
    //         if (contactInfo.IsFailure)
    //             return contactInfo.Error;
    //         
    //         var location = LocationType.FromId(x.LocationTypeId);
    //         if (location.IsFailure)
    //             return location.Error;
    //
    //         var type = TransactionalPartnerType.FromId(x.TransactionalPartnerTypeId);
    //         if (type.IsFailure)
    //             return type.Error;
    //
    //         var currency = CurrencyType.FromId(x.CurrencyTypeId);
    //         if (currency.IsFailure)
    //             return currency.Error;
    //         
    //         var website = Website.Create(x.Website);
    //         if (website.IsFailure)
    //             return website.Error;
    //
    //         var address = Address.Create(x.Address.Street, x.Address.City, x.Address.District, 
    //             x.Address.Ward, x.Address.ZipCode, country.Value);
    //         if (address.IsFailure)
    //             return address.Error;
    //         
    //         var taxNo = TaxNo.Create(x.TaxNo, country.Value);
    //         if (taxNo.IsFailure)
    //             return taxNo.Error;
    //         
    //         var personContact = new ContactPersonInformation(personName.Value, contactInfo.Value);
    //         
    //         return TransactionalPartner.Create(companyName.Value, taxNo.Value, website.Value, 
    //             personContact, address.Value,
    //             type.Value, currency.Value, location.Value);
    //     });
    // }
}