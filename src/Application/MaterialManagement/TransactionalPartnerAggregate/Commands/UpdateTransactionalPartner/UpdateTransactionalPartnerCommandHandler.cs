using Application.Interfaces;
using Application.Interfaces.Messaging;
using Application.Interfaces.Queries;
using Application.Interfaces.Repositories;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.DomainClasses;
using Domain.SharedKernel.Base;
using MaterialManagementDomainErrors = Domain.MaterialManagement.DomainErrors;

namespace Application.MaterialManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;

internal sealed class UpdateTransactionalPartnerCommandHandler : ICommandHandler<UpdateTransactionalPartnerCommand>
{
    private readonly ITransactionalPartnerRepository _transactionalPartnerRepository;
    private readonly ITransactionalPartnerQuery _transactionalPartnerQuery;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateTransactionalPartnerCommandHandler(ITransactionalPartnerRepository transactionalPartnerRepository,
        ITransactionalPartnerQuery transactionalPartnerQuery
        , IUnitOfWork unitOfWork)
    {
        _transactionalPartnerRepository = transactionalPartnerRepository;
        _transactionalPartnerQuery = transactionalPartnerQuery;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(UpdateTransactionalPartnerCommand request, CancellationToken cancellationToken)
    {
        var contactInfo = ContactInformation.Create(request.TelNo, request.Email).Value;
        var transactionalPartner = await _transactionalPartnerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (transactionalPartner is null)
            return MaterialManagementDomainErrors.TransactionalPartner.NotFoundId(request.Id);
        if (await _transactionalPartnerQuery.ExistByContactInfoAsync(request.Id, contactInfo.Email, contactInfo.TelNo, cancellationToken))
            return MaterialManagementDomainErrors.ContactPersonInformation.TelNoOrEmailIsTaken;
        
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
        
        var result = transactionalPartner.Update
        (
            companyName, 
            taxNo, 
            website, 
            address,
            transactionalPartnerType, 
            currencyType, 
            location
        );
        if (result.IsFailure)
            return result.Error;
        
        transactionalPartner.UpdateContactPersonInfo(personName, contactInfo);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}