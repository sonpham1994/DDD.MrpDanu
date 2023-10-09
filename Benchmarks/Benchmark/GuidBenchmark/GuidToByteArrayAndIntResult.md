|                            Method |     Mean |   Error |  StdDev |   Gen0 | Allocated |
|---------------------------------- |---------:|--------:|--------:|-------:|----------:|
|                ConvertGuidToArray | 465.1 ns | 2.24 ns | 1.99 ns | 0.0095 |      40 B |
| ConvertGuidToNumberUsingByteArray | 469.2 ns | 1.85 ns | 1.64 ns | 0.0095 |      40 B |
|  ConvertGuidToIntUsingGetHashCode | 449.3 ns | 1.12 ns | 0.87 ns |      - |         - |
