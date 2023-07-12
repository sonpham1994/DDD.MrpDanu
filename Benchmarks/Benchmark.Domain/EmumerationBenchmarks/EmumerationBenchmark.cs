using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Domain.EmumerationBenchmarks;

[MemoryDiagnoser()]
public class EmumerationBenchmark
{
    [Benchmark]
    public void EnumerationWithToList()
    {
        RegionalMarket.CreateEnumerations();
    }

    [Benchmark]
    public void EnumerationWithToArray()
    {
        RegionalMarket.CreateEnumerationsWithToArray();
    }

    [Benchmark]
    public void EnumerationWithToFixedLengthArray()
    {
        RegionalMarket.CreateEnumerationsWithToFixedLengthArray();
    }

    [Benchmark]
    public void EnumerationWithToArrayAndCopyTo()
    {
        RegionalMarket.CreateEnumerationsWithToArrayAndCopyTo();
    }
}
