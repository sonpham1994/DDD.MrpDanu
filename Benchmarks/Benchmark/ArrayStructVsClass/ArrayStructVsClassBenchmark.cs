using BenchmarkDotNet.Attributes;

namespace Benchmark.ArrayStructVsClass;

[MemoryDiagnoser]
public class ArrayStructVsClassBenchmark
{
    [Params(10, 100, 1000)] 
    public int Length { get; set; }
    
    private StructTest[] _structTest1;
    private StructTest[] _structTest2;
    private StructTest[] _structTest3;
    
    private ClassTest[] _classTest1;
    private ClassTest[] _classTest2;
    private ClassTest[] _classTest3;

    [IterationSetup]
    public void IterationSetup()
    {
        _structTest1 = new StructTest[Length];
        _structTest2 = new StructTest[Length];
        _structTest3 = new StructTest[Length];
        
        _classTest1 = new ClassTest[Length];
        _classTest2 = new ClassTest[Length];
        _classTest3 = new ClassTest[Length];

        for (int i = 0; i < Length; i++)
        {
            _classTest1[i] = new();
            _classTest2[i] = new();
            _classTest3[i] = new();
        }
    }
    
    [Benchmark]
    public void ArrayStruct_AssigningData()
    {
        for (int i = 0; i < Length; i++)
        {
            _structTest1[i].Id = i;
        }
    }
    
    [Benchmark]
    public void ArrayStruct_AssigningStruct()
    {
        for (int i = 0; i < Length; i++)
        {
            _structTest2[i] = new StructTest() {Id = i};
        }
    }
    
    [Benchmark]
    public void ArrayStruct_CheckAndAssigningData()
    {
        for (int i = 0; i < Length; i++)
        {
            if (i % 2 == 0)
            {
                _structTest3[i].Id = i;
            }
        }
    }
    
    
    
    
    
    [Benchmark]
    public void ArrayClass_AssigningData()
    {
        for (int i = 0; i < Length; i++)
        {
            _classTest1[i].Id = i;
        }
    }
    
    [Benchmark]
    public void ArrayClass_AssigningClass()
    {
        for (int i = 0; i < Length; i++)
        {
            _classTest2[i] = new ClassTest() {Id = i};
        }
    }
    
    [Benchmark]
    public void ArrayClass_CheckAndAssigningData()
    {
        for (int i = 0; i < Length; i++)
        {
            if (i % 2 == 0)
            {
                _classTest3[i].Id = i;
            }
        }
    }

    private class ClassTest
    {
        public int Id { get; set; }
    }

    private struct StructTest
    {
        public int Id { get; set; }
    }
}