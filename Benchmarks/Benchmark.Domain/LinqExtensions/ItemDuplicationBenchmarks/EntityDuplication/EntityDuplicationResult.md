.Net 8

| Method                                                    | Length | Mean            | Error         | StdDev        | Gen0    | Allocated |
|---------------------------------------------------------- |------- |----------------:|--------------:|--------------:|--------:|----------:|
| ItemDuplication_AtTheLastPosition_WithGroup               | 10     |     1,932.52 ns |     30.904 ns |     28.908 ns |  0.4311 |    1808 B |
| ItemDuplication_AtTheLastPosition_WithForeachDictionary   | 10     |       880.32 ns |      6.322 ns |      5.914 ns |  0.2460 |    1032 B |
| ItemDuplication_AtTheLastPosition_WithForeachHashSet      | 10     |       636.31 ns |      4.904 ns |      4.587 ns |  0.1907 |     800 B |
| ItemDuplication_AtTheLastPosition_WithForeachAny          | 10     |     2,337.87 ns |     46.083 ns |     68.975 ns |  0.4768 |    2008 B |
| ItemDuplication_AtTheLastPosition_WithForAny              | 10     |     2,454.81 ns |     19.382 ns |     18.130 ns |  0.4768 |    2008 B |
| ItemDuplication_AtTheLastPosition_WithEntityDup           | 10     |       562.49 ns |      1.692 ns |      1.500 ns |  0.0210 |      88 B |
| ItemDuplication_AtTheMiddlePosition_WithGroup             | 10     |     1,408.14 ns |      6.162 ns |      5.463 ns |  0.3433 |    1440 B |
| ItemDuplication_AtTheMiddlePosition_WithForeachDictionary | 10     |       487.44 ns |      9.722 ns |     18.260 ns |  0.1202 |     504 B |
| ItemDuplication_AtTheMiddlePosition_WithForeachHashSet    | 10     |       383.54 ns |      2.702 ns |      2.257 ns |  0.0973 |     408 B |
| ItemDuplication_AtTheMiddlePosition_WithForeachAny        | 10     |       499.84 ns |      7.987 ns |     11.954 ns |  0.1354 |     568 B |
| ItemDuplication_AtTheMiddlePosition_WithForAny            | 10     |       514.47 ns |      4.113 ns |      3.848 ns |  0.1354 |     568 B |
| ItemDuplication_AtTheMiddlePosition_WithEntityDup         | 10     |        79.59 ns |      0.544 ns |      0.482 ns |  0.0210 |      88 B |
| ItemDuplication_AtTheLastPosition_WithGroup               | 100    |    16,914.83 ns |    322.988 ns |    252.167 ns |  3.4485 |   14448 B |
| ItemDuplication_AtTheLastPosition_WithForeachDictionary   | 100    |     7,506.13 ns |     28.834 ns |     22.512 ns |  2.4414 |   10232 B |
| ItemDuplication_AtTheLastPosition_WithForeachHashSet      | 100    |     5,598.38 ns |     19.150 ns |     16.976 ns |  1.7700 |    7416 B |
| ItemDuplication_AtTheLastPosition_WithForeachAny          | 100    |    83,176.79 ns |    232.213 ns |    205.851 ns |  3.9063 |   16832 B |
| ItemDuplication_AtTheLastPosition_WithForAny              | 100    |    88,508.20 ns |    232.949 ns |    206.504 ns |  3.9063 |   16832 B |
| ItemDuplication_AtTheLastPosition_WithEntityDup           | 100    |    43,644.31 ns |     96.322 ns |     85.387 ns |       - |      88 B |
| ItemDuplication_AtTheMiddlePosition_WithGroup             | 100    |    11,947.68 ns |    213.535 ns |    219.284 ns |  2.6703 |   11200 B |
| ItemDuplication_AtTheMiddlePosition_WithForeachDictionary | 100    |     3,636.05 ns |     15.448 ns |     12.900 ns |  1.1139 |    4664 B |
| ItemDuplication_AtTheMiddlePosition_WithForeachHashSet    | 100    |     2,792.76 ns |     18.550 ns |     17.352 ns |  0.8163 |    3424 B |
| ItemDuplication_AtTheMiddlePosition_WithForeachAny        | 100    |     2,248.39 ns |     10.125 ns |      9.471 ns |  0.5798 |    2432 B |
| ItemDuplication_AtTheMiddlePosition_WithForAny            | 100    |     2,348.19 ns |     18.608 ns |     16.496 ns |  0.5798 |    2432 B |
| ItemDuplication_AtTheMiddlePosition_WithEntityDup         | 100    |       472.55 ns |      1.100 ns |      1.029 ns |  0.0210 |      88 B |
| ItemDuplication_AtTheLastPosition_WithGroup               | 1000   |   171,736.63 ns |    822.533 ns |    686.853 ns | 32.4707 |  136832 B |
| ItemDuplication_AtTheLastPosition_WithForeachDictionary   | 1000   |    79,580.86 ns |    645.423 ns |    538.958 ns | 24.2920 |  102256 B |
| ItemDuplication_AtTheLastPosition_WithForeachHashSet      | 1000   |    58,429.81 ns |    270.300 ns |    252.839 ns | 17.4561 |   73192 B |
| ItemDuplication_AtTheLastPosition_WithForeachAny          | 1000   | 6,625,500.40 ns |  7,620.952 ns |  6,363.840 ns | 31.2500 |  160846 B |
| ItemDuplication_AtTheLastPosition_WithForAny              | 1000   | 7,394,332.38 ns | 12,271.993 ns | 10,878.802 ns | 31.2500 |  160846 B |
| ItemDuplication_AtTheLastPosition_WithEntityDup           | 1000   | 4,217,280.57 ns | 19,893.776 ns | 18,608.651 ns |       - |      94 B |
| ItemDuplication_AtTheMiddlePosition_WithGroup             | 1000   |   126,182.77 ns |  2,069.729 ns |  1,834.760 ns | 24.9023 |  104784 B |
| ItemDuplication_AtTheMiddlePosition_WithForeachDictionary | 1000   |    35,038.95 ns |    222.505 ns |    185.802 ns | 11.4746 |   48136 B |
| ItemDuplication_AtTheMiddlePosition_WithForeachHashSet    | 1000   |    28,369.46 ns |    143.508 ns |    127.216 ns |  8.2397 |   34520 B |
| ItemDuplication_AtTheMiddlePosition_WithForeachAny        | 1000   |    18,222.58 ns |     94.656 ns |     79.042 ns |  3.9978 |   16840 B |
| ItemDuplication_AtTheMiddlePosition_WithForAny            | 1000   |    18,621.79 ns |    142.743 ns |    126.538 ns |  3.9978 |   16840 B |
| ItemDuplication_AtTheMiddlePosition_WithEntityDup         | 1000   |     4,182.43 ns |      7.985 ns |      6.234 ns |  0.0153 |      88 B |
