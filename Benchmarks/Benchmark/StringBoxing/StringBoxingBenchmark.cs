using BenchmarkDotNet.Attributes;

namespace Benchmark.StringBoxing;

//https://www.youtube.com/watch?v=bnVfrd3lRv8&t=1113s&ab_channel=NickChapsas

[MemoryDiagnoser()]
public class StringBoxingBenchmark
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