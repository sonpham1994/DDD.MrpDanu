| Method             | Mean      | Error    | StdDev   | Gen0   | Allocated |
|------------------- |----------:|---------:|---------:|-------:|----------:|
| BoxingEquals       |  65.23 ns | 2.990 ns | 8.676 ns | 0.0306 |      96 B |
| AvoidBoxingEquals  |  20.66 ns | 0.469 ns | 0.822 ns |      - |         - |
| BoxingCompare      | 161.92 ns | 3.038 ns | 5.321 ns | 0.0203 |      64 B |
| AvoidBoxingCompare |  67.11 ns | 1.423 ns | 2.086 ns |      - |         - |