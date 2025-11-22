| Method                            | Length | Mean         | Error     | StdDev      | Median       | Allocated |
|---------------------------------- |------- |-------------:|----------:|------------:|-------------:|----------:|
| ArrayStruct_AssigningData         | 10     |     58.78 ns |  14.51 ns |    40.68 ns |     83.00 ns |     736 B |
| ArrayStruct_AssigningStruct       | 10     |     67.28 ns |  10.94 ns |    30.50 ns |     84.00 ns |     736 B |
| ArrayStruct_CheckAndAssigningData | 10     |     79.30 ns |  19.36 ns |    53.96 ns |     83.00 ns |     736 B |
| ArrayClass_AssigningData          | 10     |     61.95 ns |  13.26 ns |    37.61 ns |     42.00 ns |     736 B |
| ArrayClass_AssigningClass         | 10     |    468.57 ns |  33.97 ns |    97.45 ns |    458.50 ns |     976 B |
| ArrayClass_CheckAndAssigningData  | 10     |     62.46 ns |  13.22 ns |    36.63 ns |     83.00 ns |      64 B |
| ArrayStruct_AssigningData         | 100    |    430.17 ns |  23.99 ns |    63.20 ns |    416.00 ns |     736 B |
| ArrayStruct_AssigningStruct       | 100    |    450.75 ns |  22.62 ns |    59.58 ns |    458.00 ns |     448 B |
| ArrayStruct_CheckAndAssigningData | 100    |    469.83 ns |  34.63 ns |    91.22 ns |    459.00 ns |     736 B |
| ArrayClass_AssigningData          | 100    |    436.33 ns |  33.87 ns |    89.22 ns |    416.00 ns |     736 B |
| ArrayClass_AssigningClass         | 100    |  1,354.81 ns |  83.57 ns |   238.43 ns |  1,291.50 ns |    3136 B |
| ArrayClass_CheckAndAssigningData  | 100    |    467.32 ns |  29.21 ns |    76.96 ns |    459.00 ns |     736 B |
| ArrayStruct_AssigningData         | 1000   |  7,849.82 ns | 529.03 ns | 1,509.34 ns |  7,833.00 ns |     448 B |
| ArrayStruct_AssigningStruct       | 1000   |  8,099.52 ns | 467.17 ns | 1,325.29 ns |  8,083.00 ns |     736 B |
| ArrayStruct_CheckAndAssigningData | 1000   |  9,679.21 ns | 752.25 ns | 2,182.41 ns |  9,458.00 ns |     736 B |
| ArrayClass_AssigningData          | 1000   |  7,863.64 ns | 522.70 ns | 1,465.71 ns |  7,958.00 ns |     736 B |
| ArrayClass_AssigningClass         | 1000   | 13,693.09 ns | 523.24 ns | 1,475.82 ns | 14,083.00 ns |   24736 B |
| ArrayClass_CheckAndAssigningData  | 1000   |  9,810.32 ns | 739.79 ns | 2,110.66 ns |  9,604.00 ns |     736 B |
