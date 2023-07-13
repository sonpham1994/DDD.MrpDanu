using BenchmarkDotNet.Attributes;

namespace Benchmark.DynamicVsReflection;

[MemoryDiagnoser()]
public class DynamicVsReflectionBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; } = 10;
    
    [Benchmark]
    public void GetIdReflection()
    {
        var test = getMyObject();
        var result = test.Select(x =>
        {
            return x.GetType().GetProperty("Id")!.GetValue(x, null)!.ToString();
        }).ToList();
    }
    
    [Benchmark]
    public void GetIdDynamic()
    {
        var test = getMyObject();
        var result = test.Select(x =>
        {
            dynamic d = x;

            return d.Id.ToString();
        }).ToList();
    }

    [Benchmark]
    public void GetIdByCheckingIfElse()
    {
        var test = getMyObject();
        var result = test.Select(x =>
        {
            if (x is Entity<Guid> d)
            {
                return d.Id.ToString();
            }

            return string.Empty;
            
        }).ToList();
    }

    [Benchmark]
    public void GetIdManually()
    {
        var test = getMyClasses();
        var result = test.Select(x =>
        {
            return x.Id.ToString();
        }).ToList();
    }

    private IReadOnlyList<MyClass> getMyClasses()
    {
        var result = new List<MyClass>(Length);
        
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        return result;
    }
    
    private IReadOnlyList<object> getMyObject()
    {
        var result = new List<MyClass>(Length);
        
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        return result;
    }
    
    public class MyClass : Entity<Guid>
    {
        public string Name { get; set; }
    }

    public abstract class Entity<T>
    {
        public T Id { get; set; }
    }
}