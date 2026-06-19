// using System.Text.Json.Serialization;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.Shared;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
// using MinimalApi.ApiResponses;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
// using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;
//
// [JsonSourceGenerationOptions(
//     PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
//     //Serialization is just for Serializer, not Deserializer? If we just need Serialization, we use this mode instead Default mode which include both Serialization and Metadata, and as a result it reduces compile time
//     GenerationMode = JsonSourceGenerationMode.Default)]
// [JsonSerializable(typeof(ApiResponse))]
// [JsonSerializable(typeof(ApiCollectionResponse<MaterialsResponse>))]
// [JsonSerializable(typeof(ApiResponse<MaterialResponse>))]
// [JsonSerializable(typeof(ApiCollectionResponse<SuppliersResponse>))]
// [JsonSerializable(typeof(ApiCollectionResponse<TransactionalPartnersResponse>))]
// [JsonSerializable(typeof(ApiResponse<TransactionalPartnerResponse>))]
// // Cannot use request model due to "ValidationProblemDetails" issue, so in this case we use reflection-based deserialization. Please check at program
// [JsonSerializable(typeof(ApiCollectionResponse<MaterialTypeResponse>))]
// [JsonSerializable(typeof(ApiCollectionResponse<RegionalMarketResponse>))]
// [JsonSerializable(typeof(ApiCollectionResponse<CountryResponse>))]
// [JsonSerializable(typeof(ApiCollectionResponse<LocationTypeResponse>))]
// [JsonSerializable(typeof(ApiCollectionResponse<TransactionalPartnerTypeResponse>))]
// [JsonSerializable(typeof(ApiCollectionResponse<CurrencyTypeResponse>))]
// [JsonSerializable(typeof(ApiResponse<Guid>))]
// public partial class ApiResponseJsonSourceGenerator : JsonSerializerContext
// {
// }
//
// [JsonSourceGenerationOptions(
//  PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
//  GenerationMode = JsonSourceGenerationMode.Default)]
// [JsonSerializable(typeof(CreateTransactionalPartnerCommand))]
// [JsonSerializable(typeof(UpdateTransactionalPartnerCommand))]
// [JsonSerializable(typeof(CreateMaterialCommand))]
// [JsonSerializable(typeof(UpdateMaterialCommand))]
// public partial class ApiRequestJsonSourceGenerator : JsonSerializerContext
// {
// }
