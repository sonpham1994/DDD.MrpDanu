.Net 7
| Method                                 | Mean     | Error   | StdDev  | Gen0   | Allocated |
|--------------------------------------- |---------:|--------:|--------:|-------:|----------:|
| CompareSequentialGuidWithCustomMethod  | 233.7 ns | 4.74 ns | 9.89 ns |      - |         - |
| CompareSequentialGuidWithSqlGuidMethod | 334.9 ns | 6.77 ns | 9.92 ns | 0.0505 |     160 B |

.Net 8
| Method                                 | Mean     | Error   | StdDev  | Allocated |
|--------------------------------------- |---------:|--------:|--------:|----------:|
| CompareSequentialGuidWithCustomMethod  | 111.2 ns | 0.20 ns | 0.19 ns |         - |
| CompareSequentialGuidWithSqlGuidMethod | 166.4 ns | 0.90 ns | 0.75 ns |         - |
