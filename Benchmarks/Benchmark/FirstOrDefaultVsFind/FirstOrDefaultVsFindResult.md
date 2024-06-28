| Method         | Length | Mean           | Error       | StdDev      | Median         | Gen0   | Allocated |
|--------------- |------- |---------------:|------------:|------------:|---------------:|-------:|----------:|
| FirstOrDefault | 10     |    315.6580 ns |   3.4003 ns |   2.8394 ns |    315.0444 ns | 0.0408 |     128 B |
| Find           | 10     |     80.9216 ns |   1.4024 ns |   1.1710 ns |     80.7371 ns | 0.0280 |      88 B |
| Index          | 10     |      1.0040 ns |   0.0248 ns |   0.0194 ns |      1.0101 ns |      - |         - |
| FirstOrDefault | 100    |  2,264.8020 ns |  44.8987 ns |  79.8074 ns |  2,231.8766 ns | 0.0381 |     128 B |
| Find           | 100    |    566.2273 ns |  11.2109 ns |  11.5127 ns |    562.9943 ns | 0.0277 |      88 B |
| Index          | 100    |      0.9527 ns |   0.1039 ns |   0.2568 ns |      0.8197 ns |      - |         - |
| FirstOrDefault | 1000   | 21,299.3508 ns | 158.2113 ns | 140.2502 ns | 21,332.6630 ns | 0.0305 |     128 B |
| Find           | 1000   |  5,316.1644 ns | 105.2289 ns | 169.9251 ns |  5,265.0509 ns | 0.0229 |      88 B |
| Index          | 1000   |      1.1112 ns |   0.1029 ns |   0.1302 ns |      1.0429 ns |      - |         - |


.Net8
| Method                            | Length | Mean         | Error       | StdDev      | Median       | Gen0   | Allocated |
|---------------------------------- |------- |-------------:|------------:|------------:|-------------:|-------:|----------:|
| FirstOrDefault                    | 10     |   117.014 ns |   3.2384 ns |   9.5485 ns |   114.680 ns | 0.0331 |     104 B |
| Find                              | 10     |    57.036 ns |   1.4091 ns |   4.1105 ns |    55.284 ns | 0.0204 |      64 B |
| Index                             | 10     |     1.274 ns |   0.1072 ns |   0.2526 ns |     1.211 ns |      - |         - |
| FindOrFirstOrDefaultWithPredicate | 10     |    56.516 ns |   2.7766 ns |   7.9218 ns |    54.589 ns | 0.0204 |      64 B |
| FindOrFirstOrDefaultWithWithFunc  | 10     |   120.195 ns |   2.6521 ns |   7.7780 ns |   118.734 ns | 0.0408 |     128 B |
| FirstOrDefault                    | 100    |   692.149 ns |  12.5665 ns |  18.0225 ns |   687.441 ns | 0.0324 |     104 B |
| Find                              | 100    |   425.056 ns |  11.5071 ns |  33.7482 ns |   418.520 ns | 0.0200 |      64 B |
| Index                             | 100    |     1.350 ns |   0.1641 ns |   0.4736 ns |     1.147 ns |      - |         - |
| FindOrFirstOrDefaultWithPredicate | 100    |   363.857 ns |  14.1032 ns |  40.9158 ns |   352.037 ns | 0.0200 |      64 B |
| FindOrFirstOrDefaultWithWithFunc  | 100    |   808.022 ns |  16.2347 ns |  36.3113 ns |   795.325 ns | 0.0401 |     128 B |
| FirstOrDefault                    | 1000   | 5,787.181 ns | 149.9662 ns | 437.4583 ns | 5,647.320 ns | 0.0305 |     104 B |
| Find                              | 1000   | 3,532.690 ns |  70.5836 ns | 182.1988 ns | 3,470.973 ns | 0.0191 |      64 B |
| Index                             | 1000   |     1.636 ns |   0.2326 ns |   0.6636 ns |     1.509 ns |      - |         - |
| FindOrFirstOrDefaultWithPredicate | 1000   | 2,966.541 ns |  85.3809 ns | 244.9738 ns | 2,877.614 ns | 0.0191 |      64 B |
| FindOrFirstOrDefaultWithWithFunc  | 1000   | 7,802.184 ns | 154.1201 ns | 383.8126 ns | 7,742.376 ns | 0.0381 |     128 B |