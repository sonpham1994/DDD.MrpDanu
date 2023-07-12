// See https://aka.ms/new-console-template for more information

using Benchmark.Application.LoggingMessage;
using BenchmarkDotNet.Running;
using Serilog;

// var log = new LoggerConfiguration()
//     .MinimumLevel.Information()
//     .WriteTo.Console()
//     .CreateLogger();

BenchmarkRunner.Run<LoggingMessageBenchmark>();

Console.WriteLine("Hello, World!");