using BenchmarkDotNet.Attributes;

namespace Benchmark.StringBoxing;

//https://www.youtube.com/watch?v=bnVfrd3lRv8&t=1113s&ab_channel=NickChapsas

[MemoryDiagnoser()]
public class ValueTypeBoxingBenchmark
{
    [Params(10, 100, 1000)]
    public int Length { get; set; }
    
    [Benchmark]
    public void ValueTypeBoxing()
    {
        for (int i = 0; i < Length; i++)
        {
            int bc = int.MaxValue;
            LogMessage($"Max int is: {bc}", false);
        }
    }
    
    [Benchmark]
    public void ValueTypePreventingBoxing()
    {
        for (int i = 0; i < Length; i++)
        {
            int bc = int.MaxValue;
            LogMessagePreventingBoxing("Max int is: {Parans}", false, bc);
        }
    }
    
    [Benchmark]
    public void IntWithString2_1()
    {
        for (int i = 0; i < Length; i++)
        {
            int bc = 5;
            var str = $"This is a message {bc}";
        }
    }
    
    [Benchmark]
    public void IntWithString2_2()
    {
        for (int i = 0; i < Length; i++)
        {
            int bc = 5;
            var str = $"This is a message {bc.ToString()}";
        }
    }
    
    [Benchmark]
    public void IntWithString2_3()
    {
        for (int i = 0; i < Length; i++)
        {
            int bc = 5;
            var str = $"This is a message {bc switch
            {
                5 => "5", 
                _ => "0"
            }}";
        }
    }
    
    [Benchmark]
    public void EnumWithStringBoxing()
    {
        for (int i = 0; i < Length; i++)
        {
            var enumTest = EnumTest.One;
            var str = $"This is a message {enumTest}";
        }
    }
    
    [Benchmark]
    public void EnumWithStringBoxing2()
    {
        for (int i = 0; i < Length; i++)
        {
            var enumTest = EnumTest.One;
            var str = $"This is a message {enumTest.ToString()}";
        }
    }
    
    [Benchmark]
    public void EnumWithStringPreventBoxing2()
    {
        for (int i = 0; i < Length; i++)
        {
            var enumTest = EnumTest.One;
            var str = $"This is a message {enumTest switch
            {
                EnumTest.One => "One",
                EnumTest.Two => "Two",
                _ => "Zero"
            }}";
        }
    }

    private void LogMessage(string message, bool isLogging)
    {
        if (isLogging)
        {
        }
            
    }

    private void LogMessagePreventingBoxing<T>(string message, bool isLogging, T param)
    {
        if (isLogging)
        {
        }
    }
}