// See https://aka.ms/new-console-template for more information

using Benchmark.Infrastructure.CacheDelegateFunction;
using Benchmark.Infrastructure.CacheEntityAuditChanged;
using BenchmarkDotNet.Running;
using Benchmark.Infrastructure.ConnectionPoolings;
using Benchmark.Infrastructure.EnumerationLoading;

BenchmarkRunner.Run<CacheDelegateFunctionBenchmark>();
// var a = new CacheEntityAuditChangedBenchmark();
// a.Setup();
// a.CachedGetEntityAuditChanged();
// a.WithoutCachedGetEntityAuditChanged();

Console.WriteLine("Hello, World!");