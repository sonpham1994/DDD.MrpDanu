using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.Shared;
using Shouldly;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

public class GetSupplierTests(EndpointFactory endpointFactory)
    : SharedTests(endpointFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Get_ReturnsResult_WhenSupplierTypesExist()
    {
        //Arrange
        await SetupCreatingSupplierTypeAsync();
        
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/suppliers");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var supplierTypesResponse = await ReadFromJsonAsync<IReadOnlyList<SuppliersResponse>>(response);
        supplierTypesResponse!.IsSuccess.ShouldBeTrue();
        supplierTypesResponse.Errors.ShouldBeEmpty();
        supplierTypesResponse.Result.ShouldHaveSingleItem();
    }
    
    [Fact]
    public async Task Get_ReturnsResult_WhenBothTypesExist()
    {
        //Arrange
        await SetupCreatingBothTypeAsync();
        
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/suppliers");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var bothTypesResponse = await ReadFromJsonAsync<IReadOnlyList<SuppliersResponse>>(response);
        bothTypesResponse!.IsSuccess.ShouldBeTrue();
        bothTypesResponse.Errors.ShouldBeEmpty();
        bothTypesResponse.Result.ShouldHaveSingleItem();
    }
    
    [Fact]
    public async Task Get_ReturnsEmptyResult_WhenNoSupplierOrBothTypesExist()
    {
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/suppliers");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var resultResponse = await ReadFromJsonAsync<IReadOnlyList<SuppliersResponse>>(response);
        resultResponse!.IsSuccess.ShouldBeTrue();
        resultResponse.Errors.ShouldBeEmpty();
        resultResponse.Result.ShouldBeEmpty();
    }
}