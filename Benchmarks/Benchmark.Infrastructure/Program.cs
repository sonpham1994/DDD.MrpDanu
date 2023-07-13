// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Benchmark.Infrastructure.ConnectionPoolings;

BenchmarkRunner.Run<ConnectionPoolingBenchmark>();
Console.WriteLine("Hello, World!");