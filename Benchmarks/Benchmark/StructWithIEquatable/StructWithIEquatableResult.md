|                                        Method | Length |            Mean |          Error |         StdDev |			Median |			 P95 |	  Gen0 | Allocated |
|---------------------------------------------- |------- |----------------:|---------------:|---------------:|----------------:|----------------:|--------:|----------:|
|                             CreateErrorStruct |     10 |       0.0226 ns |      0.0219 ns |      0.0646 ns |       0.0000 ns |       0.2178 ns |       - |         - |
|                     ErrorStructWithIEquatable |     10 |       0.0186 ns |      0.0210 ns |      0.0186 ns |       0.0207 ns |       0.0436 ns |       - |         - |
|                            CompareErrorStruct |     10 |       0.0000 ns |      0.0000 ns |      0.0000 ns |       0.0000 ns |       0.0000 ns |       - |         - |
|                         CompareWithIEquatable |     10 |       0.0284 ns |      0.0520 ns |      0.0461 ns |       0.0007 ns |       0.1133 ns |       - |         - |
|                              GroupErrorStruct |     10 |   3,203.1074 ns |     56.0556 ns |    111.9492 ns |   3,160.5238 ns |   3,432.3440 ns |  0.6561 |    2064 B |
|                GroupErrorStructWithIEquatable |     10 |   3,296.1310 ns |     32.9050 ns |     27.4772 ns |   3,301.8564 ns |   3,322.5779 ns |  0.6561 |    2064 B |
|               GroupErrorStructWithDuplication |     10 |   3,863.2688 ns |     74.1889 ns |    190.1744 ns |   3,780.1567 ns |   4,271.2279 ns |  0.8621 |    2712 B |
| GroupErrorStructWithIEquatableWithDuplication |     10 |   3,701.6700 ns |     36.5472 ns |     35.8943 ns |   3,690.4032 ns |   3,765.9195 ns |  0.8507 |    2680 B |
|                             CreateErrorStruct |    100 |       0.0200 ns |      0.0210 ns |      0.0281 ns |       0.0081 ns |       0.0782 ns |       - |         - |
|                     ErrorStructWithIEquatable |    100 |       1.3498 ns |      0.3561 ns |      1.0501 ns |       2.1106 ns |       2.3668 ns |       - |         - |
|                            CompareErrorStruct |    100 |       0.1250 ns |      0.0662 ns |      0.1823 ns |       0.0450 ns |       0.5383 ns |       - |         - |
|                         CompareWithIEquatable |    100 |       0.0029 ns |      0.0109 ns |      0.0091 ns |       0.0000 ns |       0.0160 ns |       - |         - |
|                              GroupErrorStruct |    100 |  18,891.4421 ns |    876.3415 ns |  2,323.9333 ns |  17,903.7064 ns |  22,525.7387 ns |  5.3711 |   16920 B |
|                GroupErrorStructWithIEquatable |    100 |  32,161.1547 ns |    868.1476 ns |  2,532.4273 ns |  31,949.1913 ns |  36,732.2894 ns |  5.3711 |   16920 B |
|               GroupErrorStructWithDuplication |    100 |  33,748.9963 ns |    971.5654 ns |  2,849.4331 ns |  32,642.6697 ns |  38,115.5328 ns |  6.9580 |   21888 B |
| GroupErrorStructWithIEquatableWithDuplication |    100 |  33,807.4257 ns |    766.1484 ns |  2,234.8909 ns |  32,937.2864 ns |  38,462.9407 ns |  6.9580 |   21856 B |
|                             CreateErrorStruct |   1000 |       0.0619 ns |      0.0621 ns |      0.1255 ns |       0.0000 ns |       0.3504 ns |       - |         - |
|                     ErrorStructWithIEquatable |   1000 |       0.1832 ns |      0.0829 ns |      0.1971 ns |       0.1376 ns |       0.5707 ns |       - |         - |
|                            CompareErrorStruct |   1000 |       0.1197 ns |      0.0639 ns |      0.1760 ns |       0.0000 ns |       0.4428 ns |       - |         - |
|                         CompareWithIEquatable |   1000 |       0.0612 ns |      0.0624 ns |      0.1274 ns |       0.0000 ns |       0.3839 ns |       - |         - |
|                              GroupErrorStruct |   1000 | 324,613.0461 ns | 10,384.7884 ns | 29,290.4800 ns | 309,395.9473 ns | 390,301.7090 ns | 48.8281 |  153648 B |
|                GroupErrorStructWithIEquatable |   1000 | 347,831.5075 ns | 10,113.5626 ns | 29,661.3285 ns | 345,891.2109 ns | 397,749.1504 ns | 48.8281 |  153648 B |
|               GroupErrorStructWithDuplication |   1000 | 323,626.8717 ns |  4,541.2601 ns |  5,904.9209 ns | 322,896.2158 ns | 333,543.1982 ns | 63.9648 |  201816 B |
| GroupErrorStructWithIEquatableWithDuplication |   1000 | 388,490.2134 ns | 14,157.0045 ns | 41,742.2471 ns | 387,539.8193 ns | 454,156.9971 ns | 63.9648 |  201784 B |