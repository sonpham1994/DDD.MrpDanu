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



public static class LoggingDefinition
{
    public static void StartResult(this ILogger logger, Result result)
    {
        StartResultDefinition(logger, result, null);
    }

    public static void StartResultWithNoSourcegenerator(this ILogger logger, ResultWithNoJsonSourceGenerator result)
    {
        StartResultWithNoJsonSourceGeneratorDefinition(logger, result, null);
    }

    public static void StartResultT(this ILogger logger, Result<MyClass> result)
    {
        StartResultTDefinition(logger, result, null);
    }

    public static void StartResultTWithNoSourcegenerator(this ILogger logger, ResultWithNoJsonSourceGenerator<MyClass> result)
    {
        StartResultTWithNoJsonSourceGeneratorDefinition(logger, result, null);
    }


    private static readonly Action<Microsoft.Extensions.Logging.ILogger, ResultWithNoJsonSourceGenerator, Exception?> StartResultWithNoJsonSourceGeneratorDefinition =
        LoggerMessage.Define<ResultWithNoJsonSourceGenerator>(LogLevel.Information, 0,
            "----- Result: {@Result}");
    private static readonly Action<Microsoft.Extensions.Logging.ILogger, ResultWithNoJsonSourceGenerator<MyClass>, Exception?> StartResultTWithNoJsonSourceGeneratorDefinition =
        LoggerMessage.Define<ResultWithNoJsonSourceGenerator<MyClass>>(LogLevel.Information, 0,
            "----- Result: {@Result}");

    private static readonly Action<Microsoft.Extensions.Logging.ILogger, Result, Exception?> StartResultDefinition =
        LoggerMessage.Define<Result>(LogLevel.Information, 0,
            "----- Result: {@Result}");

    private static readonly Action<Microsoft.Extensions.Logging.ILogger, Result<MyClass>, Exception?> StartResultTDefinition =
        LoggerMessage.Define<Result<MyClass>>(LogLevel.Information, 0,
            "----- Result: {@Result}");
}