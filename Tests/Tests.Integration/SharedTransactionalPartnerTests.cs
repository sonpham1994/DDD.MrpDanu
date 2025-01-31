using System.Net.Http.Json;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Bogus;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using SharedKernel.Enumerations;

namespace Tests.Integration;

public partial class SharedTests
{
    private const string TransactionalPartnerEndpoint = SupplyChainManagement.TransactionalPartnerEndpoint.SharedTests.Endpoint;

    protected static readonly Faker<AddressCommand> CreateAddressFaker = new Faker<AddressCommand>()
        .RuleFor(x => x.ZipCode, f => f.Random.Int(10000, 99999).ToString())
        .RuleFor(x => x.City, f => f.Address.City())
        .RuleFor(x => x.CountryId, Country.VietNam.Id)
        .RuleFor(x => x.Street, f => f.Address.StreetAddress())
        .RuleFor(x => x.District, f => f.Address.County())
        .RuleFor(x => x.Ward, f => f.Address.Random.Words(1));

    protected static readonly Faker<CreateTransactionalPartnerCommand> CreateTransactionalPartnerFaker = new Faker<CreateTransactionalPartnerCommand>()
        .CustomInstantiator(f=> new CreateTransactionalPartnerCommand(
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
    
    public static readonly Faker<CreateTransactionalPartnerCommand> CreateSupplierOrBothTypeFaker = CreateTransactionalPartnerFaker
        .Clone()
        .RuleFor(x => x.TransactionalPartnerTypeId,
            f => f.PickRandom(TransactionalPartnerType.Supplier.Id, TransactionalPartnerType.Both.Id));

    private static readonly Faker<CreateTransactionalPartnerCommand> CreateSupplierTypeFaker = CreateTransactionalPartnerFaker
        .Clone()
        .RuleFor(x => x.TransactionalPartnerTypeId, TransactionalPartnerType.Supplier.Id);

    private static readonly Faker<CreateTransactionalPartnerCommand> CreateBothTypeFaker = CreateTransactionalPartnerFaker
        .Clone()
        .RuleFor(x => x.TransactionalPartnerTypeId, TransactionalPartnerType.Both.Id);

    protected async Task<Guid> SetupCreatingTransactionalPartnerAsync(CreateTransactionalPartnerCommand request)
    {
        var response = await HttpClient.PostAsJsonAsync(TransactionalPartnerEndpoint, request);
        var result = await ReadFromJsonAsync<Guid>(response);
        var id = result!.Result;
        
        CreatedIds.Add(id);
        
        return id;
    }

    protected async Task<Guid> SetupCreatingTransactionalPartnerAsync()
    {
        var request = CreateTransactionalPartnerFaker.Generate();
        var id = await SetupCreatingTransactionalPartnerAsync(request);
        
        return id;
    }

    protected async Task<Guid> SetupCreatingSupplierTypeAsync()
    {
        var request = CreateSupplierTypeFaker.Generate();
        var id = await SetupCreatingTransactionalPartnerAsync(request);
        
        return id;
    }

    protected async Task<Guid> SetupCreatingBothTypeAsync()
    {
        var request = CreateBothTypeFaker.Generate();
        var id = await SetupCreatingTransactionalPartnerAsync(request);
        
        return id;
    }
}