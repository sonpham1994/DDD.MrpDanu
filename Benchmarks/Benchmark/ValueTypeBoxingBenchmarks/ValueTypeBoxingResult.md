| Method                       | Length |           Mean |       Error |      StdDev |    Gen0 |   Gen1 | Allocated |
|------------------------------|------- |---------------:|------------:|------------:|--------:|-------:|----------:|
| ValueTypeBoxing              |     10 |     867.880 ns |  17.3549 ns |  33.0196 ns |  0.1717 |      - |     720 B |
| ValueTypePreventingBoxing    |     10 |       4.646 ns |   0.0319 ns |   0.0282 ns |       - |      - |         - |
| IntWithString2_1             |     10 |     776.301 ns |  15.3513 ns |  26.4802 ns |  0.1526 |      - |     640 B |
| IntWithString2_2             |     10 |     307.682 ns |   1.5455 ns |   1.3700 ns |  0.1526 |      - |     640 B |
| IntWithString2_3             |     10 |     260.838 ns |   1.2532 ns |   1.1723 ns |  0.1526 |      - |     640 B |
| EnumWithStringBoxing         |     10 |   1,176.118 ns |   3.1606 ns |   2.6393 ns |  0.2098 |      - |     880 B |
| EnumWithStringBoxing2        |     10 |     628.110 ns |   2.4052 ns |   2.0085 ns |  0.2098 |      - |     880 B |
| EnumWithStringPreventBoxing2 |     10 |     236.615 ns |   3.2370 ns |   2.8695 ns |  0.1526 | 0.0005 |     640 B |
| ValueTypeBoxing              |    100 |   8,271.296 ns |  11.4378 ns |  10.1393 ns |  1.7090 | 0.0153 |    7200 B |
| ValueTypePreventingBoxing    |    100 |      50.858 ns |   0.0612 ns |   0.0511 ns |       - |      - |         - |
| IntWithString2_1             |    100 |   7,562.541 ns |  59.2909 ns |  52.5598 ns |  1.5259 | 0.0076 |    6400 B |
| IntWithString2_2             |    100 |   2,953.747 ns |   8.7491 ns |   7.7558 ns |  1.5297 | 0.0038 |    6400 B |
| IntWithString2_3             |    100 |   2,491.747 ns |   7.4806 ns |   6.6314 ns |  1.5297 | 0.0038 |    6400 B |
| EnumWithStringBoxing         |    100 |  12,341.576 ns |  26.1277 ns |  21.8178 ns |  2.0905 |      - |    8801 B |
| EnumWithStringBoxing2        |    100 |   6,284.034 ns |  16.9282 ns |  15.0064 ns |  2.0981 | 0.0076 |    8801 B |
| EnumWithStringPreventBoxing2 |    100 |   2,285.633 ns |   6.7211 ns |   5.6124 ns |  1.5297 | 0.0038 |    6400 B |
| ValueTypeBoxing              |   1000 |  84,670.731 ns | 363.6232 ns | 283.8931 ns | 17.2119 |      - |   72000 B |
| ValueTypePreventingBoxing    |   1000 |     402.062 ns |   0.3947 ns |   0.3499 ns |       - |      - |         - |
| IntWithString2_1             |   1000 |  68,461.110 ns |  87.8333 ns |  68.5745 ns | 15.2588 |      - |   64000 B |
| IntWithString2_2             |   1000 |  29,110.157 ns | 531.6130 ns | 443.9209 ns | 15.2893 |      - |   64000 B |
| IntWithString2_3             |   1000 |  24,414.999 ns |  73.5701 ns |  61.4343 ns | 15.2893 |      - |   64000 B |
| EnumWithStringBoxing         |   1000 | 120,250.705 ns | 451.4595 ns | 376.9891 ns | 20.9961 |      - |   88007 B |
| EnumWithStringBoxing2        |   1000 |  60,831.579 ns | 129.4861 ns | 101.0942 ns | 20.9961 |      - |   88007 B |
| EnumWithStringPreventBoxing2 |   1000 |  22,500.394 ns |  76.3801 ns |  67.7089 ns | 15.2893 |      - |   64000 B |
