using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using FluentAssertions;

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
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialResponse = await ReadFromJsonAsync<TransactionalPartnerResponse>(response);
        materialResponse!.IsSuccess.Should().BeTrue();
        materialResponse.Errors.Should().BeEmpty();
        materialResponse.Result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Get_ReturnsNotFound_WhenNoTransactionalPartnerExists()
    {
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var materialResponse = await ReadFromJsonAsync<TransactionalPartnerResponse>(response);
        materialResponse!.IsSuccess.Should().BeFalse();
        materialResponse.Errors.Should().NotBeEmpty();
        materialResponse.Errors[0].Should().Be(NotFoundId);
        materialResponse.Result.Should().BeNull();
    }
}