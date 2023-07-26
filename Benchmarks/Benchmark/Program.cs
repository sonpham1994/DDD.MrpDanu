// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Benchmark.DynamicVsReflection;
using Benchmark.EmptyCollectionAndToList;
using Benchmark.IEnumerableBenchmarks;
using Benchmark.MoveNextEnumerators;
using Benchmark.PassStructAsObjectParameter;
using Benchmark.StringBoxing;
using Benchmark.StructWithIEquatable;

BenchmarkRunner.Run<StructWithIEquatableAndClassBenchmark>();
Console.WriteLine("Hello, World!");