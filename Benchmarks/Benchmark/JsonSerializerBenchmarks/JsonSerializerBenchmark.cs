using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System.Text.Json;

namespace Benchmark.JsonSerializerBenchmarks;

//https://www.linkedin.com/posts/thisisnabi_csharp-dotnet-performance-activity-7118264068355829761-L1aH/?utm_source=share&utm_medium=member_ios&fbclid=IwAR38yk53cvq4yPi0-rSNbzYYCnmzU7RzdEn0ojRwZrslZ_dYAJjgSRqfHKw
[MemoryDiagnoser()]
public class JsonSerializerBenchmark
{
    private MyClass mclass;
    private IMyClass Imclass;
    private string serializeMClass;
    private string serializeMClassWithDefaultSourceGenerator;
    private string serializeMClassWithMetadataSourceGenerator;
    private string serializeMClassWithSerializationSourceGenerator;
    private readonly Serilog.ILogger _logger;

    public JsonSerializerBenchmark()
    {
        mclass = new MyClass()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            MyClass2 = new List<MyClass2>
            {
                new MyClass2
                {
                    Id = Guid.NewGuid(),
                    Name = "NameMyclass2_1"
                },
                new MyClass2
                {
                    Id = Guid.NewGuid(),
                    Name = "NameMyclass2_2"
                }
            },
            MyClass3 = new MyClass3
            {
                Id = Guid.NewGuid(),
                Name = "Namemyclass3"
            }
        };

        Imclass = new MyClass()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            MyClass2 = new List<MyClass2>
            {
                new MyClass2
                {
                    Id = Guid.NewGuid(),
                    Name = "NameMyclass2_1"
                },
                new MyClass2
                {
                    Id = Guid.NewGuid(),
                    Name = "NameMyclass2_2"
                }
            },
            MyClass3 = new MyClass3
            {
                Id = Guid.NewGuid(),
                Name = "Namemyclass3"
            }
        };

        serializeMClass = System.Text.Json.JsonSerializer.Serialize(mclass);
        serializeMClassWithDefaultSourceGenerator = System.Text.Json.JsonSerializer.Serialize(mclass, MyClassDefaultJsonContext.Default.MyClass);
        serializeMClassWithMetadataSourceGenerator = System.Text.Json.JsonSerializer.Serialize(mclass, MyClassMetadatatJsonContext.Default.MyClass);
        serializeMClassWithSerializationSourceGenerator = System.Text.Json.JsonSerializer.Serialize(mclass, MyClassSerializationJsonContext.Default.MyClass);

        // _logger = new Logger<JsonSerializerBenchmark>(LoggerFactory.Create(builder =>
        // {
        //     builder.AddConsole().SetMinimumLevel(LogLevel.Information);
        // }));
        _logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .WriteTo.Console()
                        .CreateLogger();
    }

    // [Benchmark]
    // public void NewtonsoftJsonSerializerWithClassImplementation()
    // {
    //     var a = Newtonsoft.Json.JsonConvert.SerializeObject(mclass);
    // }

    // [Benchmark]
    // public void SystemTextJsonSerializerWithClassImplementation()
    // {
    //     var a = System.Text.Json.JsonSerializer.Serialize(mclass);
    // }

    // //https://www.youtube.com/watch?v=HhyBaJ7uisU&t=402s&ab_channel=NickChapsas
    // //https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/
    // //https://devblogs.microsoft.com/dotnet/system-text-json-in-dotnet-7/
    // //https://www.youtube.com/watch?v=2zUQwVD7T_E&ab_channel=NickChapsas
    // [Benchmark]
    // public void SystemTextJsonSerializerSourceGeneratorDefaultWithClassImplementation()
    // {
    //     var a = System.Text.Json.JsonSerializer.Serialize(mclass, MyClassDefaultJsonContext.Default.MyClass);
    //     //Console.WriteLine(a);
    // }

    // // [Benchmark]
    // // public void SystemTextJsonSerializerSourceGeneratorMetadataWithClassImplementation()
    // // {
    // //     var a = System.Text.Json.JsonSerializer.Serialize(mclass, MyClassMetadatatJsonContext.Default.MyClass);
    // //     //Console.WriteLine(a);
    // // }
    // //
    // // [Benchmark]
    // // public void SystemTextJsonSerializerSourceGeneratorSerializationWithClassImplementation()
    // // {
    // //     var a = System.Text.Json.JsonSerializer.Serialize(mclass, MyClassSerializationJsonContext.Default.MyClass);
    // //     //Console.WriteLine(a);
    // // }

    // [Benchmark]
    // public void NewtonsoftJsonSerializerWithInterface()
    // {
    //     var a = Newtonsoft.Json.JsonConvert.SerializeObject(Imclass);
    // }

    // /*
    //  * The important to know that with System.Text.Json if your class implement interface, the System.Text.Json
    //  * will convert which properties inside the interface to Json. For example Myclass has Id, Name, Myclass2, ...
    //  * but the interface just has Id property, the System.Text.Json will convert the Id property as json.
    //  * In constrast, the Newtonsoft.Json.JsonConvert can convert the properties of implementation, it means
    //  * the json has Id, Name, Myclass2, ...
    //  * this drawback will may fix on .net 8: https://devblogs.microsoft.com/dotnet/system-text-json-in-dotnet-8/
    //  * please check "Interface hierarchy support" section
    //  * To fix this issue in .NET 7, we can use JsonDerivedType attribute, please check: https://www.youtube.com/watch?v=2zUQwVD7T_E&ab_channel=NickChapsas
    //  */
    // [Benchmark]
    // public void SystemTextJsonSerializerWithInterface()
    // {
    //     var a = System.Text.Json.JsonSerializer.Serialize(Imclass);
    // }









    // [Benchmark]
    // public void NewtonsoftJsonDeserializerWithClassImplementation()
    // {
    //     var a = Newtonsoft.Json.JsonConvert.DeserializeObject<MyClass>(serializeMClass);
    // }

    // [Benchmark]
    // public void SystemTextJsonDeserializerWithClassImplementation()
    // {
    //     var a = System.Text.Json.JsonSerializer.Deserialize<MyClass>(serializeMClass);
    // }

    // [Benchmark]
    // public void SystemTextJsonDeserializerSourceGeneratorDefaultWithClassImplementation()
    // {
    //     var a = System.Text.Json.JsonSerializer.Deserialize(serializeMClassWithDefaultSourceGenerator, MyClassDefaultJsonContext.Default.MyClass);
    //     //Console.WriteLine(a);
    // }

    // [Benchmark]
    // public void SystemTextJsonDeserializerSourceGeneratorMetadataWithClassImplementation()
    // {
    //     var a = System.Text.Json.JsonSerializer.Deserialize(serializeMClassWithMetadataSourceGenerator, MyClassMetadatatJsonContext.Default.MyClass);
    //     //Console.WriteLine(a);
    // }







    [Benchmark]
    public void SystemTextJsonStructWithNoSourceGeneratorSerializer()
    {
        var result = ResultWithNoJsonSourceGenerator.Success();
        _logger.Information("--Result: {@Result}", result);
        //_logger.StartResultWithNoSourcegenerator(result);
    }

    [Benchmark]
    public void SystemTextJsonStructSerializer()
    {
        Result result = Result.Success();
        _logger.Information("--Result: {@Result}", result);
        //_logger.StartResult(result);
    }

    [Benchmark]
    public void SystemTextJsonStructGenericWithNoSourceGeneratorSerializer()
    {
        ResultWithNoJsonSourceGenerator<MyClass> result = mclass;
        _logger.Information("--Result: {@Result}", result);
        //_logger.StartResultTWithNoSourcegenerator(result);
    }

    [Benchmark]
    public void SystemTextJsonStructGenericSerializer()
    {
        Result<MyClass> result = mclass;
        _logger.Information("--Result: {@Result}", result);
        //_logger.StartResultT(result);
    }





    [Benchmark]
    public void SystemTextJsonStructCustomSerializer()
    {
        Result result = Result.Success();
        var a = System.Text.Json.JsonSerializer.Serialize(result, ResultDefaultJsonContext.Default.Result);
        _logger.Information("--Result: {Result}", a);
        //_logger.StartResult(result);
    }

    [Benchmark]
    public void SystemTextJsonStructGenericCustomSerializer()
    {
        Result<MyClass> result = mclass;
        var a = System.Text.Json.JsonSerializer.Serialize(result, ResultGenericDefaultJsonContext.Default.ResultMyClass);
        _logger.Information("--Result: {Result}", result);
        //_logger.StartResult(result);
    }
}