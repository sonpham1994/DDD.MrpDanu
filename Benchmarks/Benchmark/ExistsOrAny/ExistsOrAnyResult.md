.Net8

| Method                  | Length | Mean        | Error      | StdDev     | Median      | Gen0   | Allocated |
|------------------------ |------- |------------:|-----------:|-----------:|------------:|-------:|----------:|
| Exists                  | 10     |    50.59 ns |   1.057 ns |   1.086 ns |    50.27 ns | 0.0204 |      64 B |
| Any                     | 10     |   105.78 ns |   2.184 ns |   5.868 ns |   104.25 ns | 0.0331 |     104 B |
| ExistOrAnyWithPredicate | 10     |    50.29 ns |   1.643 ns |   4.844 ns |    48.73 ns | 0.0204 |      64 B |
| ExistOrAnWithFunc       | 10     |   112.16 ns |   2.227 ns |   2.083 ns |   112.10 ns | 0.0408 |     128 B |
| Exists                  | 100    |   260.53 ns |   5.293 ns |  11.730 ns |   257.00 ns | 0.0200 |      64 B |
| Any                     | 100    |   681.28 ns |  13.450 ns |  17.489 ns |   675.83 ns | 0.0324 |     104 B |
| ExistOrAnyWithPredicate | 100    |   259.16 ns |   5.244 ns |  12.664 ns |   254.80 ns | 0.0200 |      64 B |
| ExistOrAnWithFunc       | 100    |   826.59 ns |  17.144 ns |  50.549 ns |   804.27 ns | 0.0401 |     128 B |
| Exists                  | 1000   | 2,261.66 ns |  68.179 ns | 195.619 ns | 2,234.98 ns | 0.0191 |      64 B |
| Any                     | 1000   | 5,404.11 ns | 142.966 ns | 417.038 ns | 5,279.59 ns | 0.0305 |     104 B |
| ExistOrAnyWithPredicate | 1000   | 2,135.48 ns |  42.599 ns | 114.438 ns | 2,109.41 ns | 0.0191 |      64 B |
| ExistOrAnWithFunc       | 1000   | 7,279.76 ns |  68.776 ns |  67.548 ns | 7,265.95 ns | 0.0381 |     128 B |