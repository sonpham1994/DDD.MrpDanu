using Application.Interfaces;
using Application.Interfaces.Messaging;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using SharedKernel;
using SharedKernel.Enumerations;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;
using Application.Interfaces.Writes.TransactionalPartnerWrite;
using SharedKernel.ValueObjects;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;

internal sealed class CreateTransactionalPartnerCommandHandler(
    ITransactionalPartnerRepository transactionalPartnerRepository,
    ITransactionalPartnerReadonlyRepository transactionalReadonlyRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateTransactionalPartnerCommand, TransactionalPartnerId>
{
    public async Task<Result<TransactionalPartnerId>> Handle(CreateTransactionalPartnerCommand request, CancellationToken cancellationToken)
    {
        var contactInfo = ContactInformation.Create(request.TelNo, request.Email).Value!;

        /*
         * for uniqueContact We delegate a part of invariant of Domain to Application layer. In DDD trilemma (CAP), we
         *  cannot achieve three properties of them, such as domain purity, completeness (AKA domain model
         *  encapsulation), and performance. In terms of performance, data in ContactPersonInformation are small, so
         *  we go for domain purity, and completeness.
         *
         * From FluentValidation_Explaining/7.DivingDeeperIntoDDDAndValidation/7.6.DDDTrilemma;
         * This approach is not always valid domain model, but according to Vladimir Khorikov's experience, domain
         *  logic fragmentation is a lesser evil than merging the responsibilities of domain modeling and communication
         *  with out‑of‑process dependencies.
         * Domain logic is the most important part in the application. It's also the most complex part of it. Mixing it
         *  with additional responsibility of talking to out‑of‑process dependencies, makes that logic's complexity grow
         *  even bigger, avoid this as much as possible. The domain layer should be exempted from all responsibilities
         *  other than the domain logic itself.
         *  
         *  We will put the existing contact info on query side, because this method don't need to return aggregate root, just
         *  checking contact info and return boolean data type. This approach will make your repositories clean 
         */
        if (await transactionalReadonlyRepository.ExistByContactInfoAsync(contactInfo.Email, contactInfo.TelNo, cancellationToken))
            return DomainErrors.ContactPersonInformation.TelNoOrEmailIsTaken;

        var country = Country.FromId(request.Address!.CountryId).Value!;
        var taxNo = TaxNo.Create(request.TaxNo, country).Value!;
        var companyName = CompanyName.Create(request.Name).Value!;
        var personName = PersonName.Create(request.ContactPersonName).Value!;
        var location = LocationType.FromId(request.LocationTypeId).Value!;
        var address = Address.Create(request.Address.Street, request.Address.City, request.Address.District,
            request.Address.Ward, request.Address.ZipCode, country).Value!;
        var transactionalPartnerType = TransactionalPartnerType.FromId(request.TransactionalPartnerTypeId).Value!;
        var currencyType = CurrencyType.FromId(request.CurrencyTypeId).Value!;

        var transactionalPartner = TransactionalPartner.Create(companyName, taxNo, request.Website,
            personName, contactInfo, address,
            transactionalPartnerType, currencyType, location).Value!;

        transactionalPartnerRepository.Save(transactionalPartner);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return transactionalPartner.Id;
    }
}