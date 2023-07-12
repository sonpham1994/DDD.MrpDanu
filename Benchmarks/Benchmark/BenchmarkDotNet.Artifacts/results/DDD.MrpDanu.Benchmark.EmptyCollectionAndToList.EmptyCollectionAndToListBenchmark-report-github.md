``` ini

BenchmarkDotNet=v0.13.5, OS=macOS Ventura 13.4 (22F66) [Darwin 22.5.0]
Intel Core i7-8569U CPU 2.80GHz (Coffee Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.304
  [Host]     : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2


```
|                                    Method |         Mean |     Error |    StdDev |    Gen0 |   Gen1 | Allocated |
|------------------------------------------ |-------------:|----------:|----------:|--------:|-------:|----------:|
|                 ToViewModelWithHavingData | 545,093.8 ns | 913.23 ns | 854.24 ns | 40.0391 | 0.9766 | 163.93 KB |
|    ToViewModelCheckingEmptyWithHavingData | 545,878.7 ns | 959.57 ns | 850.63 ns | 40.0391 | 0.9766 | 163.93 KB |
|              ToViewModelWithoutHavingData |     745.8 ns |   3.85 ns |   3.60 ns |  1.9493 |      - |   7.97 KB |
| ToViewModelCheckingEmptyWithoutHavingData |     659.7 ns |   3.22 ns |   2.69 ns |  1.9226 |      - |   7.87 KB |
