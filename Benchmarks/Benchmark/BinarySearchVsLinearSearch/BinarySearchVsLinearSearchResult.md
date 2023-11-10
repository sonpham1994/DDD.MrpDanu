| Method                | Length  | Mean            | Error         | StdDev         | Median          | Gen0   | Allocated |
|---------------------- |-------- |----------------:|--------------:|---------------:|----------------:|-------:|----------:|
| CompareSequentialGuid | 10000   |       191.27 ns |      0.653 ns |       0.545 ns |       191.49 ns |      - |         - |
| CreateGuid            | 10000   |       169.90 ns |      1.369 ns |       1.143 ns |       170.14 ns |      - |         - |
| CreateSequentialGuid  | 10000   |       311.55 ns |      2.167 ns |       2.027 ns |       311.06 ns | 0.0429 |     136 B |
| LinearSearch          | 10000   |    44,612.93 ns |    193.654 ns |     161.710 ns |    44,576.39 ns |      - |         - |
| BinarySearch          | 10000   |       304.53 ns |      2.518 ns |       2.232 ns |       303.86 ns |      - |         - |
| CompareSequentialGuid | 100000  |       190.99 ns |      3.900 ns |       6.728 ns |       187.51 ns |      - |         - |
| CreateGuid            | 100000  |       172.26 ns |      2.956 ns |       2.904 ns |       171.58 ns |      - |         - |
| CreateSequentialGuid  | 100000  |       313.05 ns |      2.666 ns |       2.226 ns |       312.54 ns | 0.0429 |     136 B |
| LinearSearch          | 100000  |   408,464.64 ns |  2,522.370 ns |   2,106.293 ns |   409,326.46 ns |      - |         - |
| BinarySearch          | 100000  |       304.57 ns |      3.621 ns |       3.024 ns |       305.22 ns |      - |         - |
| CompareSequentialGuid | 1000000 |       190.76 ns |      3.828 ns |       5.846 ns |       187.97 ns |      - |         - |
| CreateGuid            | 1000000 |        66.36 ns |      0.705 ns |       0.625 ns |        66.39 ns |      - |         - |
| CreateSequentialGuid  | 1000000 |       344.26 ns |     19.254 ns |      56.165 ns |       350.65 ns | 0.0432 |     136 B |
| LinearSearch          | 1000000 | 5,005,536.88 ns | 99,656.992 ns | 255,458.968 ns | 4,955,092.19 ns |      - |       4 B |
| BinarySearch          | 1000000 |       446.06 ns |      8.897 ns |       6.946 ns |       446.09 ns |      - |         - |