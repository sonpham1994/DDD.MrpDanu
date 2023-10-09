using BenchmarkDotNet.Attributes;

namespace Benchmark.GuidBenchmark;

[MemoryDiagnoser()]
public class GuidToByteArrayAndInt
{
    [Benchmark]
    public void ConvertGuidToArray()
    {
        var a = Guid.NewGuid();
        var b= a.ToByteArray();
    }

    [Benchmark]
    public void ConvertGuidToNumberUsingByteArray()
    {
        var a = Guid.NewGuid();
        byte[] bytes = a.ToByteArray();

        // Extract some bytes and convert to integers
        int int1 = BitConverter.ToInt32(bytes, 0);  // First 4 bytes
        int int2 = BitConverter.ToInt32(bytes, 4);  // Next 4 bytes

        // Check if the integers are odd or even
        bool isInt1Odd = int1 % 2 != 0;
        bool isInt2Odd = int2 % 2 != 0;
    }

    [Benchmark]
    public void ConvertGuidToIntUsingGetHashCode()
    {
        var a = Guid.NewGuid();
        var b = a.GetHashCode();
    }
}