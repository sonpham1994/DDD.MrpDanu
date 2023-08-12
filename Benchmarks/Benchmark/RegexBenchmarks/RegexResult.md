|                      Method | Length |         Mean |      Error |     StdDev | Allocated |
|---------------------------- |------- |-------------:|-----------:|-----------:|----------:|
|       RegexWithEmailPattern |     10 |    32.920 us |  0.0373 us |  0.0312 us |         - |
| RegexWithEmailPatternStatic |     10 |     1.197 us |  0.0028 us |  0.0022 us |         - |
|       RegexWithEmailPattern |    100 |   327.217 us |  3.1038 us |  3.0483 us |         - |
| RegexWithEmailPatternStatic |    100 |    11.674 us |  0.0141 us |  0.0110 us |         - |
|       RegexWithEmailPattern |   1000 | 3,323.026 us | 44.0460 us | 45.2320 us |       4 B |
| RegexWithEmailPatternStatic |   1000 |   118.746 us |  0.2412 us |  0.2014 us |         - |
