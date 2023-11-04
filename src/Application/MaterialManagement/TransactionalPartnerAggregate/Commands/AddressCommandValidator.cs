using Application.Extensions;
using Domain.MaterialManagement;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using FluentValidation;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands;

internal sealed class AddressCommandValidator : AbstractValidator<AddressCommand>
{
    public AddressCommandValidator()
    {
        RuleFor(x => x).NotNull()
            .WithErrorCode(DomainErrors.TransactionalPartner.NullAddress.Code)
            .WithMessage(DomainErrors.TransactionalPartner.NullAddress.Message);
        
        RuleFor(x => x.CountryId).MustBeEnumeration(x=> Country.FromId(x));
        
        RuleFor(x => x)
            .MustBeValueObject(x => 
                Address.Create(x.Street, x.City, x.District, x.Ward, x.ZipCode, Country.FromId(x.CountryId).Value));
    }
}