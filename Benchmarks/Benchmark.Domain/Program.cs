using BenchmarkDotNet.Running;
using Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;
using Benchmark.Domain.EntityGetHashCode;

BenchmarkRunner.Run<ItemDuplicationWithAlgorithmAndHashCodeBenchmark>();
//var test = new EntityGetHashCodeBenchmark();
//test.CacheGroupWithNameIntGetHashCode();
Console.WriteLine("Hello, World!");
