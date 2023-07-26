| Method                                          | Length |            Mean |         Error |        StdDev |          Median |    Gen0 |   Gen1 | Allocated |
|-------------------------------------------------|------- |----------------:|--------------:|--------------:|----------------:|--------:|-------:|----------:|
| CreateErrorStruct                               |     10 |       0.0275 ns |     0.0359 ns |     0.0318 ns |       0.0083 ns |       - |      - |         - |
| CreateErrorStructWithIEquatable                 |     10 |       0.0242 ns |     0.0012 ns |     0.0011 ns |       0.0245 ns |       - |      - |         - |
| CreateErrorClass                                |     10 |      11.1422 ns |     0.1126 ns |     0.1053 ns |      11.0991 ns |  0.0076 |      - |      32 B |
| CompareErrorStruct                              |     10 |       0.0161 ns |     0.0348 ns |     0.0308 ns |       0.0000 ns |       - |      - |         - |
| CompareWithIEquatable                           |     10 |       0.0369 ns |     0.0209 ns |     0.0185 ns |       0.0244 ns |       - |      - |         - |
| CompareErrorClass                               |     10 |      23.6350 ns |     0.1850 ns |     0.1444 ns |      23.5690 ns |  0.0153 |      - |      64 B |
| GroupErrorStruct                                |     10 |   1,969.5938 ns |     6.0618 ns |     5.6702 ns |   1,969.3837 ns |  0.4921 |      - |    2064 B |
| GroupErrorStructWithIEquatable                  |     10 |   1,846.9293 ns |    31.6474 ns |    37.6739 ns |   1,829.0975 ns |  0.4921 |      - |    2064 B |
| GroupErrorClass                                 |     10 |   2,071.5746 ns |    40.5449 ns |   103.9320 ns |   2,035.8073 ns |  0.3967 |      - |    1664 B |
| GroupErrorStructWithDuplication                 |     10 |   2,430.4991 ns |     7.2081 ns |     6.3898 ns |   2,429.1494 ns |  0.6485 |      - |    2712 B |
| GroupErrorStructWithIEquatableWithDuplication   |     10 |   2,403.8329 ns |    30.0143 ns |    26.6069 ns |   2,396.8062 ns |  0.6371 |      - |    2680 B |
| GroupErrorClassWithDuplication                  |     10 |   2,321.1321 ns |    10.5683 ns |     8.8250 ns |   2,318.2932 ns |  0.4883 |      - |    2056 B |
| HashSetErrorStruct                              |     10 |     293.4257 ns |     2.3566 ns |     2.2043 ns |     293.4298 ns |  0.1411 |      - |     592 B |
| HashSetErrorStructWithIEquatable                |     10 |     295.3824 ns |     3.0690 ns |     2.5627 ns |     294.9265 ns |  0.1411 |      - |     592 B |
| HashSetErrorClass                               |     10 |     327.9587 ns |     2.5799 ns |     2.4133 ns |     326.5259 ns |  0.1163 |      - |     488 B |
| HashSetErrorStructWithDuplication               |     10 |     476.6860 ns |     1.4693 ns |     1.3025 ns |     476.3197 ns |  0.1678 |      - |     704 B |
| HashSetErrorStructWithIEquatableWithDuplication |     10 |     459.1250 ns |     4.1313 ns |     3.6623 ns |     458.1719 ns |  0.1602 |      - |     672 B |
| HashSetErrorClassWithDuplication                |     10 |     322.9308 ns |     0.7935 ns |     0.7034 ns |     323.0616 ns |  0.1163 |      - |     488 B |
| CreateListWithErrorStruct                       |     10 |   1,483.1206 ns |    10.8001 ns |     9.5740 ns |   1,479.9524 ns |  0.2422 |      - |    1016 B |
| CreateListWithErrorStructIEquatable             |     10 |   1,507.7681 ns |     5.3316 ns |     4.4521 ns |   1,506.2523 ns |  0.2422 |      - |    1016 B |
| CreateListWithErrorClass                        |     10 |   1,558.8637 ns |     8.4233 ns |     7.4670 ns |   1,555.4679 ns |  0.2995 |      - |    1256 B |
| CreateErrorStruct                               |    100 |       0.0233 ns |     0.0028 ns |     0.0026 ns |       0.0227 ns |       - |      - |         - |
| CreateErrorStructWithIEquatable                 |    100 |       0.0236 ns |     0.0132 ns |     0.0117 ns |       0.0242 ns |       - |      - |         - |
| CreateErrorClass                                |    100 |      11.1320 ns |     0.0614 ns |     0.0574 ns |      11.1363 ns |  0.0076 |      - |      32 B |
| CompareErrorStruct                              |    100 |       0.0000 ns |     0.0000 ns |     0.0000 ns |       0.0000 ns |       - |      - |         - |
| CompareWithIEquatable                           |    100 |       0.0245 ns |     0.0015 ns |     0.0014 ns |       0.0244 ns |       - |      - |         - |
| CompareErrorClass                               |    100 |      23.6802 ns |     0.0710 ns |     0.0629 ns |      23.6650 ns |  0.0153 |      - |      64 B |
| GroupErrorStruct                                |    100 |  16,951.8047 ns |    87.8252 ns |    77.8548 ns |  16,944.0091 ns |  4.0283 |      - |   16920 B |
| GroupErrorStructWithIEquatable                  |    100 |  17,205.5498 ns |   171.9666 ns |   152.4439 ns |  17,159.2312 ns |  4.0283 |      - |   16920 B |
| GroupErrorClass                                 |    100 |  18,532.9230 ns |   353.2525 ns |   446.7514 ns |  18,612.6037 ns |  3.1738 |      - |   13288 B |
| GroupErrorStructWithDuplication                 |    100 |  19,854.9745 ns |   239.6302 ns |   212.4259 ns |  19,785.6510 ns |  5.2185 |      - |   21888 B |
| GroupErrorStructWithIEquatableWithDuplication   |    100 |  19,888.2065 ns |   138.8523 ns |   115.9480 ns |  19,851.9867 ns |  5.2185 |      - |   21856 B |
| GroupErrorClassWithDuplication                  |    100 |  18,866.8853 ns |   104.9309 ns |    87.6220 ns |  18,872.4995 ns |  3.7842 |      - |   15840 B |
| HashSetErrorStruct                              |    100 |     956.7158 ns |    18.7751 ns |    21.6215 ns |     964.4405 ns |  1.3447 |      - |    5632 B |
| HashSetErrorStructWithIEquatable                |    100 |     965.7148 ns |    16.8478 ns |    20.6906 ns |     971.3356 ns |  1.3447 |      - |    5632 B |
| HashSetErrorClass                               |    100 |     810.5412 ns |    16.1613 ns |    27.8775 ns |     818.7942 ns |  0.9775 |      - |    4088 B |
| HashSetErrorStructWithDuplication               |    100 |   1,212.5420 ns |     8.3587 ns |     7.8187 ns |   1,210.8150 ns |  1.3733 |      - |    5752 B |
| HashSetErrorStructWithIEquatableWithDuplication |    100 |   1,155.7445 ns |     6.7957 ns |     5.6747 ns |   1,157.3740 ns |  1.3657 |      - |    5720 B |
| HashSetErrorClassWithDuplication                |    100 |     842.4415 ns |    20.5432 ns |    58.6110 ns |     830.5799 ns |  0.9775 |      - |    4088 B |
| CreateListWithErrorStruct                       |    100 |  14,895.1818 ns |   287.6771 ns |   488.4975 ns |  15,087.3554 ns |  2.4719 |      - |   10376 B |
| CreateListWithErrorStructIEquatable             |    100 |  15,258.3024 ns |    62.8372 ns |    55.7035 ns |  15,259.4765 ns |  2.4719 |      - |   10376 B |
| CreateListWithErrorClass                        |    100 |  16,018.4637 ns |   178.6943 ns |   158.4078 ns |  15,953.7142 ns |  3.0518 |      - |   12776 B |
| CreateErrorStruct                               |   1000 |       0.0581 ns |     0.0434 ns |     0.0608 ns |       0.0531 ns |       - |      - |         - |
| CreateErrorStructWithIEquatable                 |   1000 |       0.0244 ns |     0.0186 ns |     0.0145 ns |       0.0250 ns |       - |      - |         - |
| CreateErrorClass                                |   1000 |      11.1611 ns |     0.0409 ns |     0.0342 ns |      11.1532 ns |  0.0076 |      - |      32 B |
| CompareErrorStruct                              |   1000 |       0.0000 ns |     0.0000 ns |     0.0000 ns |       0.0000 ns |       - |      - |         - |
| CompareWithIEquatable                           |   1000 |       0.0363 ns |     0.0149 ns |     0.0139 ns |       0.0366 ns |       - |      - |         - |
| CompareErrorClass                               |   1000 |      23.6403 ns |     0.2596 ns |     0.2301 ns |      23.5723 ns |  0.0153 |      - |      64 B |
| GroupErrorStruct                                |   1000 | 181,774.0672 ns | 1,980.4038 ns | 1,755.5764 ns | 181,161.3334 ns | 36.6211 | 0.9766 |  153648 B |
| GroupErrorStructWithIEquatable                  |   1000 | 184,082.4603 ns | 1,685.3832 ns | 1,407.3713 ns | 183,917.8098 ns | 36.6211 | 0.9766 |  153648 B |
| GroupErrorClass                                 |   1000 | 177,849.0527 ns | 1,037.5386 ns |   919.7510 ns | 177,614.6056 ns | 28.8086 |      - |  121280 B |
| GroupErrorStructWithDuplication                 |   1000 | 186,458.1524 ns | 1,522.9509 ns | 1,350.0563 ns | 186,415.4476 ns | 48.0957 | 0.2441 |  201816 B |
| GroupErrorStructWithIEquatableWithDuplication   |   1000 | 186,304.4867 ns |   844.9311 ns |   790.3491 ns | 186,381.9941 ns | 48.0957 | 0.2441 |  201784 B |
| GroupErrorClassWithDuplication                  |   1000 | 183,108.7294 ns | 1,726.3448 ns | 1,614.8240 ns | 182,976.9460 ns | 34.6680 | 7.5684 |  145432 B |
| HashSetErrorStruct                              |   1000 |   6,008.3516 ns |    61.9789 ns |    51.7552 ns |   5,992.4962 ns | 12.8174 |      - |   54184 B |
| HashSetErrorStructWithIEquatable                |   1000 |   6,047.5930 ns |    68.9285 ns |    57.5584 ns |   6,037.0641 ns | 12.8174 | 1.8311 |   54184 B |
| HashSetErrorClass                               |   1000 |   4,855.3983 ns |    93.0627 ns |   241.8824 ns |   4,753.0900 ns |  9.2163 | 1.1520 |   38768 B |
| HashSetErrorStructWithDuplication               |   1000 |   6,958.7283 ns |    52.5244 ns |    49.1314 ns |   6,951.5737 ns | 12.8174 | 1.8311 |   54304 B |
| HashSetErrorStructWithIEquatableWithDuplication |   1000 |   7,114.6937 ns |   137.9217 ns |   153.2996 ns |   7,082.6383 ns | 12.8174 | 1.8311 |   54272 B |
| HashSetErrorClassWithDuplication                |   1000 |   4,797.2851 ns |    21.0613 ns |    17.5872 ns |   4,793.0163 ns |  9.2163 | 1.1520 |   38768 B |
| CreateListWithErrorStruct                       |   1000 | 144,928.8508 ns | 1,970.0683 ns | 1,645.0963 ns | 144,932.6853 ns | 24.6582 | 6.1035 |  103976 B |
| CreateListWithErrorStructIEquatable             |   1000 | 141,919.3134 ns |   432.6676 ns |   383.5485 ns | 141,841.6671 ns | 24.6582 | 6.1035 |  103976 B |
| CreateListWithErrorClass                        |   1000 | 158,983.8681 ns | 2,198.5413 ns | 1,948.9496 ns | 158,328.3999 ns | 30.5176 | 0.2441 |  127976 B |