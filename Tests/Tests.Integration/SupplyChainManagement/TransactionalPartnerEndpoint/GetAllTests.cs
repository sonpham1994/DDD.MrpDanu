using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Shouldly;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

public class GetAllTests(EndpointFactory endpointFactory)
    : SharedTests(endpointFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task GetAll_ReturnsResult_WhenTransactionalPartnersExist()
    {
        //Arrange
        await SetupCreatingTransactionalPartnerAsync();
        
        // Act
        var response = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var transactionalPartnersResponse = await ReadFromJsonAsync<IReadOnlyList<TransactionalPartnersResponse>>(response);
        transactionalPartnersResponse!.IsSuccess.ShouldBeTrue();
        transactionalPartnersResponse.Errors.ShouldBeEmpty();
        transactionalPartnersResponse.Result.ShouldHaveSingleItem();
    }
    
    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNoTransactionalPartnerExists()
    {
        // Act
        var response = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var transactionalPartnersResponse = await ReadFromJsonAsync<IReadOnlyList<TransactionalPartnersResponse>>(response);
        transactionalPartnersResponse!.IsSuccess.ShouldBeTrue();
        transactionalPartnersResponse.Errors.ShouldBeEmpty();
        transactionalPartnersResponse.Result.ShouldBeEmpty();
    }
}