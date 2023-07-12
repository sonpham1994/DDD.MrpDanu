|                                Method | Length |       Mean |      Error |     StdDev |     Median |    Gen0 | Allocated |
|-------------------------------------- |------- |-----------:|-----------:|-----------:|-----------:|--------:|----------:|
|      GroupWithToStringGuidGetHashCode |     10 |  16.866 us |  1.3610 us |  3.8611 us |  18.582 us |  3.3417 |  10.27 KB |
|       GroupWithToStringIntGetHashCode |     10 |   8.917 us |  0.1781 us |  0.4267 us |   8.824 us |  2.0294 |   6.24 KB |
|          GroupWithNameGuidGetHashCode |     10 |  18.889 us |  0.3766 us |  0.8187 us |  18.591 us |  3.2959 |  10.12 KB |
|           GroupWithNameIntGetHashCode |     10 |   9.343 us |  0.2355 us |  0.6486 us |   9.147 us |  1.9836 |   6.09 KB |
| CacheGroupWithToStringGuidGetHashCode |     10 |  12.093 us |  0.0660 us |  0.0617 us |  12.096 us |  2.1973 |   6.76 KB |
|  CacheGroupWithToStringIntGetHashCode |     10 |   5.952 us |  0.0658 us |  0.0583 us |   5.951 us |  1.4191 |   4.37 KB |
|     CacheGroupWithNameGuidGetHashCode |     10 |   4.818 us |  0.0402 us |  0.0336 us |   4.827 us |  2.1667 |   6.68 KB |
|      CacheGroupWithNameIntGetHashCode |     10 |   2.414 us |  0.0421 us |  0.0351 us |   2.432 us |  1.4000 |   4.29 KB |
|      GroupWithToStringGuidGetHashCode |    100 | 215.954 us | 10.1805 us | 30.0175 us | 202.865 us | 30.7617 |  94.56 KB |
|       GroupWithToStringIntGetHashCode |    100 |  83.569 us |  1.6680 us |  3.9641 us |  82.068 us | 19.7754 |  60.66 KB |
|          GroupWithNameGuidGetHashCode |    100 | 192.846 us |  4.9998 us | 13.9375 us | 186.937 us | 30.2734 |  92.99 KB |
|           GroupWithNameIntGetHashCode |    100 |  88.810 us |  1.7199 us |  3.4348 us |  88.028 us | 19.2871 |   59.1 KB |
| CacheGroupWithToStringGuidGetHashCode |    100 | 115.370 us |  2.0003 us |  3.1142 us | 114.343 us | 19.2871 |  59.39 KB |
|  CacheGroupWithToStringIntGetHashCode |    100 |  35.207 us |  4.4231 us | 13.0415 us |  28.261 us | 12.7563 |   39.1 KB |
|     CacheGroupWithNameGuidGetHashCode |    100 | 112.463 us |  1.6167 us |  1.4332 us | 112.227 us | 19.0430 |  58.61 KB |
|      CacheGroupWithNameIntGetHashCode |    100 |  56.593 us |  1.0488 us |  2.2799 us |  56.282 us | 12.4512 |  38.32 KB |