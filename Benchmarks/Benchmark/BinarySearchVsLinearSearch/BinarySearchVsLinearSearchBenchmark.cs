using BenchmarkDotNet.Attributes;

namespace AlgorithmsAndDataStructures_Part2.Benchmarks.BinarySearchVsLinearSearch;

[MemoryDiagnoser]
public class BinarySearchVsLinearSearchBenchmark
{
    private Guid[] _guids;
    private Guid[] _sequentialGuids;

    private Guid _itemFindInGuid;
    private Guid _itemFindInSequentialGuid;

    //public long ComparisonLinearSearch;
    //public long ComparisonBinarySearch;

    [Params(10_000, 100_000, 1_000_000)] 
    public int Length { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _guids = new Guid[Length];
        _sequentialGuids = new Guid[Length];
        for (int i = 0; i < Length; i++)
        {
            _guids[i] = Guid.NewGuid();
            _sequentialGuids[i] = SequentialGuid.New();
        }

        _itemFindInGuid = _guids[Length - 100];
        _itemFindInSequentialGuid = _sequentialGuids[Length - 100];
    }

    [Benchmark]
    public void CompareSequentialGuid()
    {
        var sequentialGuid1 = new Guid("bd8bab48-8778-4b7e-20fa-08dbe1be19f5");
        var sequentialGuid2 = new Guid("b8853244-d2fc-4918-20fb-08dbe1be19f5");

        var result = SequentialGuid.CompareTo(sequentialGuid1, sequentialGuid2);
    }

    [Benchmark]
    public void CreateGuid()
    {
        var a = Guid.NewGuid();
    }

    [Benchmark]
    public void CreateSequentialGuid()
    {
        var a = SequentialGuid.New();
    }

    [Benchmark]
    public void LinearSearch()
    {
        var a = linearSearch(_guids, _itemFindInGuid);
    }

    [Benchmark]
    public void BinarySearch()
    {
        var a = binarySearch(_sequentialGuids, _itemFindInSequentialGuid);
        // if (a == false)
        // {
        //     throw new InvalidOperationException("Binary Search not working");
        // }
    }

    private bool linearSearch(Guid[] data, Guid value)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == value)
            {
                //ComparisonLinearSearch++;
                    return true;
            }
                
            //ComparisonLinearSearch++;
        }

        return false;
    }
    
    private bool binarySearch(Guid[] data, Guid value)
    {
        int start = 0;
        int end = data.Length - 1;

        while (start <= end)
        {
            int middle = (start + end) / 2;

            if (SequentialGuid.CompareTo(data[middle], value) == 0)
            {
                //ComparisonBinarySearch++;
                return true;
            }
                
            else if (SequentialGuid.CompareTo(data[middle], value) < 0)
            {
                //ComparisonBinarySearch += 2;
                start = middle + 1;
            }
            else
                end = middle - 1;
        }

        return false;
    }
}