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

// Logger = new LoggerConfiguration()
//             .WriteTo.Console()

BenchmarkRunner.Run<ReplaceCharacterVsStringBenchmark>();
//var a = new ConvertGuidToStringAndViceVersaBenchmark();
//a.Setup();
// a.ToBase64StringFromGuid();
// a.ToUrlFriendlyBase64StringFromGuid();
// a.ToUrlFriendlyBase64StringFromGuidOp();
//a.ToUrlFriendlyBase64StringFromGuidOpWithTryWriteBytes();
// a.ToUrlFriendlyBase64StringFromGuidOp_SteveGordon();
//a.ToUrlFriendlyBase64StringFromGuidOp_SteveGordonWithTryWriteBytes();
// a.ToGuidFromString();
// a.ToGuidFromStringOp();

Console.WriteLine("Hello, World!");