|                                              Method |       Mean |     Error |    StdDev |     Median |   Gen0 | Allocated |
|---------------------------------------------------- |-----------:|----------:|----------:|-----------:|-------:|----------:|
|                                 PropertyErrorStruct |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |      - |         - |
|                    PropertyWithAssigningErrorStruct |  0.0217 ns | 0.0038 ns | 0.0031 ns |  0.0207 ns |      - |         - |
|                                   MethodErrorStruct |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |      - |         - |
|                   PropertyErrorStructWithIEquatable |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |      - |         - |
|      PropertyErrorWithAssigningStructWithIEquatable |  0.0240 ns | 0.0043 ns | 0.0033 ns |  0.0229 ns |      - |         - |
|                     MethodErrorStructWithIEquatable |  0.0243 ns | 0.0290 ns | 0.0387 ns |  0.0000 ns |      - |         - |
|                                  PropertyErrorClass | 12.6825 ns | 0.4085 ns | 1.1388 ns | 12.4379 ns | 0.0076 |      32 B |
|                                    MethodErrorClass | 11.4564 ns | 0.1556 ns | 0.1299 ns | 11.3922 ns | 0.0076 |      32 B |
|                            PropertyErrorStructTwice |  0.0214 ns | 0.0032 ns | 0.0028 ns |  0.0202 ns |      - |         - |
|               PropertyWithAssigningErrorStructTwice |  0.0566 ns | 0.0310 ns | 0.0305 ns |  0.0568 ns |      - |         - |
|                              MethodErrorStructTwice |  0.0086 ns | 0.0281 ns | 0.0234 ns |  0.0000 ns |      - |         - |
|              PropertyErrorStructWithIEquatableTwice |  0.0459 ns | 0.0349 ns | 0.0309 ns |  0.0339 ns |      - |         - |
| PropertyErrorWithAssigningStructWithIEquatableTwice |  0.0281 ns | 0.0375 ns | 0.0351 ns |  0.0219 ns |      - |         - |
|                MethodErrorStructWithIEquatableTwice |  0.0730 ns | 0.0338 ns | 0.0316 ns |  0.0758 ns |      - |         - |
|                             PropertyErrorClassTwice | 24.2037 ns | 0.5323 ns | 1.5101 ns | 23.6582 ns | 0.0153 |      64 B |
|                               MethodErrorClassTwice | 23.1726 ns | 0.0786 ns | 0.0697 ns | 23.1821 ns | 0.0153 |      64 B |
