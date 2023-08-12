// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Benchmark.DynamicVsReflection;
using Benchmark.EmptyCollectionAndToList;
using Benchmark.IEnumerableBenchmarks;
using Benchmark.MoveNextEnumerators;
using Benchmark.PassStructAsObjectParameter;
using Benchmark.RegexBenchmarks;
using Benchmark.SpanWithObjects;
using Benchmark.ValueTypeBoxingBenchmarks;
using Benchmark.StructWithIEquatable;

BenchmarkRunner.Run<RegexBenchmark>();
Console.WriteLine("Hello, World!");