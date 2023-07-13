using BenchmarkDotNet.Running;
using Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;
using Benchmark.Domain.EntityGetHashCode;
using Benchmark.Domain.LinqExtensions.DistinctBenchmarks;

BenchmarkRunner.Run<DistinctBenchmark>();
//var test = new EntityGetHashCodeBenchmark();
//test.CacheGroupWithNameIntGetHashCode();
Console.WriteLine("Hello, World!");
