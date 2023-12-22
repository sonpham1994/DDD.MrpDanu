using Application.Interfaces;
using Application.Interfaces.Messaging;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.Enumerations;
using DomainErrors = Domain.MaterialManagement.DomainErrors;
using Application.Interfaces.Write;
using Application.Interfaces.Reads;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;

internal sealed class CreateTransactionalPartnerCommandHandler : ICommandHandler<CreateTransactionalPartnerCommand>
{
    private readonly ITransactionalPartnerRepository _transactionalPartnerRepository;
    private readonly ITransactionalPartnerQuery _transactionalPartnerQuery;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateTransactionalPartnerCommandHandler(ITransactionalPartnerRepository transactionalPartnerRepository,
        ITransactionalPartnerQuery transactionalPartnerQuery
        , IUnitOfWork unitOfWork)
    {
        _transactionalPartnerRepository = transactionalPartnerRepository;
        _transactionalPartnerQuery = transactionalPartnerQuery;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(CreateTransactionalPartnerCommand request, CancellationToken cancellationToken)
    {
        var contactInfo = ContactInformation.Create(request.TelNo, request.Email).Value;
        
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
        if (await _transactionalPartnerQuery.ExistByContactInfoAsync(contactInfo.Email, contactInfo.TelNo, cancellationToken))
            return DomainErrors.ContactPersonInformation.TelNoOrEmailIsTaken;
        
        var country = Country.FromId(request.Address.CountryId).Value;
        var taxNo = TaxNo.Create(request.TaxNo, country).Value;
        var companyName = CompanyName.Create(request.Name).Value;
        var personName = PersonName.Create(request.ContactPersonName).Value;
        var location = LocationType.FromId(request.LocationTypeId).Value;
        var address = Address.Create(request.Address.Street, request.Address.City, request.Address.District,
            request.Address.Ward, request.Address.ZipCode, country).Value;
        var website = Website.Create(request.Website).Value;
        var transactionalPartnerType = TransactionalPartnerType.FromId(request.TransactionalPartnerTypeId).Value;
        var currencyType = CurrencyType.FromId(request.CurrencyTypeId).Value;

        var transactionalPartner = TransactionalPartner.Create(companyName, taxNo, website, 
            personName, contactInfo, address,
            transactionalPartnerType, currencyType, location).Value;

        _transactionalPartnerRepository.Save(transactionalPartner);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}