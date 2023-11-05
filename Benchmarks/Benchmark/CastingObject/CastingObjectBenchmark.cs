using BenchmarkDotNet.Attributes;

namespace Benchmark.CastingObject;

[MemoryDiagnoser()]
public class CastingObjectBenchmark
{
    private MyClass _mClass1;
    private MyClass _mClass2;
    
    [Params(1, 10,100)]
    public int Length { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _mClass1 = new MyClass()
        {
            Id = Guid.NewGuid(),
            Name = "Name"
        };
        _mClass2 = new MyClass()
        {
            Id = Guid.NewGuid(),
            Name = "Name"
        };
    }
    
    [Benchmark]
    public void NoCastObject()
    {
        for (int i = 0; i < Length; i++)
        {
            var a = ComparingWithNoCast(_mClass1, _mClass2);
        }
    }
    
    [Benchmark]
    public void ComparingClassWithBaseClass()
    {
        for (int i = 0; i < Length; i++)
        {
            var a = ComparingWithBaseClass(_mClass1, _mClass2);
        }
    }
    
    [Benchmark]
    public void CastObject()
    {
        for (int i = 0; i < Length; i++)
        {
            var a = ComparingWithCast(_mClass1, _mClass2);
        }
    }

    private bool ComparingWithNoCast(MyClass mclass1, MyClass mclass2)
    {
        return mclass1.Id == mclass2.Id;
    }
    
    private bool ComparingWithBaseClass(BaseClass mclass1, BaseClass mclass2)
    {
        return mclass1.Id == mclass2.Id;
    }
    
    private bool ComparingWithCast(object mclass1, object mclass2)
    {
        if (mclass1 is MyClass m1)
        {
            if (mclass2 is MyClass m2)
            {
                return m1.Id == m2.Id;
            }
        }

        return false;
    }
}