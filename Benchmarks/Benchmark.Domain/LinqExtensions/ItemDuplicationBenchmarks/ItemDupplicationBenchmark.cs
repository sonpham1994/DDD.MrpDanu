using BenchmarkDotNet.Attributes;

namespace Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;

//https://www.youtube.com/watch?v=zYavFBwsJxE&ab_channel=MilanJovanovi%C4%87
[MemoryDiagnoser()]
public class ItemDuplicationBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }

    public class MyClass : Entity
    {
        public string Name {get; set; }
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithGroup()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Add(classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithGroup(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithForeachDictionary()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Add(classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithForeachDictionary(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithForeachHashSet()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Add(classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithForeachHashSet(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithForeachAny()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Add(classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithForeachAny(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithForAny()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Add(classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithForAny(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheLastPosition_WithForAlgorithm()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Add(classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithAlgorithm(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithGroup()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Insert(Length / 2, classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithGroup(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithForeachDictionary()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Insert(Length / 2, classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithForeachDictionary(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithForeachHashSet()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Insert(Length / 2, classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithForeachHashSet(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithForeachAny()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Insert(Length / 2, classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithForeachAny(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithForAny()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Insert(Length / 2, classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithForAny(x => x);
    }
    
    [Benchmark]
    public void ItemDuplication_AtTheMiddlePosition_WithAlgorithm()
    {
        var classes = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            classes.Add(new MyClass
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        classes.Insert(Length / 2, classes.FirstOrDefault());

        var t = classes.ItemDuplicationWithAlgorithm(x => x);
    }
}