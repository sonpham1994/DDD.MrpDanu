using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Bogus;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using FluentAssertions;
using MinimalApi.ApiResponses;
using SharedKernel.Enumerations;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

[Collection(CollectionEndpointName.TransactionalPartnerEndpoint)]
public abstract class SharedTests(HttpClient httpClient) : Tests.Integration.SharedTests(httpClient)
{
    public const string Endpoint = "supply-chain-management/transactional-partners";
    protected override string BaseEndpoint => Endpoint;
    protected override string Url => $"{Host}/{Endpoint}";

    public static Faker<UpdateTransactionalPartnerCommand> UpdateTransactionalPartnerFaker(Guid id) => new Faker<UpdateTransactionalPartnerCommand>()
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

    public static readonly ApiError TelNoOrEmailIsTaken = DomainErrors.ContactPersonInformation.TelNoOrEmailIsTaken.ToApiError();
    public static readonly ApiError NotFoundId = DomainErrors.TransactionalPartner.NotFoundId(Guid.NewGuid()).ToApiError();
    
    public async Task<TransactionalPartnerResponse> GetTransactionalPartnerByIdAsync(Guid id)
    {
        var response = await HttpClient.GetAsync($"{Endpoint}/{id}");
        var result  = await ReadFromJsonAsync<TransactionalPartnerResponse>(response);
        
        CreatedIds.Add(id);
        
        return result!.Result;
    }
    
    public async Task<IReadOnlyList<TransactionalPartnersResponse>> GetAllTransactionalPartnersAsync()
    {
        var response = await HttpClient.GetAsync($"{Endpoint}");
        var result = await ReadFromJsonAsync<IReadOnlyList<TransactionalPartnersResponse>>(response);
        
        return result!.Result;
    }

    protected async Task EnsureEqualAsync(Guid id, CreateTransactionalPartnerCommand request)
    {
        var transactionalPartner = await GetTransactionalPartnerByIdAsync(id);
        transactionalPartner.Should().NotBeNull();
        transactionalPartner.Id.Should().Be(id);
        transactionalPartner.Name.Should().Be(request.Name);
        transactionalPartner.Website.Should().Be(request.Website);
        transactionalPartner.Address.City.Should().Be(request.Address!.City);
        transactionalPartner.Address.Country.Id.Should().Be(request.Address.CountryId);
        transactionalPartner.Address.District.Should().Be(request.Address.District);
        transactionalPartner.Address.Street.Should().Be(request.Address.Street);
        transactionalPartner.Address.Ward.Should().Be(request.Address.Ward);
        transactionalPartner.Address.ZipCode.Should().Be(request.Address.ZipCode);
        transactionalPartner.CurrencyType.Id.Should().Be(request.CurrencyTypeId);
        transactionalPartner.TransactionalPartnerType.Id.Should().Be(request.TransactionalPartnerTypeId);
        transactionalPartner.LocationType.Id.Should().Be(request.LocationTypeId);
        transactionalPartner.TaxNo.Should().Be(request.TaxNo);
        transactionalPartner.ContactPersonInformation.Name.Should().Be(request.ContactPersonName);
        transactionalPartner.ContactPersonInformation.Email.Should().Be(request.Email);
        transactionalPartner.ContactPersonInformation.TelNo.Should().Be(request.TelNo);
    }
    
    protected async Task EnsureEqualAsync(Guid id, UpdateTransactionalPartnerCommand request)
    {
        var transactionalPartner = await GetTransactionalPartnerByIdAsync(id);
        transactionalPartner.Should().NotBeNull();
        transactionalPartner.Id.Should().Be(request.Id);
        transactionalPartner.Name.Should().Be(request.Name);
        transactionalPartner.Website.Should().Be(request.Website);
        transactionalPartner.Address.City.Should().Be(request.Address!.City);
        transactionalPartner.Address.Country.Id.Should().Be(request.Address.CountryId);
        transactionalPartner.Address.District.Should().Be(request.Address.District);
        transactionalPartner.Address.Street.Should().Be(request.Address.Street);
        transactionalPartner.Address.Ward.Should().Be(request.Address.Ward);
        transactionalPartner.Address.ZipCode.Should().Be(request.Address.ZipCode);
        transactionalPartner.CurrencyType.Id.Should().Be(request.CurrencyTypeId);
        transactionalPartner.TransactionalPartnerType.Id.Should().Be(request.TransactionalPartnerTypeId);
        transactionalPartner.LocationType.Id.Should().Be(request.LocationTypeId);
        transactionalPartner.TaxNo.Should().Be(request.TaxNo);
        transactionalPartner.ContactPersonInformation.Name.Should().Be(request.ContactPersonName);
        transactionalPartner.ContactPersonInformation.Email.Should().Be(request.Email);
        transactionalPartner.ContactPersonInformation.TelNo.Should().Be(request.TelNo);
    }
}