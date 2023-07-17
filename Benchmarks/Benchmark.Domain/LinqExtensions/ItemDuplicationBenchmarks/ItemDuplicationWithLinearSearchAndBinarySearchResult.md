|                         Method | Length |           Mean |        Error |       StdDev |   Gen0 | Allocated |
|------------------------------- |------- |---------------:|-------------:|-------------:|-------:|----------:|
| LinearSearchWithoutDuplication |     10 |       546.9 ns |      0.83 ns |      0.64 ns |      - |         - |
| BinarySearchWithoutDuplication |     10 |       265.7 ns |      0.92 ns |      0.86 ns | 0.0210 |      88 B |
|    LinearSearchWithDuplication |     10 |       529.6 ns |      0.83 ns |      0.74 ns |      - |         - |
|    BinarySearchWithDuplication |     10 |       280.2 ns |      0.82 ns |      0.73 ns | 0.0210 |      88 B |
| LinearSearchWithoutDuplication |    100 |    52,017.4 ns |     94.14 ns |     83.45 ns |      - |         - |
| BinarySearchWithoutDuplication |    100 |    17,706.1 ns |     19.39 ns |     18.14 ns |      - |      88 B |
|    LinearSearchWithDuplication |    100 |    40,455.3 ns |     55.88 ns |     49.54 ns |      - |         - |
|    BinarySearchWithDuplication |    100 |    17,814.1 ns |     34.54 ns |     30.62 ns |      - |      88 B |
| LinearSearchWithoutDuplication |   1000 | 5,107,267.5 ns | 15,165.61 ns | 13,443.92 ns |      - |       7 B |
| BinarySearchWithoutDuplication |   1000 | 1,368,026.7 ns |  5,141.99 ns |  4,558.24 ns |      - |      90 B |
|    LinearSearchWithDuplication |   1000 | 3,843,926.5 ns |  5,976.06 ns |  5,297.62 ns |      - |       4 B |
|    BinarySearchWithDuplication |   1000 | 1,363,016.1 ns |  2,307.79 ns |  2,045.80 ns |      - |      90 B |
