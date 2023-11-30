| Method                                            | Length  | Mean           | Error        | StdDev       | Gen0   | Gen1   | Allocated |
|-------------------------------------------------- |-------- |---------------:|-------------:|-------------:|-------:|-------:|----------:|
| CompareSequentialGuid                             | 10000   |       110.5 ns |      2.21 ns |      1.84 ns |      - |      - |         - |
| CreateGuid                                        | 10000   |       446.9 ns |      2.01 ns |      1.68 ns |      - |      - |         - |
| CreateSequentialGuid                              | 10000   |       547.2 ns |      8.60 ns |      8.05 ns | 0.0229 | 0.0010 |      96 B |
| LinearSearch                                      | 10000   |    31,364.2 ns |    601.38 ns |    781.97 ns |      - |      - |         - |
| BinarySearch                                      | 10000   |       630.7 ns |      6.83 ns |      6.39 ns |      - |      - |         - |
| BinarySearchWithBitwiseRightShiftOperator         | 10000   |       616.6 ns |      6.74 ns |      5.63 ns |      - |      - |         - |
| LinearSearchNotFound                              | 10000   |    36,628.1 ns |    582.13 ns |    544.52 ns |      - |      - |         - |
| BinarySearchNotFound                              | 10000   |       871.2 ns |     13.18 ns |     11.69 ns |      - |      - |         - |
| BinarySearchWithBitwiseRightShiftOperatorNotFound | 10000   |       830.7 ns |      3.87 ns |      3.43 ns |      - |      - |         - |
| CompareSequentialGuid                             | 100000  |       107.4 ns |      0.07 ns |      0.06 ns |      - |      - |         - |
| CreateGuid                                        | 100000  |       440.8 ns |      0.65 ns |      0.50 ns |      - |      - |         - |
| CreateSequentialGuid                              | 100000  |       537.8 ns |      2.17 ns |      1.81 ns | 0.0229 |      - |      96 B |
| LinearSearch                                      | 100000  |   318,026.8 ns |  4,551.32 ns |  4,034.63 ns |      - |      - |         - |
| BinarySearch                                      | 100000  |       706.1 ns |      5.85 ns |      5.47 ns |      - |      - |         - |
| BinarySearchWithBitwiseRightShiftOperator         | 100000  |       711.2 ns |      6.12 ns |      5.43 ns |      - |      - |         - |
| LinearSearchNotFound                              | 100000  |   366,424.5 ns |  6,579.81 ns |  6,756.98 ns |      - |      - |         - |
| BinarySearchNotFound                              | 100000  |       930.9 ns |      0.61 ns |      0.48 ns |      - |      - |         - |
| BinarySearchWithBitwiseRightShiftOperatorNotFound | 100000  |       911.5 ns |      1.37 ns |      1.07 ns |      - |      - |         - |
| CompareSequentialGuid                             | 1000000 |       108.9 ns |      0.09 ns |      0.08 ns |      - |      - |         - |
| CreateGuid                                        | 1000000 |       442.4 ns |      0.32 ns |      0.25 ns |      - |      - |         - |
| CreateSequentialGuid                              | 1000000 |       543.0 ns |      1.30 ns |      1.16 ns | 0.0229 | 0.0010 |      96 B |
| LinearSearch                                      | 1000000 | 3,388,066.9 ns | 14,938.78 ns | 13,242.84 ns |      - |      - |       4 B |
| BinarySearch                                      | 1000000 |       896.5 ns |      1.10 ns |      1.03 ns |      - |      - |         - |
| BinarySearchWithBitwiseRightShiftOperator         | 1000000 |       869.0 ns |      2.31 ns |      1.93 ns |      - |      - |         - |
| LinearSearchNotFound                              | 1000000 | 3,346,885.8 ns |  8,708.18 ns |  7,719.57 ns |      - |      - |       4 B |
| BinarySearchNotFound                              | 1000000 |     1,022.2 ns |      1.78 ns |      1.58 ns |      - |      - |         - |
| BinarySearchWithBitwiseRightShiftOperatorNotFound | 1000000 |       996.3 ns |      1.22 ns |      1.08 ns |      - |      - |         - |


Length: 10_000 - Item found in 9900 index
Comparison in LinearSearch: 9901
Comparison in BinarySearch: 23

Length: 100_000 - Item found in 99000 index
Comparison in LinearSearch: 99901
Comparison in BinarySearch: 29

Length: 1_000_000 - Item found in 990000 index
Comparison in LinearSearch: 999901
Comparison in BinarySearch: 31

