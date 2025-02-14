using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Shouldly;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

//[Collection("MaterialEndpoint")]
public class GetAllTests(EndpointFactory apiFactory) 
    : SharedTests(apiFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task GetAll_ReturnsResult_WhenMaterialsExist()
    {
        //Arrange
        await SetupCreatingMaterialAsync();
        
        // Act
        var response = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var materialsResponse = await ReadFromJsonAsync<IReadOnlyList<MaterialsResponse>>(response);
        materialsResponse!.IsSuccess.ShouldBeTrue();
        materialsResponse.Errors.ShouldBeEmpty();
        materialsResponse.Result.ShouldHaveSingleItem();
    }
    
    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNoMaterialExists()
    {
        // Act
        var response = await HttpClient.GetAsync(Endpoint);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var materialsResponse = await ReadFromJsonAsync<IReadOnlyList<MaterialsResponse>>(response);
        materialsResponse!.IsSuccess.ShouldBeTrue();
        materialsResponse.Errors.ShouldBeEmpty();
        materialsResponse.Result.ShouldBeEmpty();
    }
}