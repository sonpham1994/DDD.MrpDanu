using BenchmarkDotNet.Attributes;

namespace Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;

[MemoryDiagnoser()]
public class ItemDuplicationWithAlgorithmAndHashCodeBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }

    public class MyClass : Entity
    {
        public string Name {get; set; }
    }
    
    [Benchmark]
    public void ItemDuplication_FirstElementDuplicateAtTheMiddlePosition_WithAlgorithm()
    {
        var myClasses = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            myClasses.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }
        myClasses.Insert(Length / 2, myClasses.FirstOrDefault());

        var t = myClasses.ItemDuplicationWithAlgorithm(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_FirstElementDuplicateAtTheMiddlePosition_WithAlgorithmAndHashCode()
    {
        var myClasses = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            myClasses.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }
        myClasses.Insert(Length / 2, myClasses.FirstOrDefault());

        var t = myClasses.ItemDuplicationWithAlgorithmAndHashCode(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_FirstElementDuplicateAtTheLastPosition_WithAlgorithm()
    {
        var myClasses = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            myClasses.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }
        myClasses.Add(myClasses.FirstOrDefault());

        var t = myClasses.ItemDuplicationWithAlgorithm(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_FirstElementDuplicateAtTheLastPosition_WithAlgorithmAndHashCode()
    {
        var myClasses = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            myClasses.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }
        myClasses.Add(myClasses.FirstOrDefault());

        var t = myClasses.ItemDuplicationWithAlgorithmAndHashCode(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_MiddleElementDuplicateAtTheLastPosition_WithAlgorithm()
    {
        var myClasses = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            myClasses.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }
        var element = myClasses[Length / 2];
        myClasses.Insert(Length - 1, element);

        var t = myClasses.ItemDuplicationWithAlgorithm(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_MiddleElementDuplicateAtTheLastPosition_WithAlgorithmAndHashCode()
    {
        var myClasses = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            myClasses.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }
        var element = myClasses[Length / 2];
        myClasses.Insert(Length - 1, element);

        var t = myClasses.ItemDuplicationWithAlgorithmAndHashCode(x => x);
    }
}