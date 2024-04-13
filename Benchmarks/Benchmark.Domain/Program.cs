using BenchmarkDotNet.Running;
using Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;
using Benchmark.Domain.EntityGetHashCode;
using Benchmark.Domain.InKeywordForValueType;
using Benchmark.Domain.LinqExtensions.DistinctBenchmarks;
using Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks.EntityDuplication;

BenchmarkRunner.Run<EntityDuplicationBenchmark>();
Console.WriteLine("Hello, World!");
