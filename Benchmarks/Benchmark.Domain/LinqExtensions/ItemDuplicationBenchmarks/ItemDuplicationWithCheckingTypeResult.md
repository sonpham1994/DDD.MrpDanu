| Method                                             | Length |            Mean |        Error |       StdDev | Allocated |
|----------------------------------------------------|------- |----------------:|-------------:|-------------:|----------:|
| Enumerable                                         |     10 |      1,549.6 ns |      2.30 ns |      2.04 ns |         - |
| EnumerableWithCheckingList                         |     10 |        548.5 ns |      0.95 ns |      0.89 ns |         - |
| EnumerableWithCheckingIReadOnlyList                |     10 |        795.9 ns |      1.08 ns |      0.90 ns |         - |
| EnumerableWithDuplication                          |     10 |      1,451.0 ns |      2.51 ns |      2.35 ns |         - |
| EnumerableWithCheckingListWithDuplication          |     10 |        530.9 ns |      1.61 ns |      1.34 ns |         - |
| EnumerableWithCheckingIReadOnlyListWithDuplication |     10 |        678.1 ns |      0.58 ns |      0.55 ns |         - |
| Enumerable                                         |    100 |    138,103.5 ns |    225.70 ns |    188.47 ns |         - |
| EnumerableWithCheckingList                         |    100 |     52,111.9 ns |    145.22 ns |    135.84 ns |         - |
| EnumerableWithCheckingIReadOnlyList                |    100 |     68,431.1 ns |     54.21 ns |     50.71 ns |         - |
| EnumerableWithDuplication                          |    100 |    105,627.0 ns |    266.87 ns |    236.57 ns |         - |
| EnumerableWithCheckingListWithDuplication          |    100 |     40,482.2 ns |     42.46 ns |     37.64 ns |         - |
| EnumerableWithCheckingIReadOnlyListWithDuplication |    100 |     51,685.5 ns |     47.95 ns |     40.04 ns |         - |
| Enumerable                                         |   1000 | 13,640,850.6 ns | 22,204.68 ns | 19,683.87 ns |      15 B |
| EnumerableWithCheckingList                         |   1000 |  5,225,668.5 ns |  9,250.08 ns |  7,221.86 ns |       7 B |
| EnumerableWithCheckingIReadOnlyList                |   1000 |  5,977,067.3 ns | 10,893.94 ns | 10,190.20 ns |       7 B |
| EnumerableWithDuplication                          |   1000 | 10,252,977.7 ns | 24,607.00 ns | 21,813.46 ns |      15 B |
| EnumerableWithCheckingListWithDuplication          |   1000 |  4,422,572.0 ns | 10,090.77 ns |  9,438.91 ns |       2 B |
| EnumerableWithCheckingIReadOnlyListWithDuplication |   1000 |  5,044,329.6 ns | 23,889.51 ns | 21,177.43 ns |       7 B |
