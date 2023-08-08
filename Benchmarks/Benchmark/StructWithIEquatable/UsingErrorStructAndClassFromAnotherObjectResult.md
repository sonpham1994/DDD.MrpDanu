|                                                      Method |       Mean |     Error |    StdDev |     Median |   Gen0 | Allocated |
|------------------------------------------------------------ |-----------:|----------:|----------:|-----------:|-------:|----------:|
|                                         PropertyErrorStruct |  0.0677 ns | 0.0491 ns | 0.0820 ns |  0.0255 ns |      - |         - |
|                            PropertyWithAssigningErrorStruct |  0.3749 ns | 0.1210 ns | 0.3529 ns |  0.2672 ns |      - |         - |
|                    PropertyWithAssigningReadonlyErrorStruct |  0.2586 ns | 0.0999 ns | 0.2945 ns |  0.1742 ns |      - |         - |
|                                                    ConstInt |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |      - |         - |
|                                           StaticReadonlyInt |  0.2158 ns | 0.0824 ns | 0.2037 ns |  0.1702 ns |      - |         - |
|                                           MethodErrorStruct |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |      - |         - |
|                           PropertyErrorStructWithIEquatable |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |      - |         - |
|              PropertyErrorWithAssigningStructWithIEquatable |  0.1972 ns | 0.0839 ns | 0.1556 ns |  0.1851 ns |      - |         - |
|      PropertyErrorWithAssigningReadonlyStructWithIEquatable |  0.1158 ns | 0.0803 ns | 0.0956 ns |  0.0768 ns |      - |         - |
|                             MethodErrorStructWithIEquatable |  0.2351 ns | 0.1094 ns | 0.3193 ns |  0.0674 ns |      - |         - |
|                                          PropertyErrorClass | 15.7120 ns | 0.3919 ns | 0.9759 ns | 15.5415 ns | 0.0102 |      32 B |
|                                            MethodErrorClass | 14.9223 ns | 0.3809 ns | 0.5931 ns | 14.7064 ns | 0.0102 |      32 B |
|                                    PropertyErrorStructTwice |  0.5819 ns | 0.0878 ns | 0.2235 ns |  0.5739 ns |      - |         - |
|                       PropertyWithAssigningErrorStructTwice |  0.0133 ns | 0.0388 ns | 0.0324 ns |  0.0000 ns |      - |         - |
|               PropertyWithAssigningReadonlyErrorStructTwice |  0.4239 ns | 0.1457 ns | 0.4274 ns |  0.3057 ns |      - |         - |
|                                               ConstIntTwice |  0.1232 ns | 0.0729 ns | 0.1237 ns |  0.1156 ns |      - |         - |
|                                      StaticReadonlyIntTwice |  0.0510 ns | 0.0602 ns | 0.0824 ns |  0.0000 ns |      - |         - |
|                                      MethodErrorStructTwice |  0.0315 ns | 0.0473 ns | 0.0486 ns |  0.0000 ns |      - |         - |
|                      PropertyErrorStructWithIEquatableTwice |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |      - |         - |
|         PropertyErrorWithAssigningStructWithIEquatableTwice |  0.0785 ns | 0.0761 ns | 0.0711 ns |  0.0809 ns |      - |         - |
| PropertyErrorWithAssigningReadonlyStructWithIEquatableTwice |  0.0545 ns | 0.0666 ns | 0.0590 ns |  0.0344 ns |      - |         - |
|                        MethodErrorStructWithIEquatableTwice |  0.1558 ns | 0.0953 ns | 0.2811 ns |  0.0000 ns |      - |         - |
|                                     PropertyErrorClassTwice | 32.6231 ns | 0.7317 ns | 1.6062 ns | 31.9675 ns | 0.0204 |      64 B |
|                                       MethodErrorClassTwice | 31.9819 ns | 0.6581 ns | 1.0996 ns | 31.8982 ns | 0.0204 |      64 B |

## Although PropertyWithAssigningErrorStruct and PropertyWithAssigningReadonlyErrorStruct don't allocate memory. In fact,
they allocate memory, please check by using Rider and check at memory tab.