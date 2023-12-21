// See https://aka.ms/new-console-template for more information

using Benchmark.BoxingCompareTo;
using Benchmark.CastingObject;
using Benchmark.ConvertGuidToStringAndViceVersa;
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
using Benchmark.BoxingEntityEquals;
using Benchmark.ValueObjectEqualsBoxing;
using Benchmark.FirstOrDefaultVsFind;

// Logger = new LoggerConfiguration()
//             .WriteTo.Console()

BenchmarkRunner.Run<SequentialGuidBenchmark>();
//var a = new SequentialGuidBenchmark();
//a.Setup();
//a.CompareSequentialGuidWithSqlGuidMethod();
//a.ValueObjectEqualsBoxing();


Console.WriteLine("Hello, World!");