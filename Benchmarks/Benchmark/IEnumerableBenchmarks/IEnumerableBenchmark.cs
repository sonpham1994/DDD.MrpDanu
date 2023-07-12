using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.IEnumerableBenchmarks;

[MemoryDiagnoser()]
public class IEnumerableBenchmark
{
    [Params(1000)]
    public int Length { get; set; }
    public class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Benchmark]
    public void IEnumerableWithToList()
    {
        IEnumerable<MyClass> result = getEnumerable().ToList();
    }
    
    [Benchmark]
    public void IEnumerableWithToArray()
    {
        IEnumerable<MyClass> result = getEnumerable().ToArray();
    }
    
    [Benchmark]
    public void IEnumerableWithList()
    {
        IEnumerable<MyClass> result = getIEnumerableWithList();
    }
    
    [Benchmark]
    public void IEnumerableWithArrayAndFixedLength()
    {
        IEnumerable<MyClass> result = getIEnumerableWithArrayAndFixedLength();
    }
    
    [Benchmark]
    public void List()
    {
        List<MyClass> result = getList();
    }
    
    [Benchmark]
    public void ListNotReturn()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };
    }
    
    [Benchmark]
    public void ListWithTrimExcess()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };
        result.TrimExcess();
    }

    [Benchmark]
    public void ListWithDefiningCapacity()
    {
        var result = getListWithDefiningCapacity();
    }
    
    [Benchmark]
    public void ListWithExceedingCapacity()
    {
        var result = getListWithExceedingCapacity();
    }
    
    
    [Benchmark]
    public void Array()
    {
        var result = getArrayWithFixedLength();
    }
    
    [Benchmark]
    public void IReadOnlyCollection()
    {
        IReadOnlyCollection<MyClass> result = getReadOnlyCollection();
    }
    
    [Benchmark]
    public void IReadOnlyList()
    {
        IReadOnlyCollection<MyClass> result = getReadOnlyList();
    }
    
    [Benchmark]
    public void IReadOnlyCollectionWithToListFromIEnumerable()
    {
        IReadOnlyCollection<MyClass> result = getEnumerable().ToList();
    }
    
    [Benchmark]
    public void IReadOnlyListWithToListFromIEnumerable()
    {
        IReadOnlyList<MyClass> result = getEnumerable().ToList();
    }
    
    [Benchmark]
    public void IReadOnlyCollectionWithToArrayFromIEnumerable()
    {
        IReadOnlyCollection<MyClass> result = getEnumerable().ToArray();
    }
    
    [Benchmark]
    public void IReadOnlyListWithToArrayFromIEnumerable()
    {
        IReadOnlyList<MyClass> result = getEnumerable().ToArray();
    }
    
    [Benchmark]
    public void IReadOnlyCollectionWithToListFromIEnumerableWithoutDefering()
    {
        IReadOnlyCollection<MyClass> result = getIEnumerableWithList().ToList();
    }
    
    [Benchmark]
    public void IReadOnlyListWithToListFromIEnumerableWithoutDefering()
    {
        IReadOnlyList<MyClass> result = getIEnumerableWithList().ToList();
    }
    
    [Benchmark]
    public void IReadOnlyCollectionWithToArrayFromIEnumerableWithoutDefering()
    {
        IReadOnlyCollection<MyClass> result = getIEnumerableWithList().ToArray();
    }
    
    [Benchmark]
    public void IReadOnlyListWithToArrayFromIEnumerableWithoutDefering()
    {
        IReadOnlyList<MyClass> result = getIEnumerableWithList().ToArray();
    }
    
    [Benchmark]
    public void IReadOnlyCollectionWithTrimExcess()
    {
        IReadOnlyCollection<MyClass> result = getReadOnlyCollectionWithTrimExcess();
    }
    
    [Benchmark]
    public void IReadOnlyListWithTrimExcess()
    {
        IReadOnlyCollection<MyClass> result = getReadOnlyListWithTrimExcess();
    }
    
    [Benchmark]
    public void IReadOnlyCollectionWithArrayAndFixedLength()
    {
        IReadOnlyCollection<MyClass> result = getReadOnlyCollectionWithArrayAndFixedLength();
    }
    
    [Benchmark]
    public void IReadOnlyListWithArrayAndFixedLength()
    {
        IReadOnlyList<MyClass> result = getReadOnlyListWithArrayAndFixedLength();
    }
    
    [Benchmark]
    public void IReadOnlyCollectionWithToArray()
    {
        IReadOnlyCollection<MyClass> result = getReadOnlyCollectionWithToArray();
    }
    
    [Benchmark]
    public void IReadOnlyListWithToArray()
    {
        IReadOnlyList<MyClass> result = getReadOnlyListWithToArray();
    }
    
    private IEnumerable<MyClass> getEnumerable()
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

    private IEnumerable<MyClass> getIEnumerableWithList()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };

        return result;
    }
    
    private List<MyClass> getList()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };

        return result;
    }
    
    private List<MyClass> getListWithDefiningCapacity()
    {
        var result = new List<MyClass>(Length);
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };

        return result;
    }
    
    private List<MyClass> getListWithExceedingCapacity()
    {
        var length = Length + 1;
        var result = new List<MyClass>(Length);
        for (int i = 0; i < length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };

        return result;
    }
    
    private MyClass[] getArrayWithFixedLength()
    {
        var result = new MyClass[Length];
        for (int i = 0; i < Length; i++)
        {
            var t = new MyClass()
            {
                Id = i,
                Name = "Name " + i
            };
            result[i] = t;
        };

        return result;
    }

    private IEnumerable<MyClass> getIEnumerableWithArrayAndFixedLength()
    {
        var result = new MyClass[Length];
        for (int i = 0; i < Length; i++)
        {
            var t = new MyClass()
            {
                Id = i,
                Name = "Name " + i
            };
            result[i] = t;
        };

        return result;
    }

    private IReadOnlyCollection<MyClass> getReadOnlyCollection()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };

        return result;
    }

    private IReadOnlyList<MyClass> getReadOnlyList()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };

        return result;
    }
    
    private IReadOnlyCollection<MyClass> getReadOnlyCollectionWithTrimExcess()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };
        result.TrimExcess();
        return result;
    }

    private IReadOnlyList<MyClass> getReadOnlyListWithTrimExcess()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            result.Add(new MyClass()
            {
                Id = i,
                Name = "Name " + i
            });
        };

        result.TrimExcess();
        return result;
    }

    private IReadOnlyCollection<MyClass> getReadOnlyCollectionWithArrayAndFixedLength()
    {
        var result = new MyClass[Length];
        for (int i = 0; i < Length; i++)
        {
            var t = new MyClass()
            {
                Id = i,
                Name = "Name " + i
            };
            result[i] = t;
        };

        return result;
    }

    private IReadOnlyList<MyClass> getReadOnlyListWithArrayAndFixedLength()
    {
        var result = new MyClass[Length];
        for (int i = 0; i < Length; i++)
        {
            var t = new MyClass()
            {
                Id = i,
                Name = "Name " + i
            };
            result[i] = t;
        };

        return result;
    }

    private IReadOnlyCollection<MyClass> getReadOnlyCollectionWithToArray()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            var t = new MyClass()
            {
                Id = i,
                Name = "Name " + i
            };
            result.Add(t);
        };

        return result.ToArray();
    }

    private IReadOnlyList<MyClass> getReadOnlyListWithToArray()
    {
        var result = new List<MyClass>();
        for (int i = 0; i < Length; i++)
        {
            var t = new MyClass()
            {
                Id = i,
                Name = "Name " + i
            };
            result.Add(t);
        };

        return result.ToArray();
    }
}
