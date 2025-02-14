using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Shouldly;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

public class GetTests(EndpointFactory endpointFactory)
    : SharedTests(endpointFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Get_ReturnsResult_WhenTransactionalPartnerExists()
    {
        // Arrange
        var id = await SetupCreatingTransactionalPartnerAsync();
        
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/{id}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var materialResponse = await ReadFromJsonAsync<TransactionalPartnerResponse>(response);
        materialResponse!.IsSuccess.ShouldBeTrue();
        materialResponse.Errors.ShouldBeEmpty();
        materialResponse.Result.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Get_ReturnsNotFound_WhenNoTransactionalPartnerExists()
    {
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var materialResponse = await ReadFromJsonAsync<TransactionalPartnerResponse>(response);
        materialResponse!.IsSuccess.ShouldBeFalse();
        materialResponse.Errors.ShouldNotBeEmpty();
        materialResponse.Errors[0].ShouldBe(NotFoundId);
        materialResponse.Result.ShouldBeNull();
    }
}