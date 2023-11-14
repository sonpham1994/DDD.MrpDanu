If the first property doesn't equals
| Method                             | Mean     | Error   | StdDev   | Median   | Gen0   | Allocated |
|----------------------------------- |---------:|--------:|---------:|---------:|-------:|----------:|
| ValueObjectEqualsBoxing            | 196.0 ns | 7.42 ns | 21.63 ns | 188.9 ns | 0.0458 |     144 B |
| ValueObjectEqualsWithNoValueBoxing | 139.8 ns | 2.85 ns |  4.35 ns | 138.5 ns | 0.0253 |      80 B |

If the third property doesn't equals
| Method                             | Mean      | Error     | StdDev    | Median    | Gen0   | Allocated |
|----------------------------------- |----------:|----------:|----------:|----------:|-------:|----------:|
| ValueObjectEqualsBoxing            | 466.16 ns | 17.325 ns | 51.082 ns | 462.37 ns | 0.0863 |     272 B |
| ValueObjectEqualsWithNoValueBoxing | 326.55 ns | 12.470 ns | 36.768 ns | 325.16 ns | 0.0253 |      80 B |
| ValueObjectEqualsWithAvoidBoxing   |  23.95 ns |  1.214 ns |  3.560 ns |  23.01 ns |      - |         - |