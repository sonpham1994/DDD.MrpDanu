using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Benchmark.Domain.EntityGetHashCode.GetHashCodeBenchmark;

namespace Benchmark.Domain.EntityGetHashCode;

[MemoryDiagnoser]
public class GetHashCodeBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }
    private List<MyClassGuid> _myClassGuids = new List<MyClassGuid>();
    private List<MyClassInt> _myClassInts = new List<MyClassInt>();

    private static MyClassInt _myClassInt = new() { Id = 1 };
    private static MyClassGuid _myClassGuid = new() { Id = Guid.NewGuid() };

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < Length; i++)
        {
            _myClassGuids.Add(new MyClassGuid { Id = Guid.NewGuid() });
            _myClassInts.Add(new MyClassInt { Id = i + 1 });
        }
    }

    public class MyClassGuid
    {
        public Guid Id { get; set; }
    }

    public class MyClassInt
    {
        public int Id { get; set; }
    }

    #region Guid
    [Benchmark]
    public void GuidGetHashCodeWithNameAndId()
    {
        var a = (_myClassGuid.GetType().Name + _myClassGuid.Id).GetHashCode();
    }

    [Benchmark]
    public void GuidGetHashCodeWithToStringAndId()
    {
        var a = (_myClassGuid.GetType().ToString() + _myClassGuid.Id).GetHashCode();
    }

    [Benchmark]
    public void GuidGetHashCodeWithNameHashCodeAndIdHashCode()
    {
        var a = (_myClassGuid.GetType().Name.GetHashCode() + _myClassGuid.Id.GetHashCode()).GetHashCode();
    }

    [Benchmark]
    public void GuidGetListHashCodeWithNameAndId()
    {
        foreach (var myClassGuid in _myClassGuids)
        {
            var a = (myClassGuid.GetType().Name + myClassGuid.Id).GetHashCode();
        }
    }

    [Benchmark]
    public void GuidGetListHashCodeWithNameHashCodeAndIdHashCode()
    {
        foreach (var myClassGuid in _myClassGuids)
        {
            var a = (myClassGuid.GetType().Name.GetHashCode() + myClassGuid.Id.GetHashCode()).GetHashCode();
        }
    }
    #endregion

    #region Int
    [Benchmark]
    public void IntGetHashCodeWithNameAndId()
    {
        var a = (_myClassInt.GetType().Name + _myClassInt.Id).GetHashCode();
    }
    
    [Benchmark]
    public void IntGetHashCodeWithToStringAndId()
    {
        var a = (_myClassInt.GetType().ToString() + _myClassInt.Id).GetHashCode();
    }
    
    [Benchmark]
    public void IntGetHashCodeWithNameHashCodeAndIdHashCode()
    {
        var a = (_myClassInt.GetType().Name.GetHashCode() + _myClassInt.Id.GetHashCode()).GetHashCode();
    }
    
    [Benchmark]
    public void IntGetListHashCodeWithNameAndId()
    {
        foreach (var myClassInt in _myClassInts)
        {
            var a = (myClassInt.GetType().Name + myClassInt.Id).GetHashCode();
        }
    }
    
    [Benchmark]
    public void IntGetListHashCodeWithNameHashCodeAndIdHashCode()
    {
        foreach (var myClassInt in _myClassInts)
        {
            var a = (myClassInt.GetType().Name.GetHashCode() + myClassInt.Id.GetHashCode()).GetHashCode();
        }
    }
    #endregion
}
