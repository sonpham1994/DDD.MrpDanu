|                                 Method |       Mean |     Error |    StdDev |     Median |   Gen0 | Allocated |
|--------------------------------------- |-----------:|----------:|----------:|-----------:|-------:|----------:|
|                    PropertyErrorStruct |  0.0593 ns | 0.0496 ns | 0.1456 ns |  0.0000 ns |      - |         - |
|                      MethodErrorStruct |  0.0145 ns | 0.0279 ns | 0.0343 ns |  0.0000 ns |      - |         - |
|      PropertyErrorStructWithIEquatable |  2.3509 ns | 0.0839 ns | 0.0824 ns |  2.3433 ns |      - |         - |
|        MethodErrorStructWithIEquatable |  0.1554 ns | 0.0736 ns | 0.1553 ns |  0.1290 ns |      - |         - |
|                     PropertyErrorClass | 14.7844 ns | 1.3289 ns | 3.9184 ns | 15.9223 ns | 0.0102 |      32 B |
|                       MethodErrorClass | 14.2576 ns | 1.3649 ns | 3.9597 ns | 15.5109 ns | 0.0102 |      32 B |
|               PropertyErrorStructTwice |  0.0068 ns | 0.0165 ns | 0.0177 ns |  0.0000 ns |      - |         - |
|                 MethodErrorStructTwice |  0.0857 ns | 0.0518 ns | 0.1526 ns |  0.0000 ns |      - |         - |
| PropertyErrorStructWithIEquatableTwice |  1.3454 ns | 0.3118 ns | 0.9195 ns |  1.9509 ns |      - |         - |
|   MethodErrorStructWithIEquatableTwice |  0.0048 ns | 0.0103 ns | 0.0091 ns |  0.0000 ns |      - |         - |
|                PropertyErrorClassTwice | 35.8634 ns | 3.1345 ns | 9.0937 ns | 37.4391 ns | 0.0204 |      64 B |
|                  MethodErrorClassTwice | 38.1713 ns | 1.4661 ns | 4.2997 ns | 37.5737 ns | 0.0204 |      64 B |