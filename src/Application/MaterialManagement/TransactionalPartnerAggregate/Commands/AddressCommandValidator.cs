using Application.Extensions;
using Domain.Errors;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using FluentValidation;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands;

internal sealed class AddressCommandValidator : AbstractValidator<AddressCommand>
{
    public AddressCommandValidator()
    {
        RuleFor(x => x).NotNull()
            .WithErrorCode(MaterialManagementDomainErrors.TransactionalPartner.NullAddress.Code)
            .WithMessage(MaterialManagementDomainErrors.TransactionalPartner.NullAddress.Message);
        
        RuleFor(x => x.CountryId).MustBeEnumeration(x=> Country.FromId(x));
        
        RuleFor(x => x)
            .MustBeValueObject(x => 
                Address.Create(x.Street, x.City, x.District, x.Ward, x.ZipCode, Country.FromId(x.CountryId).Value));
    }
}