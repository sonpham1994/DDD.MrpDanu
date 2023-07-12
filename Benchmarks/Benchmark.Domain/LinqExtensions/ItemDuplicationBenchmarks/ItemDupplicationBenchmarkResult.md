|                                                    Method | Length |         Mean |       Error |      StdDev |       Median |     Gen0 |    Gen1 |  Allocated |
|---------------------------------------------------------- |------- |-------------:|------------:|------------:|-------------:|---------:|--------:|-----------:|
|               ItemDuplication_AtTheLastPosition_WithGroup |     10 |     7.120 us |   0.4645 us |   1.3549 us |     6.728 us |   2.2125 |       - |    6.81 KB |
|   ItemDuplication_AtTheLastPosition_WithForeachDictionary |     10 |    17.480 us |   0.3531 us |   1.0300 us |    17.233 us |   3.2349 |       - |    9.92 KB |
|      ItemDuplication_AtTheLastPosition_WithForeachHashSet |     10 |     4.850 us |   0.0693 us |   0.0541 us |     4.855 us |   2.0142 |       - |    6.18 KB |
|          ItemDuplication_AtTheLastPosition_WithForeachAny |     10 |     1.813 us |   0.0195 us |   0.0183 us |     1.811 us |   0.8144 |       - |     2.5 KB |
|              ItemDuplication_AtTheLastPosition_WithForAny |     10 |     3.653 us |   0.4084 us |   1.2040 us |     4.254 us |   0.8144 |       - |     2.5 KB |
|        ItemDuplication_AtTheLastPosition_WithForAlgorithm |     10 |     2.996 us |   0.3697 us |   1.0899 us |     3.577 us |   0.6332 |       - |    1.95 KB |

|                                                    Method | Length |         Mean |       Error |      StdDev |       Median |     Gen0 |    Gen1 |  Allocated |
|---------------------------------------------------------- |------- |-------------:|------------:|------------:|-------------:|---------:|--------:|-----------:|
|             ItemDuplication_AtTheMiddlePosition_WithGroup |     10 |    12.599 us |   0.1137 us |   0.0950 us |    12.601 us |   2.2125 |       - |    6.81 KB |
| ItemDuplication_AtTheMiddlePosition_WithForeachDictionary |     10 |     4.559 us |   0.1269 us |   0.3516 us |     4.484 us |   1.7853 |       - |     5.5 KB |
|    ItemDuplication_AtTheMiddlePosition_WithForeachHashSet |     10 |     6.525 us |   0.7328 us |   2.1607 us |     7.665 us |   1.2512 |       - |    3.84 KB |
|        ItemDuplication_AtTheMiddlePosition_WithForeachAny |     10 |     2.896 us |   0.3873 us |   1.1419 us |     2.313 us |   0.6599 |       - |    2.03 KB |
|            ItemDuplication_AtTheMiddlePosition_WithForAny |     10 |     3.785 us |   0.0455 us |   0.0403 us |     3.778 us |   0.6561 |       - |    2.03 KB |
|         ItemDuplication_AtTheMiddlePosition_WithAlgorithm |     10 |     3.177 us |   0.0598 us |   0.0530 us |     3.157 us |   0.4807 |       - |    1.48 KB |


|                                                    Method | Length |         Mean |       Error |      StdDev |       Median |     Gen0 |    Gen1 |  Allocated |
|---------------------------------------------------------- |------- |-------------:|------------:|------------:|-------------:|---------:|--------:|-----------:|
|               ItemDuplication_AtTheLastPosition_WithGroup |    100 |   117.363 us |   0.8559 us |   0.8007 us |   117.424 us |  20.5078 |       - |   63.16 KB |
|   ItemDuplication_AtTheLastPosition_WithForeachDictionary |    100 |   169.036 us |   3.1606 us |   2.6393 us |   168.008 us |  32.7148 |       - |   100.9 KB |
|      ItemDuplication_AtTheLastPosition_WithForeachHashSet |    100 |    49.587 us |   1.5885 us |   4.6336 us |    47.843 us |  19.4092 |       - |   59.47 KB |
|          ItemDuplication_AtTheLastPosition_WithForeachAny |    100 |    38.781 us |   1.3017 us |   3.6286 us |    38.849 us |   7.9651 |       - |   24.42 KB |
|              ItemDuplication_AtTheLastPosition_WithForAny |    100 |    38.969 us |   0.7343 us |   0.8456 us |    38.770 us |   7.9346 |       - |   24.42 KB |
|        ItemDuplication_AtTheLastPosition_WithForAlgorithm |    100 |    15.573 us |   0.3109 us |   0.8080 us |    15.367 us |   7.1716 |       - |   22.05 KB |


|                                                    Method | Length |         Mean |       Error |      StdDev |       Median |     Gen0 |    Gen1 |  Allocated |
|---------------------------------------------------------- |------- |-------------:|------------:|------------:|-------------:|---------:|--------:|-----------:|
|             ItemDuplication_AtTheMiddlePosition_WithGroup |    100 |   116.878 us |   2.3266 us |   2.2851 us |   116.701 us |  20.5688 |       - |   63.16 KB |
| ItemDuplication_AtTheMiddlePosition_WithForeachDictionary |    100 |    96.913 us |   0.8805 us |   0.7806 us |    96.943 us |  18.3105 |       - |   56.39 KB |
|    ItemDuplication_AtTheMiddlePosition_WithForeachHashSet |    100 |    67.205 us |   1.1250 us |   0.9973 us |    67.370 us |  11.7188 |       - |   36.04 KB |
|        ItemDuplication_AtTheMiddlePosition_WithForeachAny |    100 |    22.101 us |   3.2865 us |   9.6902 us |    15.978 us |   6.4087 |       - |   19.73 KB |
|            ItemDuplication_AtTheMiddlePosition_WithForAny |    100 |    26.568 us |   3.0491 us |   8.9904 us |    32.740 us |   6.4392 |       - |   19.73 KB |
|         ItemDuplication_AtTheMiddlePosition_WithAlgorithm |    100 |    30.573 us |   0.4842 us |   0.4292 us |    30.451 us |   5.6152 |       - |   17.36 KB |

|                                                    Method | Length |         Mean |       Error |      StdDev |       Median |     Gen0 |    Gen1 |  Allocated |
|---------------------------------------------------------- |------- |-------------:|------------:|------------:|-------------:|---------:|--------:|-----------:|
|               ItemDuplication_AtTheLastPosition_WithGroup |   1000 | 1,348.628 us |  14.1102 us |  11.7827 us | 1,345.173 us | 199.2188 | 66.4063 |  618.69 KB |
|   ItemDuplication_AtTheLastPosition_WithForeachDictionary |   1000 | 1,871.898 us |  37.2086 us |  88.4302 us | 1,836.520 us | 320.3125 | 19.5313 | 1006.51 KB |
|      ItemDuplication_AtTheLastPosition_WithForeachHashSet |   1000 |   730.126 us | 101.2087 us | 298.4162 us |   550.944 us | 191.4063 |       - |  587.84 KB |
|          ItemDuplication_AtTheLastPosition_WithForeachAny |   1000 |   393.926 us |  36.6611 us | 104.5963 us |   434.273 us |  76.6602 |       - |  235.38 KB |
|              ItemDuplication_AtTheLastPosition_WithForAny |   1000 |   327.560 us |  38.7724 us | 114.3214 us |   398.450 us |  76.6602 |       - |  235.38 KB |
|        ItemDuplication_AtTheLastPosition_WithForAlgorithm |   1000 |   162.579 us |   3.1950 us |   3.5512 us |   161.665 us |  71.2891 |       - |  218.93 KB |

|                                                    Method | Length |         Mean |       Error |      StdDev |       Median |     Gen0 |    Gen1 |  Allocated |
|---------------------------------------------------------- |------- |-------------:|------------:|------------:|-------------:|---------:|--------:|-----------:|
|             ItemDuplication_AtTheMiddlePosition_WithGroup |   1000 |   878.241 us | 121.9784 us | 359.6561 us |   629.837 us | 199.2188 | 53.7109 |  618.69 KB |
| ItemDuplication_AtTheMiddlePosition_WithForeachDictionary |   1000 | 1,018.052 us |  11.2679 us |  10.5400 us | 1,015.640 us | 183.5938 |       - |  562.98 KB |
|    ItemDuplication_AtTheMiddlePosition_WithForeachHashSet |   1000 |   684.932 us |   4.7965 us |   4.0053 us |   684.775 us | 115.2344 |  0.9766 |  354.73 KB |
|        ItemDuplication_AtTheMiddlePosition_WithForeachAny |   1000 |   159.732 us |   4.4941 us |  12.7490 us |   154.836 us |  61.5234 | 15.1367 |   188.5 KB |
|            ItemDuplication_AtTheMiddlePosition_WithForAny |   1000 |   345.470 us |   4.7715 us |   3.9844 us |   344.444 us |  61.5234 | 15.3809 |   188.5 KB |
|         ItemDuplication_AtTheMiddlePosition_WithAlgorithm |   1000 |   352.688 us |  11.7648 us |  34.1319 us |   340.700 us |  56.1523 |       - |  172.05 KB |