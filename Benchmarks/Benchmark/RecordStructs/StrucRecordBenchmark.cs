using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.RecordStructs;

[MemoryDiagnoser()]
public class StrucRecordBenchmark
{
    [Benchmark]
    public void NotEqualsTheFirstItem()
    {
        var a = new RecordStruct2(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        var b = new RecordStruct2(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        var result = a == b;
    }

    [Benchmark]
    public void NotEqualsTheSecondItem()
    {
        var id1 = Guid.NewGuid();
        var a = new RecordStruct2(id1, Guid.NewGuid(), Guid.NewGuid());
        var b = new RecordStruct2(id1, Guid.NewGuid(), Guid.NewGuid());
        var result = a == b;
    }

    [Benchmark]
    public void NotEqualsTheThirdItem()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var a = new RecordStruct2(id1, id2, Guid.NewGuid());
        var b = new RecordStruct2(id1, id2, Guid.NewGuid());
        var result = a == b;
    }

    [Benchmark]
    public void EqualsAllItems()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var id3 = Guid.NewGuid();
        var a = new RecordStruct2(id1, id2, id3);
        var b = new RecordStruct2(id1, id2, id3);
        var result = a == b;
    }
}
