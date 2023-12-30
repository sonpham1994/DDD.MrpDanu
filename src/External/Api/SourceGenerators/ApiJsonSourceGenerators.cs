using System.Text.Json.Serialization;
using Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Application.SupplyChainManagement.Shared;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Api.ApiResponses;
using Application.SupplyChainManagement.MaterialAggregate;
using Application.SupplyChainManagement.TransactionalPartnerAggregate;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;
using Application.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;

namespace Api.SourceGenerators;
/*
 * https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview
 * Source generator: Source generators are used to produce code that avoids the need for reflection.
 * Runtime reflection is a powerful technology that was added to .NET a long time ago. There are countless
 *  scenarios for using it. A common scenario is to perform some analysis of user code when an app starts up
 *  and use that data to generate things.
 * For example, ASP.NET Core uses reflection when your web service first runs to discover constructs you've defined
 *  so that it can "wire up" things like controllers and razor pages. Although this enables you to write
 *  straightforward code with powerful abstractions, it comes with a performance penalty at run time: when your web
 *  service or app first starts up, it can’t accept any requests until all the runtime reflection code that
 *  discovers information about your code is finished running. Although this performance penalty isn't enormous,
 *  it's somewhat of a fixed cost that you can’t improve yourself in your own app.
 * With a Source Generator, the controller discovery phase of startup could instead happen at compile time. A
 *  generator can analyze your source code and emit the code it needs to "wire up" your app. Using source generators
 *  could result in some faster startup times, since an action happening at run time today could get pushed into
 *  compile time.
 */
/*
 * .NET Ahead-Of-Time, or .NET AOT for short, released in .Net 8 won't use reflection, it will use source generator
 *  instead. -> May be in the era of future, the use of reflection slowly die out.
 * https://learn.microsoft.com/en-us/aspnet/core/fundamentals/native-aot?view=aspnetcore-8.0
 * https://www.thinktecture.com/en/net/native-aot-with-asp-net-core-overview/?fbclid=IwAR3JFkhx52jmnR5PVHxcRNVGP7_8hAtQpOKiECbI-2L_XmhNUa9Q9ODd-EE_aem_AaCAuIuSRRllvxVtD2ZrFskfaXL4sh6yjpH3KUUELCY2DvtfdUdKgFwiaL6xTbiZlLs
 */

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

//to know your Json source generator is working or not, just romove the type for certain api and call that api, it will throw an exception

//Some questions in Json source generator: https://github.com/dotnet/docs/issues/37370
// multi objects in JsonSerializerContext (please check "Then boolean and int have to be declared as [JsonSerializable]"): https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation?pivots=dotnet-7-0
// or multi objects in JsonSerializerContext also show here (please check "Recommended action"): https://learn.microsoft.com/en-us/dotnet/core/compatibility/serialization/7.0/reflection-fallback
//for Serialization GenerationMode with Record, it would be in .Net 8, please check: https://github.com/dotnet/runtime/issues/75139
// .Net 8 and Json source generator: https://www.linkedin.com/posts/sina-riyahi_leaked-net-8-features-ugcPost-7127674450916859904-mvkZ?utm_source=share&utm_medium=member_ios
//disable using reflection for serialization at runtime. We use Json source generator instead in .Net 8. https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation?pivots=dotnet-8-0
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    //Serialization is just for Serializer, not Deserializer? If we just need Serialization, we use this mode instead Default mode which include both Serialization and Metadata, and as a result it reduces compile time
    GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(ApiResponse))]
[JsonSerializable(typeof(ApiResponse<IReadOnlyList<MaterialsResponse>>))]
[JsonSerializable(typeof(ApiResponse<MaterialResponse>))]
[JsonSerializable(typeof(ApiResponse<IReadOnlyList<SuppliersResponse>>))]
[JsonSerializable(typeof(ApiResponse<IReadOnlyList<TransactionalPartnersResponse>>))]
[JsonSerializable(typeof(ApiResponse<TransactionalPartnerResponse>))]
// Cannot use request model due to "ValidationProblemDetails" issue, so in this case we use reflection-based deserialization. Please check at program
//[JsonSerializable(typeof(CreateTransactionalPartnerCommand))]
public partial class ApiResponseJsonSourceGenerator : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(
 PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
 GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(CreateTransactionalPartnerCommand))]
[JsonSerializable(typeof(UpdateTransactionalPartnerCommand))]
[JsonSerializable(typeof(CreateMaterialCommand))]
[JsonSerializable(typeof(UpdateMaterialCommand))]
public partial class ApiRequestJsonSourceGenerator : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    //Serialization is just for Serializer, not Deserializer? If we just need Serialization, we use this mode instead Default mode which include both Serialization and Metadata, and as a result it reduces compile time
    GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(ApiResponse<IReadOnlyList<MaterialTypeResponse>>))]
[JsonSerializable(typeof(ApiResponse<IReadOnlyList<RegionalMarketResponse>>))]
[JsonSerializable(typeof(ApiResponse<IReadOnlyList<LocationTypeResponse>>))]
[JsonSerializable(typeof(ApiResponse<IReadOnlyList<TransactionalPartnerTypeResponse>>))]
[JsonSerializable(typeof(ApiResponse<IReadOnlyList<CurrencyTypeResponse>>))]
[JsonSerializable(typeof(ApiResponse<IReadOnlyList<CountryResponse>>))]
public partial class MinimalApiJsonSourceGenerator : JsonSerializerContext
{
}