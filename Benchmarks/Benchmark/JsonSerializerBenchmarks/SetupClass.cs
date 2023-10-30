using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Logging;

namespace Benchmark.JsonSerializerBenchmarks;

public class MyClass : IMyClass
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<MyClass2> MyClass2 { get; set; }
    public MyClass3 MyClass3 { get; set; }
    public DateTime Time { get; set; } = DateTime.UtcNow;
}

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
}

public class MyClassWithNoSourceGenerator
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<MyClass2> MyClass2 { get; set; }
    public MyClass3 MyClass3 { get; set; }
    public DateTime Time { get; set; } = DateTime.UtcNow;
}

public class MyClass2
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
public class MyClass3
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public interface IMyClass
{
    Guid Id { get; set; }
}

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default)]
[JsonSerializable(typeof(MyClass))]
public partial class MyClassDefaultJsonContext : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(MyClass))]
public partial class MyClassMetadatatJsonContext : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Serialization)]
[JsonSerializable(typeof(MyClass))]
public partial class MyClassSerializationJsonContext : JsonSerializerContext
{
}


[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default)]
[JsonSerializable(typeof(ApiResponse<MyClass>))]
public partial class ApiResponseMyClassDefaultJsonContext : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default)]
[JsonSerializable(typeof(ApiResponse<MyClassWithNoSourceGenerator>))]
public partial class ApiResponseMyClassWithNoSourceGeneratorDefaultJsonContext : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default)]
[JsonSerializable(typeof(Result))]
public partial class ResultDefaultJsonContext : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default)]
[JsonSerializable(typeof(Result<MyClass>))]
public partial class ResultGenericDefaultJsonContext : JsonSerializerContext
{
}