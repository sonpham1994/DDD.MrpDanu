using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using FluentAssertions;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

public class GetAllTests(EndpointFactory apiFactory)
    : SharedTests(apiFactory.CreateClient()) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task GetAll_ReturnsResult_WhenTransactionalPartnersExist()
    {
        //Arrange
        await SetupCreatingTransactionalPartnerAsync();
        
        // Act
        var response = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var transactionalPartnersResponse = await ReadFromJsonAsync<IReadOnlyList<TransactionalPartnersResponse>>(response);
        transactionalPartnersResponse!.IsSuccess.Should().BeTrue();
        transactionalPartnersResponse.Errors.Should().BeEmpty();
        transactionalPartnersResponse.Result.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNoTransactionalPartnerExists()
    {
        // Act
        var response = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var transactionalPartnersResponse = await ReadFromJsonAsync<IReadOnlyList<TransactionalPartnersResponse>>(response);
        transactionalPartnersResponse!.IsSuccess.Should().BeTrue();
        transactionalPartnersResponse.Errors.Should().BeEmpty();
        transactionalPartnersResponse.Result.Should().BeEmpty();
    }
}