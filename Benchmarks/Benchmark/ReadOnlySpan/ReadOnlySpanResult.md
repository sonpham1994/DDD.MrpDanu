.NET 9

| Method                               | Mean      | Error     | StdDev    | Median    | Gen0   | Allocated |
|------------------------------------- |----------:|----------:|----------:|----------:|-------:|----------:|
| ReadOnlySpanWithStackAlloc           | 0.1046 ns | 0.0048 ns | 0.0038 ns | 0.1045 ns |      - |         - |
| ReadOnlySpanWithCollectionExpression | 0.0229 ns | 0.0240 ns | 0.0276 ns | 0.0050 ns |      - |         - |
| ReadOnlySpanStackAllocWithString     | 5.7335 ns | 0.0193 ns | 0.0151 ns | 5.7369 ns | 0.0051 |      32 B |
| ReadOnlySpanCollectionExpressionWithString | 5.5921 ns | 0.1366 ns | 0.1573 ns | 5.4843 ns | 0.0051 |      32 B |
| CharArrayWithString                  | 7.3755 ns | 0.0457 ns | 0.0357 ns | 7.3682 ns | 0.0102 |      64 B |
