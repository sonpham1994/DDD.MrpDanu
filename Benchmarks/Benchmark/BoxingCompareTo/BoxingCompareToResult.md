| Method                                       | Mean     | Error    | StdDev   | Gen0   | Allocated |
|--------------------------------------------- |---------:|---------:|---------:|-------:|----------:|
| CompareToMyClassGenericWithComparable        | 23.90 ns | 0.144 ns | 0.120 ns | 0.0172 |      72 B |
| CompareToMyClassGenericWithComparableGeneric | 13.80 ns | 0.043 ns | 0.033 ns | 0.0115 |      48 B |
| CompareToMyClassGenericWithComparableInt     | 13.77 ns | 0.064 ns | 0.057 ns | 0.0115 |      48 B |

CompareToMyClassGenericWithComparable -> boxing