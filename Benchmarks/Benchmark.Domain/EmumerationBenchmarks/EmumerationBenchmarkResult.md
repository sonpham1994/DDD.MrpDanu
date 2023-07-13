| Method                            |     Mean |     Error |    StdDev |   Median |   Gen0 | Allocated |
|-----------------------------------|---------:|----------:|----------:|---------:|-------:|----------:|
| EnumerationWithToList             | 1.748 us | 0.0462 us | 0.1347 us | 1.739 us | 0.1783 |     560 B |
| EnumerationWithToArray            | 1.630 us | 0.0325 us | 0.0790 us | 1.595 us | 0.1678 |     528 B |
| EnumerationWithToFixedLengthArray | 1.667 us | 0.0334 us | 0.0843 us | 1.638 us | 0.2060 |     648 B |
| EnumerationWithToArrayAndCopyTo   | 1.636 us | 0.0098 us | 0.0091 us | 1.633 us | 0.2060 |     648 B |
