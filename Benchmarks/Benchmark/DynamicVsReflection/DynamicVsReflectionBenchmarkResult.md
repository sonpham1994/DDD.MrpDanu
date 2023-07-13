|                Method | Length |       Mean |      Error |     StdDev |     Median |    Gen0 |    Gen1 | Allocated |
|---------------------- |------- |-----------:|-----------:|-----------:|-----------:|--------:|--------:|----------:|
|       GetIdReflection |     10 |   7.589 us |  0.6648 us |  1.8860 us |   8.300 us |  0.8392 |       - |   2.58 KB |
|          GetIdDynamic |     10 |   6.832 us |  0.0583 us |  0.0517 us |   6.827 us |  0.8392 |       - |   2.58 KB |
| GetIdByCheckingIfElse |     10 |   6.573 us |  0.0295 us |  0.0230 us |   6.571 us |  0.7324 |       - |   2.27 KB |
|         GetIdManually |     10 |   6.055 us |  0.1208 us |  0.2549 us |   5.935 us |  0.6638 |       - |   2.05 KB |
|       GetIdReflection |    100 |  75.197 us |  0.6743 us |  0.6307 us |  75.266 us |  8.5449 |       - |   26.2 KB |
|          GetIdDynamic |    100 |  67.588 us |  1.3415 us |  2.9164 us |  66.614 us |  8.5449 |       - |   26.2 KB |
| GetIdByCheckingIfElse |    100 |  61.497 us |  0.5871 us |  0.4903 us |  61.349 us |  7.5073 |       - |  23.07 KB |
|         GetIdManually |    100 |  25.217 us |  0.4941 us |  1.1934 us |  25.049 us |  7.0801 |       - |  21.74 KB |
|       GetIdReflection |   1000 | 780.023 us | 31.2104 us | 89.0450 us | 786.494 us | 78.1250 | 20.9961 | 258.28 KB |
|          GetIdDynamic |   1000 | 682.311 us | 13.3086 us | 14.2400 us | 676.766 us | 78.1250 | 19.5313 | 258.23 KB |
| GetIdByCheckingIfElse |   1000 | 274.036 us |  4.3187 us |  3.8284 us | 273.766 us | 73.2422 |  0.9766 | 226.98 KB |
|         GetIdManually |   1000 | 265.197 us |  5.1111 us |  5.6810 us | 263.848 us | 71.2891 |       - | 218.62 KB |