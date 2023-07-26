|                                        Method | Length |        Mean |       Error |      StdDev |      Median |    Gen0 | Allocated |
|---------------------------------------------- |------- |------------:|------------:|------------:|------------:|--------:|----------:|
| ConvertErrorStructToErrorStructWithIEquatable |     10 |    417.5 ns |     8.00 ns |     9.21 ns |    418.1 ns |  0.0968 |     304 B |
|                ConvertErrorStructToErrorClass |     10 |    535.3 ns |    28.63 ns |    84.42 ns |    505.8 ns |  0.1707 |     536 B |
|  ConvertErrorStructWithIEquatableToErrorClass |     10 |    498.1 ns |    12.66 ns |    36.53 ns |    491.1 ns |  0.1707 |     536 B |
| ConvertErrorClassToErrorStruct				|     10 |    643.2 ns |    12.14 ns |    11.36 ns |		   - |  0.0935 |	 296 B |
|                 ConvertErrorClassToErrorClass |     10 |    529.6 ns |    26.89 ns |    79.27 ns |    512.2 ns |  0.1678 |     528 B |
| ConvertErrorStructToErrorStructWithIEquatable |    100 |  2,604.6 ns |    51.56 ns |   121.53 ns |  2,583.9 ns |  0.5531 |    1744 B |
|                ConvertErrorStructToErrorClass |    100 |  3,720.8 ns |   167.16 ns |   492.87 ns |  3,657.6 ns |  1.3161 |    4136 B |
|  ConvertErrorStructWithIEquatableToErrorClass |    100 |  4,142.6 ns |   328.53 ns |   968.68 ns |  3,890.0 ns |  1.3123 |    4136 B |
| ConvertErrorClassToErrorStruct				|    100 |  3,289.4 ns |    96.74 ns |   280.65 ns |		   - |	0.5493 |    1736 B |
|                 ConvertErrorClassToErrorClass |    100 |  3,959.8 ns |   152.51 ns |   449.69 ns |  3,928.0 ns |  1.3123 |    4128 B |
| ConvertErrorStructToErrorStructWithIEquatable |   1000 | 26,699.4 ns | 1,052.00 ns | 3,068.73 ns | 26,035.9 ns |  5.1270 |   16144 B |
|                ConvertErrorStructToErrorClass |   1000 | 37,684.8 ns | 2,035.36 ns | 6,001.30 ns | 37,169.5 ns | 12.7869 |   40136 B |
|  ConvertErrorStructWithIEquatableToErrorClass |   1000 | 31,862.3 ns |   626.99 ns | 1,162.16 ns | 31,832.9 ns | 12.7563 |   40136 B |
| ConvertErrorClassToErrorStruct				|   1000 | 36,810.7 ns | 1,511.38 ns | 4,456.33 ns |		   - |	5.1270 |   16136 B |
|                 ConvertErrorClassToErrorClass |   1000 | 32,016.0 ns |   630.63 ns |   674.77 ns | 31,901.0 ns | 12.7563 |   40128 B |