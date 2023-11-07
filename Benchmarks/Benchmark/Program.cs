// See https://aka.ms/new-console-template for more information

using Benchmark.CastingObject;
using BenchmarkDotNet.Running;
using Benchmark.DynamicVsReflection;
using Benchmark.EmptyCollectionAndToList;
using Benchmark.GuidBenchmark;
using Benchmark.IEnumerableBenchmarks;
using Benchmark.JsonSerializerBenchmarks;
using Benchmark.MoveNextEnumerators;
using Benchmark.PassStructAsObjectParameter;
using Benchmark.RegexBenchmarks;
using Benchmark.SpanWithObjects;
using Benchmark.ValueTypeBoxingBenchmarks;
using Benchmark.StructWithIEquatable;
using Microsoft.Extensions.Logging;
using Benchmark.StringBenchmarks;

// Logger = new LoggerConfiguration()
//             .WriteTo.Console()

BenchmarkRunner.Run<JsonSerializerBenchmark>();

Console.WriteLine("Hello, World!");