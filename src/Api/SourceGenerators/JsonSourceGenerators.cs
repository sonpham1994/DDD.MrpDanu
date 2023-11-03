using System.Text.Json.Serialization;
using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;
using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterials;
using Application.MaterialManagement.Shared;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Domain.MaterialManagement.MaterialAggregate;
using Web.ApiModels.BaseResponses;

namespace Api.SourceGenerators;
//https://www.youtube.com/watch?v=HhyBaJ7uisU&t=402s&ab_channel=NickChapsas
//https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/
//https://devblogs.microsoft.com/dotnet/system-text-json-in-dotnet-7/
//https://www.youtube.com/watch?v=2zUQwVD7T_E&ab_channel=NickChapsas
/*
 * The important to know that with System.Text.Json if your class implement interface, the System.Text.Json
 * will convert which properties inside the interface to Json. For example Myclass has Id, Name, Myclass2, ...
 * but the interface just has Id property, the System.Text.Json will convert the Id property as json.
 * In constrast, the Newtonsoft.Json.JsonConvert can convert the properties of implementation, it means
 * the json has Id, Name, Myclass2, ...
 * this drawback will may fix on .net 8: https://devblogs.microsoft.com/dotnet/system-text-json-in-dotnet-8/
 * please check "Interface hierarchy support" section
 * To fix this issue in .NET 7, we can use JsonDerivedType attribute, please check: https://www.youtube.com/watch?v=2zUQwVD7T_E&ab_channel=NickChapsas
 */
/*
 * Please check Benchmarks.Benchmark.JsonSerializerBenchmarks to see the improving performance
 */

//to know your Json source generator is working or not, change AppResponse<IReadOnlyList<MaterialsResponse>> to
// AppResponse<List<MaterialsResponse>>, it will throw an exception

//Some questions in Json source generator: https://github.com/dotnet/docs/issues/37370
// multi objects in JsonSerializerContext (please check "Then boolean and int have to be declared as [JsonSerializable]"): https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation?pivots=dotnet-7-0
// or multi objects in JsonSerializerContext also show here (please check "Recommended action"): https://learn.microsoft.com/en-us/dotnet/core/compatibility/serialization/7.0/reflection-fallback
//for Serialization GenerationMode with Record, it would be in .Net 8, please check: https://github.com/dotnet/runtime/issues/75139
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default)]
[JsonSerializable(typeof(AppResponse))]
[JsonSerializable(typeof(AppResponse<IReadOnlyList<MaterialsResponse>>))]
[JsonSerializable(typeof(AppResponse<IReadOnlyList<MaterialTypeResponse>>))]
[JsonSerializable(typeof(AppResponse<IReadOnlyList<RegionalMarketResponse>>))]
[JsonSerializable(typeof(AppResponse<IReadOnlyList<SuppliersResponse>>))]
[JsonSerializable(typeof(AppResponse<IReadOnlyList<LocationTypeResponse>>))]
[JsonSerializable(typeof(AppResponse<IReadOnlyList<TransactionalPartnerTypeResponse>>))]
[JsonSerializable(typeof(AppResponse<IReadOnlyList<CurrencyTypeResponse>>))]
public partial class JsonSourceGeneratorJsonContext : JsonSerializerContext
{
}