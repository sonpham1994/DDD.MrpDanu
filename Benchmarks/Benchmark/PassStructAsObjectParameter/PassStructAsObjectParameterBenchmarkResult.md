| Method                                                             | Length |        Mean |     Error |    StdDev |      Median |   Gen0 | Allocated |
|--------------------------------------------------------------------|------- |------------:|----------:|----------:|------------:|-------:|----------:|
| CreateStructAsInterfaceWithAssigningValueType                      |      1 |   0.0250 ns | 0.0026 ns | 0.0023 ns |   0.0250 ns |      - |         - |
| CreateStructAsStructWithAssigningValueType                         |      1 |   0.0260 ns | 0.0030 ns | 0.0023 ns |   0.0261 ns |      - |         - |
| CreateStructAsInterfaceWithAssigningReferenceType                  |      1 |   0.0249 ns | 0.0025 ns | 0.0021 ns |   0.0249 ns |      - |         - |
| CreateStructAsStructWithAssigningReferenceType                     |      1 |   0.0255 ns | 0.0023 ns | 0.0019 ns |   0.0256 ns |      - |         - |
| CreateStructAsStructWithAssigningValueAndReferenceType             |      1 |   0.0236 ns | 0.0023 ns | 0.0020 ns |   0.0236 ns |      - |         - |
| CreateStructAsInterfaceWithAssigningValueAndReferenceType          |      1 |  10.8743 ns | 0.0367 ns | 0.0326 ns |  10.8829 ns | 0.0076 |      32 B |
| PassStructAsStructAndDoNothing                                     |      1 |   0.0000 ns | 0.0000 ns | 0.0000 ns |   0.0000 ns |      - |         - |
| PassClassAsClassAndDoNothing                                       |      1 |  10.0562 ns | 0.1211 ns | 0.0945 ns |  10.0417 ns | 0.0076 |      32 B |
| PassStructAsObjectAndDoNothing                                     |      1 |   0.0251 ns | 0.0024 ns | 0.0021 ns |   0.0249 ns |      - |         - |
| PassStructAsObjectAndAssigningAgeStructPerson                      |      1 |  13.3257 ns | 0.0928 ns | 0.0868 ns |  13.3160 ns | 0.0076 |      32 B |
| PassStructAsObjectAndAssigningNameStructPerson                     |      1 |  13.7379 ns | 0.2315 ns | 0.1933 ns |  13.7664 ns | 0.0076 |      32 B |
| PassStructAsObjectAndAssigningAgeInterfaceStructPerson             |      1 |  11.1859 ns | 0.3822 ns | 1.1027 ns |  10.5365 ns | 0.0076 |      32 B |
| PassStructAsObjectAndAssigningNameInterfaceStructPerson            |      1 |  10.9426 ns | 0.2623 ns | 0.2576 ns |  10.8286 ns | 0.0076 |      32 B |
| PassStructAsGenericTypeAndAssigningNameAndAgeStructPerson          |      1 |   0.0271 ns | 0.0038 ns | 0.0030 ns |           - |      - |         - |
| PassStructAsGenericTypeAndAssigningNameAndAgeInterfaceStructPerson |      1 |  10.5443 ns | 0.0783 ns | 0.0732 ns |           - | 0.0076 |      32 B |
| PassClassAsObjectAndDoNothing                                      |      1 |  10.3939 ns | 0.0733 ns | 0.0650 ns |  10.3979 ns | 0.0076 |      32 B |
| PassGuidAsObjectAndDoNothing                                       |      1 | 445.3472 ns | 0.7231 ns | 0.6038 ns | 445.2674 ns |      - |         - |
| PassGuidAsObjectAndAssigningGuid                                   |      1 | 464.2209 ns | 8.4523 ns | 7.0581 ns | 461.1785 ns | 0.0076 |      32 B |
| PassIntAsObjectAndDoNothing                                        |      1 |   0.0295 ns | 0.0037 ns | 0.0034 ns |   0.0296 ns |      - |         - |
| PassIntAsObjectAndAssigningInt                                     |      1 |   6.7517 ns | 0.0319 ns | 0.0267 ns |   6.7521 ns | 0.0057 |      24 B |
