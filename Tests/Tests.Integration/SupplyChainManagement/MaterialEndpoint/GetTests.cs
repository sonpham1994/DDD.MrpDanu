using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Shouldly;

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
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var materialResponse = await ReadFromJsonAsync<MaterialResponse>(response);
        materialResponse!.IsSuccess.ShouldBeTrue();
        materialResponse.Errors.ShouldBeEmpty();
        materialResponse.Result.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Get_ReturnsNotFound_WhenNoMaterialExists()
    {
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var materialResponse = await ReadFromJsonAsync<MaterialResponse>(response);
        materialResponse!.IsSuccess.ShouldBeFalse();
        materialResponse.Errors.ShouldNotBeEmpty();
        materialResponse.Errors[0].ShouldBe(NotFoundMaterialId);
        materialResponse.Result.ShouldBeNull();
    }
}