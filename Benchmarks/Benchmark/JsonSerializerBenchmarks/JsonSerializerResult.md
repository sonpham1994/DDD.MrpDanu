| Method                                                                      | Mean         | Error        | StdDev       | Median       | Gen0   | Allocated |
|---------------------------------------------------------------------------- |-------------:|-------------:|-------------:|-------------:|-------:|----------:|
| NewtonsoftJsonSerializerWithClassImplementation                             |   6,929.8 ns |    255.23 ns |    740.48 ns |   6,606.2 ns | 1.0452 |    3280 B |
| SystemTextJsonSerializerWithClassImplementation                             |   3,216.8 ns |     65.95 ns |    184.94 ns |   3,191.6 ns | 0.3128 |     992 B |
| SystemTextJsonSerializerSourceGeneratorDefaultWithClassImplementation       |   1,751.0 ns |     44.24 ns |    126.94 ns |   1,712.5 ns | 0.2155 |     680 B |
| SystemTextJsonSerializerSourceGeneratorMetadataWithClassImplementation      |   3,029.6 ns |     60.86 ns |    174.61 ns |   2,944.1 ns | 0.3128 |     992 B |
| SystemTextJsonSerializerSourceGeneratorSerializationWithClassImplementation |   1,254.5 ns |    144.79 ns |    426.91 ns |   1,546.1 ns | 0.2155 |     680 B |
| NewtonsoftJsonSerializerWithInterface                                       |   6,449.1 ns |    135.07 ns |    380.96 ns |   6,321.0 ns | 1.0376 |    3280 B |
| SystemTextJsonSerializerWithInterface                                       |     542.1 ns |     10.68 ns |      8.92 ns |     539.5 ns | 0.0353 |     112 B |
| NewtonsoftJsonDeserializerWithClassImplementation                           |  11,684.7 ns |    258.31 ns |    741.14 ns |  11,625.3 ns | 1.1902 |    3760 B |
| SystemTextJsonDeserializerWithClassImplementation                           |   5,736.6 ns |    181.35 ns |    520.33 ns |   5,577.8 ns | 0.2823 |     904 B |
| SystemTextJsonDeserializerSourceGeneratorDefaultWithClassImplementation     |   5,221.7 ns |     66.58 ns |     55.60 ns |   5,208.2 ns | 0.2823 |     904 B |
| SystemTextJsonDeserializerSourceGeneratorMetadataWithClassImplementation    |   5,328.7 ns |    103.56 ns |    127.19 ns |   5,284.0 ns | 0.2823 |     904 B |
| ApiResponseWithChildDataWithSourceGenerator                                 |     833.1 ns |     54.70 ns |    146.01 ns |     773.4 ns | 0.2441 |     768 B |
| ApiResponseWithChildDataWithoutSourceGenerator                              |   1,734.0 ns |      9.74 ns |      7.60 ns |   1,734.3 ns | 0.2441 |     768 B |
| LoggingSystemTextJsonStructWithNoSourceGeneratorSerializer                  | 357,516.0 ns | 12,031.79 ns | 34,521.46 ns | 349,229.8 ns | 0.9766 |    3143 B |
| LoggingSystemTextJsonStructSerializer                                       | 351,729.1 ns | 11,507.73 ns | 32,078.95 ns | 344,540.7 ns | 0.9766 |    3095 B |
| LoggingSystemTextJsonStructCustomSerializer                                 | 308,784.5 ns | 10,031.40 ns | 28,781.96 ns | 306,016.5 ns |      - |    1520 B |
| LoggingSystemTextJsonStructGenericWithNoSourceGeneratorSerializer           | 631,345.6 ns | 10,266.54 ns | 10,985.08 ns | 631,103.2 ns | 2.9297 |   10161 B |
| LoggingSystemTextJsonStructGenericSerializer                                | 615,419.6 ns | 11,882.98 ns | 26,821.88 ns | 605,239.1 ns | 2.9297 |   10112 B |
| LoggingSystemTextJsonStructGenericCustomSerializer                          | 548,396.8 ns | 10,481.35 ns | 15,688.00 ns | 545,550.9 ns | 0.9766 |    3465 B |