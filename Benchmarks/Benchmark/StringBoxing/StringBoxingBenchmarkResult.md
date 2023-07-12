|                    Method |       Mean |     Error |    StdDev |   Gen0 | Allocated |
|-------------------------- |-----------:|----------:|----------:|-------:|----------:|
|           ValueTypeBoxing | 83.4617 ns | 1.7191 ns | 1.6080 ns | 0.0172 |      72 B |
| ValueTypePreventingBoxing |  0.0000 ns | 0.0000 ns | 0.0000 ns |      - |         - |


|                    Method | Length |          Mean |       Error |      StdDev |    Gen0 | Allocated |
|-------------------------- |------- |--------------:|------------:|------------:|--------:|----------:|
|           ValueTypeBoxing |     10 |    793.641 ns |   3.1507 ns |   2.9472 ns |  0.1717 |     720 B |
| ValueTypePreventingBoxing |     10 |      4.587 ns |   0.0152 ns |   0.0127 ns |       - |         - |
|           ValueTypeBoxing |    100 |  8,318.982 ns |  49.0703 ns |  43.4995 ns |  1.7090 |    7200 B |
| ValueTypePreventingBoxing |    100 |     50.514 ns |   0.1286 ns |   0.1140 ns |       - |         - |
|           ValueTypeBoxing |   1000 | 80,989.795 ns | 418.0072 ns | 391.0042 ns | 17.2119 |   72000 B |
| ValueTypePreventingBoxing |   1000 |    397.679 ns |   0.3506 ns |   0.3108 ns |       - |         - |
