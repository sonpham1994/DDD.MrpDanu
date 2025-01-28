using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using FluentAssertions;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class GetAllTests(EndpointFactory apiFactory) 
    : SharedTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task GetAll_ReturnsResult_WhenMaterialsExist()
    {
        //Arrange
        await SetupCreatingMaterialAsync();
        
        // Act
        var response = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialsResponse = await ReadFromJsonAsync<IReadOnlyList<MaterialsResponse>>(response);
        materialsResponse!.IsSuccess.Should().BeTrue();
        materialsResponse.Errors.Should().BeEmpty();
        materialsResponse.Result.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNoMaterialExists()
    {
        // Act
        var response = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var materialsResponse = await ReadFromJsonAsync<IReadOnlyList<MaterialsResponse>>(response);
        materialsResponse!.IsSuccess.Should().BeTrue();
        materialsResponse.Errors.Should().BeEmpty();
        materialsResponse.Result.Should().BeEmpty();
    }
}