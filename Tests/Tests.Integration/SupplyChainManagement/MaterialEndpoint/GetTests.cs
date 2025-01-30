using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using FluentAssertions;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

//[Collection("MaterialEndpoint")]
public class GetTests(EndpointFactory apiFactory)
    : SharedTests(apiFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Get_ReturnsResult_WhenMaterialExists()
    {
        // Arrange
        var id = await SetupCreatingMaterialAsync();
        
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/{id}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialResponse = await ReadFromJsonAsync<MaterialResponse>(response);
        materialResponse!.IsSuccess.Should().BeTrue();
        materialResponse.Errors.Should().BeEmpty();
        materialResponse.Result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Get_ReturnsNotFound_WhenNoMaterialExists()
    {
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var materialResponse = await ReadFromJsonAsync<MaterialResponse>(response);
        materialResponse!.IsSuccess.Should().BeFalse();
        materialResponse.Errors.Should().NotBeEmpty();
        materialResponse.Errors[0].Should().Be(NotFoundMaterial);
        materialResponse.Result.Should().BeNull();
    }
}