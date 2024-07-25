| Method                                    | Mean     | Error   | StdDev  | Gen0   | Allocated |
|------------------------------------------ |---------:|--------:|--------:|-------:|----------:|
| AssignGuidUsingInterfaceExt               | 300.0 ns | 2.26 ns | 2.11 ns | 0.0038 |      32 B |
| AssignGuidUsingStructExt                  | 298.3 ns | 1.33 ns | 1.24 ns |      - |         - |
| AssignGuidUsingInterfaceExtAndComparision | 601.2 ns | 4.90 ns | 4.58 ns | 0.0076 |      64 B |
| AssignGuidUsingStructExtAndComparision    | 607.6 ns | 7.88 ns | 7.37 ns |      - |         - |
| NotAssignGuid                             | 295.4 ns | 0.91 ns | 0.81 ns |      - |         - |
