using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Bogus;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Shouldly;
using MinimalApi.ApiResponses;
using SharedKernel.Enumerations;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

[Collection(CollectionEndpointName.TransactionalPartnerEndpoint)]
public abstract class SharedTests(EndpointFactory endpointFactory) : Tests.Integration.SharedTests(endpointFactory)
{
    public const string Endpoint = "supply-chain-management/transactional-partners";
    protected override string BaseEndpoint => Endpoint;
    protected override string Url => $"{Host}/{Endpoint}";

    protected static Faker<UpdateTransactionalPartnerCommand> UpdateTransactionalPartnerFaker(Guid id) => new Faker<UpdateTransactionalPartnerCommand>()
        .CustomInstantiator(f=> new UpdateTransactionalPartnerCommand(
            id,
            f.Name.FirstName(),
            f.Random.Long(1000000000, 9999999999).ToString(),
            f.Internet.Url(),
            f.Person.FirstName,
            f.Random.Long(1000000000, 9999999999).ToString(),
            f.Person.Email,
            CreateAddressFaker.Generate(),
            f.Random.Byte(TransactionalPartnerType.List[0].Id, TransactionalPartnerType.List[^1].Id),
            CurrencyType.VND.Id,
            LocationType.Domestic.Id
        ));

    protected static readonly ApiError TelNoOrEmailIsTaken = DomainErrors.ContactPersonInformation.TelNoOrEmailIsTaken.ToApiError();
    protected static readonly ApiError NotFoundId = DomainErrors.TransactionalPartner.NotFoundId(Guid.NewGuid()).ToApiError();

    protected async Task<TransactionalPartnerResponse> GetTransactionalPartnerByIdAsync(Guid id)
    {
        var response = await HttpClient.GetAsync($"{Endpoint}/{id}");
        var result  = await ReadFromJsonAsync<TransactionalPartnerResponse>(response);
        
        CreatedIds.Add(id);
        
        return result!.Result;
    }

    protected async Task<IReadOnlyList<TransactionalPartnersResponse>> GetAllTransactionalPartnersAsync()
    {
        var response = await HttpClient.GetAsync($"{Endpoint}");
        var result = await ReadFromJsonAsync<IReadOnlyList<TransactionalPartnersResponse>>(response);
        
        return result!.Result;
    }

    protected async Task EnsureEqualAsync(Guid id, CreateTransactionalPartnerCommand request)
    {
        var transactionalPartner = await GetTransactionalPartnerByIdAsync(id);
        transactionalPartner.ShouldNotBeNull();
        transactionalPartner.Id.ShouldBe(id);
        transactionalPartner.Name.ShouldBe(request.Name);
        transactionalPartner.Website.ShouldBe(request.Website);
        transactionalPartner.Address.City.ShouldBe(request.Address!.City);
        transactionalPartner.Address.Country.Id.ShouldBe(request.Address.CountryId);
        transactionalPartner.Address.District.ShouldBe(request.Address.District);
        transactionalPartner.Address.Street.ShouldBe(request.Address.Street);
        transactionalPartner.Address.Ward.ShouldBe(request.Address.Ward);
        transactionalPartner.Address.ZipCode.ShouldBe(request.Address.ZipCode);
        transactionalPartner.CurrencyType.Id.ShouldBe(request.CurrencyTypeId);
        transactionalPartner.TransactionalPartnerType.Id.ShouldBe(request.TransactionalPartnerTypeId);
        transactionalPartner.LocationType.Id.ShouldBe(request.LocationTypeId);
        transactionalPartner.TaxNo.ShouldBe(request.TaxNo);
        transactionalPartner.ContactPersonInformation.Name.ShouldBe(request.ContactPersonName);
        transactionalPartner.ContactPersonInformation.Email.ShouldBe(request.Email);
        transactionalPartner.ContactPersonInformation.TelNo.ShouldBe(request.TelNo);
    }
    
    protected async Task EnsureEqualAsync(Guid id, UpdateTransactionalPartnerCommand request)
    {
        var transactionalPartner = await GetTransactionalPartnerByIdAsync(id);
        transactionalPartner.ShouldNotBeNull();
        transactionalPartner.Id.ShouldBe(request.Id);
        transactionalPartner.Name.ShouldBe(request.Name);
        transactionalPartner.Website.ShouldBe(request.Website);
        transactionalPartner.Address.City.ShouldBe(request.Address!.City);
        transactionalPartner.Address.Country.Id.ShouldBe(request.Address.CountryId);
        transactionalPartner.Address.District.ShouldBe(request.Address.District);
        transactionalPartner.Address.Street.ShouldBe(request.Address.Street);
        transactionalPartner.Address.Ward.ShouldBe(request.Address.Ward);
        transactionalPartner.Address.ZipCode.ShouldBe(request.Address.ZipCode);
        transactionalPartner.CurrencyType.Id.ShouldBe(request.CurrencyTypeId);
        transactionalPartner.TransactionalPartnerType.Id.ShouldBe(request.TransactionalPartnerTypeId);
        transactionalPartner.LocationType.Id.ShouldBe(request.LocationTypeId);
        transactionalPartner.TaxNo.ShouldBe(request.TaxNo);
        transactionalPartner.ContactPersonInformation.Name.ShouldBe(request.ContactPersonName);
        transactionalPartner.ContactPersonInformation.Email.ShouldBe(request.Email);
        transactionalPartner.ContactPersonInformation.TelNo.ShouldBe(request.TelNo);
    }
}