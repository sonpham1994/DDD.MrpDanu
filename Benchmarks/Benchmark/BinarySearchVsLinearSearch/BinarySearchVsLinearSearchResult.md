| Method                | Length  | Mean           | Error        | StdDev       | Gen0   | Gen1   | Allocated |
|---------------------- |-------- |---------------:|-------------:|-------------:|-------:|-------:|----------:|
| CompareSequentialGuid | 10000   |       108.9 ns |      0.79 ns |      0.62 ns |      - |      - |         - |
| CreateGuid            | 10000   |       450.7 ns |      0.85 ns |      0.66 ns |      - |      - |         - |
| CreateSequentialGuid  | 10000   |       553.3 ns |      0.88 ns |      0.74 ns | 0.0229 | 0.0010 |      96 B |
| LinearSearch          | 10000   |    31,260.1 ns |    120.31 ns |    100.47 ns |      - |      - |         - |
| BinarySearch          | 10000   |       608.3 ns |      0.60 ns |      0.47 ns |      - |      - |         - |
| CompareSequentialGuid | 100000  |       109.2 ns |      1.76 ns |      1.64 ns |      - |      - |         - |
| CreateGuid            | 100000  |       453.5 ns |      4.74 ns |      3.96 ns |      - |      - |         - |
| CreateSequentialGuid  | 100000  |       552.8 ns |      1.54 ns |      1.29 ns | 0.0229 |      - |      96 B |
| LinearSearch          | 100000  |   323,392.9 ns |  1,549.16 ns |  1,293.62 ns |      - |      - |         - |
| BinarySearch          | 100000  |       704.3 ns |      2.49 ns |      2.08 ns |      - |      - |         - |
| CompareSequentialGuid | 1000000 |       107.0 ns |      0.15 ns |      0.13 ns |      - |      - |         - |
| CreateGuid            | 1000000 |       452.1 ns |      0.92 ns |      0.77 ns |      - |      - |         - |
| CreateSequentialGuid  | 1000000 |       552.3 ns |      1.29 ns |      1.15 ns | 0.0229 |      - |      96 B |
| LinearSearch          | 1000000 | 3,414,674.4 ns | 12,471.45 ns | 10,414.22 ns |      - |      - |       4 B |
| BinarySearch          | 1000000 |       940.9 ns |     17.97 ns |     42.00 ns |      - |      - |         - |


Length: 10_000 - Item found in 9900 index
Comparison in LinearSearch: 9901
Comparison in BinarySearch: 23

Length: 100_000 - Item found in 99000 index
Comparison in LinearSearch: 99901
Comparison in BinarySearch: 29

Length: 1_000_000 - Item found in 990000 index
Comparison in LinearSearch: 999901
Comparison in BinarySearch: 31

