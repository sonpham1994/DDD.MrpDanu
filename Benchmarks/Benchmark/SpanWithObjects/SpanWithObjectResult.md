| Method                  | Length |      Mean |     Error |    StdDev |    Median |   Gen0 | Allocated |
|-------------------------|------- |----------:|----------:|----------:|----------:|-------:|----------:|
| GetClassTests           |     10 | 12.820 ns | 0.0613 ns | 0.0543 ns | 12.821 ns | 0.0095 |      40 B |
| GetClassTestForSpans    |     10 |  2.363 ns | 0.0060 ns | 0.0050 ns |  2.364 ns |      - |         - |
| ExistsClassTest         |     10 | 58.841 ns | 0.2709 ns | 0.2401 ns | 58.823 ns | 0.0324 |     136 B |
| ExistsClassTestForSpans |     10 | 16.273 ns | 0.1784 ns | 0.1669 ns | 16.164 ns |      - |         - |
| GetClassTests           |    100 | 13.455 ns | 0.3600 ns | 1.0035 ns | 13.071 ns | 0.0095 |      40 B |
| GetClassTestForSpans    |    100 |  2.346 ns | 0.0265 ns | 0.0234 ns |  2.340 ns |      - |         - |
| ExistsClassTest         |    100 | 61.620 ns | 1.2835 ns | 2.7629 ns | 61.283 ns | 0.0324 |     136 B |
| ExistsClassTestForSpans |    100 | 16.818 ns | 0.1955 ns | 0.1829 ns | 16.812 ns |      - |         - |
