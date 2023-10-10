// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Benchmark.Infrastructure.ConnectionPoolings;
using Benchmark.Infrastructure.EnumerationLoading;

BenchmarkRunner.Run<EnumerationLoadingBenchmark>();
//var b = new EnumerationLoadingBenchmark();
//b.GetMaterialWithLazyLoading();
//b.GetMaterialWithoutLazyLoadingAndUseEnumeration();
Console.WriteLine("Hello, World!");