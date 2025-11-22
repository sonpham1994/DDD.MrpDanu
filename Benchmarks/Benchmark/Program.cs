// See https://aka.ms/new-console-template for more information

using Benchmark;
using Benchmark.ArrayStructVsClass;
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
using Benchmark.RecordStructs;
using Benchmark.ExistsOrAny;
using Benchmark.ReadOnlySpan;

// Logger = new LoggerConfiguration()
//             .WriteTo.Console()

BenchmarkRunner.Run<ArrayStructVsClassBenchmark>();
//var a = new SequentialGuidBenchmark();
//a.Setup();
//a.CompareSequentialGuidWithSqlGuidMethod();
//a.ValueObjectEqualsBoxing();
