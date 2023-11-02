| Method                              | Length | Mean          | Error       | StdDev      | Median        | Gen0   | Allocated |
|------------------------------------ |------- |--------------:|------------:|------------:|--------------:|-------:|----------:|
| CreateStaticString                  | 10     |      7.337 ns |   0.2191 ns |   0.3950 ns |      7.203 ns |      - |         - |
| CreateConstString                   | 10     |      7.509 ns |   0.2845 ns |   0.8299 ns |      7.210 ns |      - |         - |
| UsingEmptyStringNormalWay           | 10     |      7.516 ns |   0.3214 ns |   0.9223 ns |      7.217 ns |      - |         - |
| UsingEmptyStringProvidedByFramework | 10     |      6.711 ns |   0.1533 ns |   0.1640 ns |      6.629 ns |      - |         - |
| CacheInternalStringForReuse         | 10     |  1,060.065 ns |  18.2937 ns |  23.7870 ns |  1,059.224 ns | 0.1774 |     560 B |
| NotCacheInternalStringForReuse      | 10     |  1,088.557 ns |  28.3546 ns |  79.5092 ns |  1,062.405 ns | 0.1774 |     560 B |
| CreateStaticString                  | 100    |     82.549 ns |   0.7302 ns |   0.6473 ns |     82.534 ns |      - |         - |
| CreateConstString                   | 100    |     83.917 ns |   1.7344 ns |   1.4483 ns |     83.611 ns |      - |         - |
| UsingEmptyStringNormalWay           | 100    |     82.532 ns |   0.5874 ns |   0.5207 ns |     82.531 ns |      - |         - |
| UsingEmptyStringProvidedByFramework | 100    |     82.573 ns |   1.0316 ns |   0.8614 ns |     82.317 ns |      - |         - |
| CacheInternalStringForReuse         | 100    | 12,403.699 ns | 245.9463 ns | 430.7550 ns | 12,297.104 ns | 2.0142 |    6320 B |
| NotCacheInternalStringForReuse      | 100    | 11,165.577 ns | 318.2022 ns | 881.7369 ns | 10,886.676 ns | 2.0142 |    6320 B |