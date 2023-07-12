``` ini

BenchmarkDotNet=v0.13.5, OS=macOS Ventura 13.4.1 (22F82) [Darwin 22.5.0]
Intel Core i7-8569U CPU 2.80GHz (Coffee Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.304
  [Host]     : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2


```
|                                          Method | Length |         Mean |      Error |     StdDev |    Gen0 | Allocated |
|------------------------------------------------ |------- |-------------:|-----------:|-----------:|--------:|----------:|
|                     **IntGetHashCodeWithNameAndId** |     **10** |     **44.60 ns** |   **0.306 ns** |   **0.286 ns** |  **0.0114** |      **48 B** |
|                 IntGetHashCodeWithToStringAndId |     10 |    124.00 ns |   0.833 ns |   0.739 ns |  0.0381 |     160 B |
|     IntGetHashCodeWithNameHashCodeAndIdHashCode |     10 |     22.93 ns |   0.480 ns |   0.401 ns |       - |         - |
|                 IntGetListHashCodeWithNameAndId |     10 |    459.72 ns |   2.496 ns |   2.084 ns |  0.1221 |     512 B |
| IntGetListHashCodeWithNameHashCodeAndIdHashCode |     10 |    295.10 ns |   0.476 ns |   0.371 ns |       - |         - |
|                     **IntGetHashCodeWithNameAndId** |    **100** |     **44.53 ns** |   **0.891 ns** |   **1.190 ns** |  **0.0114** |      **48 B** |
|                 IntGetHashCodeWithToStringAndId |    100 |    118.94 ns |   1.260 ns |   1.401 ns |  0.0381 |     160 B |
|     IntGetHashCodeWithNameHashCodeAndIdHashCode |    100 |     23.08 ns |   0.156 ns |   0.130 ns |       - |         - |
|                 IntGetListHashCodeWithNameAndId |    100 |  5,663.27 ns | 113.299 ns | 100.436 ns |  1.8387 |    7712 B |
| IntGetListHashCodeWithNameHashCodeAndIdHashCode |    100 |  2,466.83 ns |   5.095 ns |   4.255 ns |       - |         - |
|                     **IntGetHashCodeWithNameAndId** |   **1000** |     **42.95 ns** |   **0.205 ns** |   **0.171 ns** |  **0.0114** |      **48 B** |
|                 IntGetHashCodeWithToStringAndId |   1000 |    117.27 ns |   0.532 ns |   0.415 ns |  0.0381 |     160 B |
|     IntGetHashCodeWithNameHashCodeAndIdHashCode |   1000 |     22.79 ns |   0.032 ns |   0.028 ns |       - |         - |
|                 IntGetListHashCodeWithNameAndId |   1000 | 58,624.40 ns | 264.873 ns | 247.762 ns | 19.0430 |   79724 B |
| IntGetListHashCodeWithNameHashCodeAndIdHashCode |   1000 | 25,108.00 ns |  93.304 ns |  82.712 ns |       - |         - |
