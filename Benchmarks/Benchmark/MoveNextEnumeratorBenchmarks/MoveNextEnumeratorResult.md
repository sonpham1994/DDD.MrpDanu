## 1000 records
|                           Method | Length |     Mean |    Error |   StdDev |   Median |    Gen0 | Allocated |
|--------------------------------- |------- |---------:|---------:|---------:|---------:|--------:|----------:|
|                          Foreach |   1000 | 26.69 us | 0.074 us | 0.069 us | 26.68 us | 12.3901 |  50.62 KB |
|                ForeachWithToList |   1000 | 70.15 us | 1.366 us | 3.477 us | 68.28 us | 28.6865 | 117.51 KB |
|           MoveNextWithEnumerator |   1000 | 26.95 us | 0.080 us | 0.071 us | 26.94 us | 12.3901 |  50.62 KB |
| MoveNextWithEnumeratorAndDispose |   1000 | 26.61 us | 0.124 us | 0.110 us | 26.58 us | 12.3901 |  50.62 KB |
