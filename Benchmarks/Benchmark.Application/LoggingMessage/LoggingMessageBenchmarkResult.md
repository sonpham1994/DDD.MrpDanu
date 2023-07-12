### Log Message for case boxing, of which case LoggingInfoMessage, LoggingWarningMessage, LoggingInfoMessageDefinitionWithObjectStruct,
### LoggingWarningMessageDefinitionWithObjectStruct are boxing, and the rest are not.

| Method                                          |          Mean |        Error |        StdDev |   Gen0 |   Gen1 | Allocated |
|-------------------------------------------------|--------------:|-------------:|--------------:|-------:|-------:|----------:|
| LoggingInfoMessage                              | 178,099.91 ns | 3,556.727 ns |  7,424.213 ns | 0.0610 |      - |     392 B |
| LoggingInfoMessageDefinition                    | 181,281.77 ns | 8,506.114 ns | 24,946.959 ns |      - |      - |     352 B |
| LoggingWarningMessage                           |     132.65 ns |     0.196 ns |      0.174 ns | 0.0153 |      - |      64 B |
| LoggingWarningMessageDefinition                 |      13.45 ns |     0.153 ns |      0.135 ns |      - |      - |         - |
| LoggingInfoMessageDefinitionWithStruct          | 181,496.90 ns | 7,884.623 ns | 23,247.989 ns |      - |      - |     352 B |
| LoggingInfoMessageDefinitionWithObjectStruct    | 180,679.67 ns | 8,753.850 ns | 25,673.527 ns |      - |      - |     384 B |
| LoggingWarningMessageDefinitionWithStruct       |      15.36 ns |     0.027 ns |      0.023 ns |      - |      - |         - |
| LoggingWarningMessageDefinitionWithObjectStruct |      28.99 ns |     0.146 ns |      0.137 ns | 0.0076 |      - |      32 B |




