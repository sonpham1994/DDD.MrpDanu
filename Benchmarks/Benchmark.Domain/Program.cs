using BenchmarkDotNet.Running;
using Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;
using Benchmark.Domain.EntityGetHashCode;
using Benchmark.Domain.LinqExtensions.DistinctBenchmarks;

//BenchmarkRunner.Run<ItemDuplicationWithLinearSearchAndBinarySearchBenchmark>();
var test = new ItemDuplicationWithLinearSearchAndBinarySearchBenchmark();
test.BinarySearchWithDuplicationAtNearMiddleAndLastPosition();
test.BinarySearchWithDuplicationAtFirstAndNearFirstPosition();
test.BinarySearchWithDuplicationAtFirstAndMiddlePosition();
Console.WriteLine("Hello, World!");
