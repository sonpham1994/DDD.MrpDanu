using System.Net;
using Application.SupplyAndProductionManagement.SupplyChainManagement.Shared;
using FluentAssertions;

namespace Tests.Integration.SupplyChainManagement.TransactionalPartnerEndpoint;

public class GetSupplierTests(EndpointFactory apiFactory)
    : SharedTests(apiFactory.CreateClient()), IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Get_ReturnsResult_WhenSupplierTypesExist()
    {
        //Arrange
        await SetupCreatingSupplierTypeAsync();
        
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/suppliers");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var supplierTypesResponse = await ReadFromJsonAsync<IReadOnlyList<SuppliersResponse>>(response);
        supplierTypesResponse!.IsSuccess.Should().BeTrue();
        supplierTypesResponse.Errors.Should().BeEmpty();
        supplierTypesResponse.Result.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task Get_ReturnsResult_WhenBothTypesExist()
    {
        //Arrange
        await SetupCreatingBothTypeAsync();
        
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/suppliers");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var bothTypesResponse = await ReadFromJsonAsync<IReadOnlyList<SuppliersResponse>>(response);
        bothTypesResponse!.IsSuccess.Should().BeTrue();
        bothTypesResponse.Errors.Should().BeEmpty();
        bothTypesResponse.Result.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task Get_ReturnsEmptyResult_WhenNoSupplierOrBothTypesExist()
    {
        // Act
        var response = await HttpClient.GetAsync($"{Endpoint}/suppliers");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var resultResponse = await ReadFromJsonAsync<IReadOnlyList<SuppliersResponse>>(response);
        resultResponse!.IsSuccess.Should().BeTrue();
        resultResponse.Errors.Should().BeEmpty();
        resultResponse.Result.Should().BeEmpty();
    }
}