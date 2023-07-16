using BenchmarkDotNet.Running;
using Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;
using Benchmark.Domain.EntityGetHashCode;
using Benchmark.Domain.LinqExtensions.DistinctBenchmarks;

BenchmarkRunner.Run<ItemDuplicationWithCheckingTypeBenchmark>();
//var test = new DistinctBenchmark();
//test.DistinctWithDuplication();
Console.WriteLine("Hello, World!");
