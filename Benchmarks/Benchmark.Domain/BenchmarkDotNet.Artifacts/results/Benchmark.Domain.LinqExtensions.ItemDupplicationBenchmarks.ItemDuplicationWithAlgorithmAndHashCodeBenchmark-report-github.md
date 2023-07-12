``` ini

BenchmarkDotNet=v0.13.5, OS=macOS Ventura 13.4.1 (22F82) [Darwin 22.5.0]
Intel Core i7-8569U CPU 2.80GHz (Coffee Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.304
  [Host]     : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2


```
|                                                                              Method | Length |          Mean |       Error |      StdDev |      Gen0 |    Gen1 |   Allocated |
|------------------------------------------------------------------------------------ |------- |--------------:|------------:|------------:|----------:|--------:|------------:|
|              **ItemDuplication_FirstElementDuplicateAtTheMiddlePosition_WithAlgorithm** |     **10** |      **5.659 μs** |   **0.0287 μs** |   **0.0254 μs** |    **0.3738** |       **-** |     **1.55 KB** |
|             ItemDuplication_FirstElementDuplicateAtTheLastPosition_WithForAlgorithm |     10 |      6.105 μs |   0.1102 μs |   0.1432 μs |    0.4883 |       - |     2.02 KB |
|   ItemDuplication_FirstElementDuplicateAtTheMiddlePosition_WithAlgorithmAndHashCode |     10 |      5.618 μs |   0.0163 μs |   0.0152 μs |    0.2823 |       - |     1.18 KB |
|  ItemDuplication_FirstElementDuplicateAtTheLastPosition_WithForAlgorithmAndHashCode |     10 |      5.783 μs |   0.0295 μs |   0.0246 μs |    0.2823 |       - |     1.18 KB |
|               ItemDuplication_MiddleElementDuplicateAtTheLastPosition_WithAlgorithm |     10 |      8.401 μs |   0.0368 μs |   0.0345 μs |    1.2665 |       - |     5.21 KB |
| ItemDuplication_MiddleElementDuplicateAtTheLastPosition_WithForAlgorithmAndHashCode |     10 |      7.055 μs |   0.1137 μs |   0.1064 μs |    0.2823 |       - |     1.18 KB |
|              **ItemDuplication_FirstElementDuplicateAtTheMiddlePosition_WithAlgorithm** |    **100** |     **55.627 μs** |   **0.8795 μs** |   **0.8227 μs** |    **4.3945** |       **-** |    **18.14 KB** |
|             ItemDuplication_FirstElementDuplicateAtTheLastPosition_WithForAlgorithm |    100 |     58.802 μs |   0.9313 μs |   0.8712 μs |    5.5542 |       - |    22.83 KB |
|   ItemDuplication_FirstElementDuplicateAtTheMiddlePosition_WithAlgorithmAndHashCode |    100 |     55.041 μs |   0.9359 μs |   1.3422 μs |    3.2959 |       - |    13.55 KB |
|  ItemDuplication_FirstElementDuplicateAtTheLastPosition_WithForAlgorithmAndHashCode |    100 |     56.444 μs |   0.2429 μs |   0.2153 μs |    3.2959 |       - |    13.55 KB |
|               ItemDuplication_MiddleElementDuplicateAtTheLastPosition_WithAlgorithm |    100 |    292.140 μs |   1.1920 μs |   1.1150 μs |   90.8203 |       - |   371.95 KB |
| ItemDuplication_MiddleElementDuplicateAtTheLastPosition_WithForAlgorithmAndHashCode |    100 |    160.447 μs |   0.3790 μs |   0.3360 μs |    3.1738 |       - |    13.55 KB |
|              **ItemDuplication_FirstElementDuplicateAtTheMiddlePosition_WithAlgorithm** |   **1000** |    **551.106 μs** |   **2.1255 μs** |   **1.9882 μs** |   **43.9453** |       **-** |   **179.87 KB** |
|             ItemDuplication_FirstElementDuplicateAtTheLastPosition_WithForAlgorithm |   1000 |    583.328 μs |   4.2324 μs |   3.9590 μs |   54.6875 |  2.9297 |   226.74 KB |
|   ItemDuplication_FirstElementDuplicateAtTheMiddlePosition_WithAlgorithmAndHashCode |   1000 |    540.352 μs |   3.1019 μs |   2.5902 μs |   32.2266 |  0.9766 |   133.09 KB |
|  ItemDuplication_FirstElementDuplicateAtTheLastPosition_WithForAlgorithmAndHashCode |   1000 |    566.128 μs |   2.4612 μs |   2.0552 μs |   32.2266 |  0.9766 |   133.09 KB |
|               ItemDuplication_MiddleElementDuplicateAtTheLastPosition_WithAlgorithm |   1000 | 24,570.913 μs |  77.6258 μs |  68.8133 μs | 8656.2500 | 31.2500 | 35359.49 KB |
| ItemDuplication_MiddleElementDuplicateAtTheLastPosition_WithForAlgorithmAndHashCode |   1000 | 11,241.612 μs | 224.3452 μs | 342.5988 μs |   31.2500 |       - |   133.11 KB |
