using BenchmarkDotNet.Attributes;

/*
 * https://www.youtube.com/watch?v=bnVfrd3lRv8&t=870s&ab_channel=NickChapsas
 * https://www.youtube.com/watch?v=a26zu-pyEyg&t=28s&ab_channel=NickChapsas
 */
namespace Benchmark.StringBenchmarks;

[MemoryDiagnoser()]
public class StringBenchmark
{
    public const string CodeTemplate = "Material.NotFoundId";
    public static string StaticCodeTemplate = "Material.NotFoundId";

    [Params(10, 100, 1000, 10000)]
    public int Length { get; set; }

    [Benchmark]
    public void CreateStaticString()
    {
        for (int i = 0; i < Length; i++)
        {
            var a = StaticCodeTemplate;
        }
    }

    [Benchmark]
    public void CreateConstString()
    {
        for (int i = 0; i < Length; i++)
        {
            var a = CodeTemplate;
        }
    }

    [Benchmark]
    public void UsingEmptyStringNormalWay()
    {
        for (int i = 0; i < Length; i++)
        {
            string a = "";
        }
    }

    [Benchmark]
    public void UsingEmptyStringProvidedByFramework()
    {
        for (int i = 0; i < Length; i++)
        {
            string a = string.Empty;
        }
    }
}