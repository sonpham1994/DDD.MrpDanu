using BenchmarkDotNet.Running;
using Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks;
using Benchmark.Domain.EntityGetHashCode;
using Benchmark.Domain.InKeywordForValueType;
using Benchmark.Domain.LinqExtensions.DistinctBenchmarks;

BenchmarkRunner.Run<InKeywordForValueType>();
Console.WriteLine("Hello, World!");
