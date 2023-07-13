using BenchmarkDotNet.Attributes;
using Benchmark.EmptyCollectionAndToList.Models;

namespace Benchmark.EmptyCollectionAndToList;

[MemoryDiagnoser]
public class EmptyCollectionAndToListBenchmark
{
    public const int Length = 1000;
    
    [Benchmark]
    public void ToViewModelWithHavingData()
    {
        var myclasses = new List<MyClass>(Length);
        
        for (int i = 0; i < Length; i++)
        {
            myclasses.Add(new MyClass()
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        var result = myclasses.ToViewModel();
    }
    
    [Benchmark]
    public void ToViewModelCheckingEmptyWithHavingData()
    {
        var myclasses = new List<MyClass>(Length);
        
        for (int i = 0; i < Length; i++)
        {
            myclasses.Add(new MyClass()
            {
                Id = Guid.NewGuid(),
                Name = "Name " + i
            });
        }

        var result = myclasses.ToViewModelCheckingEmpty();
    }
    
    [Benchmark]
    public void ToViewModelWithoutHavingData()
    {
        var myclasses = new List<MyClass>(Length);
        
        var result = myclasses.ToViewModel();
    }
    
    [Benchmark]
    public void ToViewModelCheckingEmptyWithoutHavingData()
    {
        var myclasses = new List<MyClass>(Length);
        
        var result = myclasses.ToViewModelCheckingEmpty();
    }
}