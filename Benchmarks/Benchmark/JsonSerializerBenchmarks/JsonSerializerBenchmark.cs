using BenchmarkDotNet.Attributes;

namespace Benchmark.JsonSerializerBenchmarks;

//https://www.linkedin.com/posts/thisisnabi_csharp-dotnet-performance-activity-7118264068355829761-L1aH/?utm_source=share&utm_medium=member_ios&fbclid=IwAR38yk53cvq4yPi0-rSNbzYYCnmzU7RzdEn0ojRwZrslZ_dYAJjgSRqfHKw
[MemoryDiagnoser()]
public class JsonSerializerBenchmark
{
    public class MyClass
    {
        public Guid Id {get; set; }
        public string Name { get; set; }
        public List<MyClass2> MyClass2 { get; set; }
        public MyClass3 MyClass3 { get; set; }
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

    [Params(1, 10, 100)]
    public int Length { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < Length; i++)
        {
            
        }
    }

    public void NewtonsoftJsonSerializer()
    {
        
    }
}