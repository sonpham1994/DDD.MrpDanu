using BenchmarkDotNet.Attributes;

namespace Benchmark.StructWithIEquatable;

[MemoryDiagnoser]
public class ValueTypeWithInKeyword
{
    [Benchmark]
    public void IntWithInKeyword()
    {
        int i = 5;
        PassIntWithInKeyword(i);
    }
    
    [Benchmark]
    public void IntWithRefKeyword()
    {
        int i = 1; 
        PassIntWithRefKeyword(ref i);
    }
    
    [Benchmark]
    public void IntWithOutKeyword()
    {
        PassIntWithOutKeyword(out int i);
    }

    [Benchmark]
    public void PassIntWithInKeyword(in int i)
    {
        var j = 5;
        if (i == j)
        {
            
        }
    }
    
    [Benchmark]
    public void PassIntWithRefKeyword(ref int i)
    {
        i = 6;
        var j = 5;
        if (i == j)
        {
            
        }
    }
    
    [Benchmark]
    public void PassIntWithOutKeyword(out int i)
    {
        i = 5;
        var j = 5;
        if (i == j)
        {
            
        }
    }
}