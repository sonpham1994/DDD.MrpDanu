using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * https://www.youtube.com/watch?v=cLsmW7a8MkU&t=293s
 */
namespace Benchmark.MoveNextEnumerators;

[MemoryDiagnoser()]
public class MoveNextEnumeratorBenchmark
{
    [Params(1000)]
    public int Length { get; set; }
    public class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Benchmark]
    public void Foreach()
    {
        var list = GetEnumerable();

        foreach (var item in list)
        {
            if (item.Id == 500)
                return;
        }
    }

    [Benchmark]
    public void ForeachWithToList()
    {
        var list = GetEnumerable().ToList();

        foreach (var item in list)
        {
            if (item.Id == 500)
                return;
        }
    }

    [Benchmark]
    public void MoveNextWithEnumerator()
    {
        var list = GetEnumerable();
        var enumerator = list.GetEnumerator();
        
        while (enumerator.MoveNext())
        {
            var element = enumerator.Current;
            if (element.Id == 500)
                return;
        }
    }

    [Benchmark]
    public void MoveNextWithEnumeratorAndDispose()
    {
        var list = GetEnumerable();
        var enumerator = list.GetEnumerator();
        try 
        {
            while (enumerator.MoveNext())
            {
                var element = enumerator.Current;
                if (element.Id == 500)
                    return;
            }
        }
        finally
        {
            enumerator.Dispose();
        }
        
    }

    public IEnumerable<MyClass> GetEnumerable()
    {
        for (int i = 0; i < Length; i++)
        {
            yield return new MyClass()
            {
                Id = i,
                Name = "Name " + i
            };
        };
    }

}
