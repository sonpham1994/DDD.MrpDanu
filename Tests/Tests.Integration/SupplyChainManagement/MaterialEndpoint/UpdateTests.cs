using System.Net;
using System.Net.Http.Json;
using Shouldly;

namespace Tests.Integration.SupplyChainManagement.MaterialEndpoint;

//[Collection("MaterialEndpoint")]
public class UpdateTests(EndpointFactory apiFactory) 
    : SharedTests(apiFactory) //, IClassFixture<EndpointFactory>
{
    [Fact]
    public async Task Update_Material_WhenDataIsValid()
    {
        //Arrange
        var materialId = await SetupCreatingMaterialAsync();
        var request = UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse!.IsSuccess.ShouldBeTrue();
        updatedResponse.Errors.ShouldBeEmpty();

        await EnsureEqualAsync(materialId, request);
    }
    
    [Fact]
    public async Task Update_ReturnsValidationError_WhenInputIsInvalid()
    {
        //Arrange
        var createRequest = CreateMaterialFaker.Generate();
        var materialId = await SetupCreatingMaterialAsync(createRequest);
        var request = UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .RuleFor(x => x.RegionalMarketId, (byte)200)
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var createdResponse = await ReadFromJsonAsync(response);
        createdResponse!.IsSuccess.ShouldBeFalse();
        createdResponse.Errors.ShouldHaveSingleItem();
        
        await EnsureEqualAsync(materialId, createRequest);
    }
    
    [Fact]
    public async Task Update_Material_WhenCodeWasExistedByItself()
    {
        //Arrange
        var materialId = await SetupCreatingMaterialAsync();
        var request = UpdateMaterialFaker(materialId)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse!.IsSuccess.ShouldBeTrue();
        updatedResponse.Errors.ShouldBeEmpty();

        await EnsureEqualAsync(materialId, request);
    }
    
    [Fact]
    public async Task Update_ReturnsError_WhenCodeWasExisted()
    {
        //Arrange
        await SetupCreatingMaterialAsync();
        
        var createRequest = CreateMaterialFaker
            .Clone()
            .RuleFor(x => x.Code, "code1")
            .Generate();
        var materialId2 = await SetupCreatingMaterialAsync(createRequest);
        
        var updateRequest = UpdateMaterialFaker(materialId2)
            .Clone()
            .RuleFor(x => x.Code, "code")
            .Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId2}", updateRequest);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse.ShouldNotBeNull();
        updatedResponse.IsSuccess.ShouldBeFalse();
        updatedResponse.Errors[0].ShouldBe(ExistedMaterialCode);
        
        await EnsureEqualAsync(materialId2, createRequest);
    }
    
    [Fact]
    public async Task AddUpdateDeleteMaterialCost_MaterialWithMaterialCost_WhenDataIsValid()
    {
        //Arrange
        var supplierIdAdded = await SetupCreatingSupplierTypeAsync();
        var supplierIdUpdated = await SetupCreatingSupplierTypeAsync();
        var supplierIdDeleted = await SetupCreatingSupplierTypeAsync();
        var materialId = await SetupCreatingMaterialWithCostAsync([supplierIdUpdated, supplierIdDeleted]);
        var request = UpdateMaterialWithMaterialCostsFaker(materialId, [supplierIdAdded, supplierIdUpdated]).Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse!.IsSuccess.ShouldBeTrue();
        updatedResponse.Errors.ShouldBeEmpty();

        await EnsureMaterialCostEqualAsync(materialId, request.MaterialCosts);
    }
    
    [Fact]
    public async Task AddUpdateDeleteMaterialCost_ReturnsError_WhenSupplierIdDoesNotExist()
    {
        //Arrange
        var supplierIdAdded = await SetupCreatingSupplierTypeAsync();
        var supplierIdUpdated = await SetupCreatingSupplierTypeAsync();
        var supplierIdFake = Guid.NewGuid();
        var createdRequest = CreateMaterialWithMaterialCostsFaker([supplierIdUpdated, supplierIdAdded]).Generate();
        var materialId = await SetupCreatingMaterialAsync(createdRequest);
        var request = UpdateMaterialWithMaterialCostsFaker(materialId, [supplierIdAdded, supplierIdUpdated, supplierIdFake]).Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse!.IsSuccess.ShouldBeFalse();
        updatedResponse.Errors.ShouldHaveSingleItem();
        updatedResponse.Errors[0].ShouldBe(NotFoundSupplierId);

        await EnsureMaterialCostEqualAsync(materialId, createdRequest.MaterialCosts);
    }
    
    [Fact]
    public async Task AddUpdateDeleteMaterialCost_ReturnsError_WhenSupplierIdIsDuplicated()
    {
        //Arrange
        var supplierIdAdded = await SetupCreatingSupplierTypeAsync();
        var supplierIdUpdated = await SetupCreatingSupplierTypeAsync();
        var createdRequest = CreateMaterialWithMaterialCostsFaker([supplierIdUpdated, supplierIdAdded]).Generate();
        var materialId = await SetupCreatingMaterialAsync(createdRequest);
        var request = UpdateMaterialWithMaterialCostsFaker(materialId, [supplierIdAdded, supplierIdUpdated, supplierIdUpdated]).Generate();
        
        // Act
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{materialId}", request);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var updatedResponse = await ReadFromJsonAsync(response);
        updatedResponse!.IsSuccess.ShouldBeFalse();
        updatedResponse.Errors.ShouldHaveSingleItem();
        updatedResponse.Errors[0].ShouldBe(DuplicatedSupplierId);

        await EnsureMaterialCostEqualAsync(materialId, createdRequest.MaterialCosts);
    }
}