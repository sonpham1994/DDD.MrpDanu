using System.Net;
using System.Net.Http.Json;
using Shouldly;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

public class CreateTests(EndpointFactory apiFactory)
    : SharedTests(apiFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Create_Material_WhenDataIsValid()
    {
        //Arrange
        var request = CreateMaterialFaker.Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        var createdResponse = await ReadFromJsonAsync<Guid>(response);
        var materialId = createdResponse!.Result;
        response.Headers.Location!.ToString().ShouldBe($"{Url}/{materialId}");
        createdResponse.IsSuccess.ShouldBeTrue();
        createdResponse.Errors.ShouldBeEmpty();

        await EnsureEqualAsync(materialId, request);
        
        var materials = await GetAllMaterialsAsync();
        materials.ShouldNotBeEmpty();
        materials.ShouldHaveSingleItem();
        materials[0].Id.ShouldBe(materialId);
        
        CreatedIds.Add(materialId);
    }
    
    [Fact]
    public async Task Create_ReturnsValidationError_WhenInputIsInvalid()
    {
        //Arrange
        var request = CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.RegionalMarketId, (byte)200)
            .Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.ShouldBeFalse();
        createdResponse.Errors.ShouldHaveSingleItem();
        createdResponse.Result.ShouldBeNull();
        
        var materials = await GetAllMaterialsAsync();
        materials.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task Create_ReturnsError_WhenCodeWasExisted()
    {
        //Arrange
        var request1 = CreateMaterialFaker.Generate();
        var id1 = await SetupCreatingMaterialAsync(request1);
        var code = request1.Code;
        var request2 = CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.Code, code)
            .Generate();
        
        // Act
        var response2 = await HttpClient.PostAsJsonAsync(Endpoint, request2);
        var createdResponse2 = await ReadFromJsonAsync<Guid?>(response2);
        
        // Assert
        response2.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        createdResponse2.ShouldNotBeNull();
        createdResponse2.IsSuccess.ShouldBeFalse();
        createdResponse2.Errors[0].ShouldBe(ExistedMaterialCode);
        createdResponse2.Result.ShouldBeNull();

        var materials = await GetAllMaterialsAsync();
        materials.ShouldNotBeEmpty();
        materials.ShouldHaveSingleItem();
        materials[0].Id.ShouldBe(id1);
    }
    
    [Fact]
    public async Task AddMaterialCost_MaterialWithMaterialCost_WhenDataIsValid()
    {
        //Arrange
        var supplierId = await SetupCreatingSupplierTypeAsync();
        var request = CreateMaterialWithMaterialCostFaker(supplierId).Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        var createdResponse = await ReadFromJsonAsync<Guid>(response);
        var materialId = createdResponse!.Result;
        response.Headers.Location!.ToString().ShouldBe($"{Url}/{materialId}");
        createdResponse.IsSuccess.ShouldBeTrue();
        createdResponse.Errors.ShouldBeEmpty();

        await EnsureMaterialCostEqualAsync(materialId, request.MaterialCosts);
        
        CreatedIds.Add(materialId);
    }
    
    [Fact]
    public async Task AddMaterialCost_ReturnsError_WhenSupplierIdDoesNotExist()
    {
        //Arrange
        var supplierId = Guid.NewGuid();
        var request = CreateMaterialWithMaterialCostFaker(supplierId).Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.ShouldBeFalse();
        createdResponse.Result.ShouldBeNull();
        createdResponse.Errors.ShouldHaveSingleItem();
        createdResponse.Errors[0].ShouldBe(NotFoundSupplierId);
        
        var materials = await GetAllMaterialsAsync();
        materials.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task AddTwoMaterialCosts_ReturnsError_WhenOneOfSupplierIdDoesNotExist()
    {
        //Arrange
        var supplierId1 = await SetupCreatingSupplierTypeAsync();
        var supplierId2 = Guid.NewGuid();
        var request = CreateMaterialWithMaterialCostsFaker([supplierId1, supplierId2]).Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.ShouldBeFalse();
        createdResponse.Result.ShouldBeNull();
        createdResponse.Errors.ShouldHaveSingleItem();
        createdResponse.Errors[0].ShouldBe(NotFoundSupplierId);
        
        var materials = await GetAllMaterialsAsync();
        materials.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task AddTwoMaterialCosts_ReturnsError_WhenSupplierIdIsDuplicated()
    {
        //Arrange
        var supplierId1 = await SetupCreatingSupplierTypeAsync();
        var request = CreateMaterialWithMaterialCostsFaker([supplierId1, supplierId1]).Generate();
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync<Guid?>(response);
        createdResponse!.IsSuccess.ShouldBeFalse();
        createdResponse.Result.ShouldBeNull();
        createdResponse.Errors.ShouldHaveSingleItem();
        createdResponse.Errors[0].ShouldBe(DuplicatedSupplierId);
        
        var materials = await GetAllMaterialsAsync();
        materials.ShouldBeEmpty();
    }
}