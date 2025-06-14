| Method                                                                       | Mean         | Error        | StdDev        | Median       | Gen0   | Gen1   | Allocated |
|----------------------------------------------------------------------------- |-------------:|-------------:|--------------:|-------------:|-------:|-------:|----------:|
| NewtonsoftJsonSerializerWithClassImplementation                              |   4,065.2 ns |     80.31 ns |     221.19 ns |   3,969.4 ns | 0.7782 |      - |    3280 B |
| SystemTextJsonSerializerWithClassImplementation                              |   2,036.2 ns |     33.83 ns |      33.22 ns |   2,025.8 ns | 0.2365 |      - |     992 B |
| SystemTextJsonSerializerSourceGeneratorDefaultWithClassImplementation        |   1,111.3 ns |     21.99 ns |      57.92 ns |   1,087.6 ns | 0.1621 |      - |     680 B |
| SystemTextJsonSerializerSourceGeneratorMetadataWithClassImplementation       |   1,988.7 ns |     11.26 ns |       9.98 ns |   1,986.3 ns | 0.2365 |      - |     992 B |
| SystemTextJsonSerializerSourceGeneratorSerializationWithClassImplementation  |   1,075.5 ns |      8.44 ns |       7.05 ns |   1,073.0 ns | 0.1621 |      - |     680 B |
| NewtonsoftJsonSerializerWithRecordImplementation                             |   4,045.6 ns |     28.88 ns |      22.55 ns |   4,052.3 ns | 0.7782 |      - |    3280 B |
| SystemTextJsonSerializerWithRecordImplementation                             |   2,059.6 ns |     22.75 ns |      21.28 ns |   2,052.4 ns | 0.2365 |      - |     992 B |
| SystemTextJsonSerializerSourceGeneratorDefaultWithRecordImplementation       |   1,119.2 ns |     20.90 ns |      21.46 ns |   1,112.6 ns | 0.1621 |      - |     680 B |
| SystemTextJsonSerializerSourceGeneratorMetadataWithRecordImplementation      |   2,011.1 ns |     32.16 ns |      26.85 ns |   2,001.6 ns | 0.2365 |      - |     992 B |
| SystemTextJsonSerializerSourceGeneratorSerializationWithRecordImplementation |   1,217.1 ns |     44.65 ns |     122.97 ns |   1,170.5 ns | 0.1621 |      - |     680 B |
| NewtonsoftJsonSerializerWithInterface                                        |   3,975.7 ns |     36.72 ns |      30.66 ns |   3,967.8 ns | 0.7782 |      - |    3280 B |
| SystemTextJsonSerializerWithInterface                                        |     379.3 ns |      2.47 ns |       2.06 ns |     378.5 ns | 0.0267 |      - |     112 B |
| NewtonsoftJsonDeserializerWithClassImplementation                            |   7,000.1 ns |     76.14 ns |      63.58 ns |   6,978.8 ns | 0.8926 |      - |    3760 B |
| SystemTextJsonDeserializerWithClassImplementation                            |   3,569.0 ns |     19.50 ns |      16.28 ns |   3,564.0 ns | 0.2136 |      - |     904 B |
| SystemTextJsonDeserializerSourceGeneratorDefaultWithClassImplementation      |   3,420.5 ns |     33.06 ns |      27.61 ns |   3,404.5 ns | 0.2136 |      - |     904 B |
| SystemTextJsonDeserializerSourceGeneratorMetadataWithClassImplementation     |   3,456.7 ns |     38.67 ns |      32.29 ns |   3,440.2 ns | 0.2136 |      - |     904 B |
| NewtonsoftJsonDeserializerWithRecordImplementation                           |   7,544.9 ns |    124.22 ns |     110.11 ns |   7,497.7 ns | 1.2054 |      - |    5088 B |
| SystemTextJsonDeserializerWithRecordImplementation                           |   4,558.7 ns |     45.83 ns |      40.62 ns |   4,547.9 ns | 0.3510 |      - |    1472 B |
| SystemTextJsonDeserializerSourceGeneratorDefaultWithRecordImplementation     |   4,551.1 ns |     41.26 ns |      34.45 ns |   4,542.8 ns | 0.3281 |      - |    1400 B |
| SystemTextJsonDeserializerSourceGeneratorMetadataWithRecordImplementation    |   4,845.1 ns |     95.77 ns |     193.46 ns |   4,820.7 ns | 0.3281 |      - |    1400 B |
| ApiResponseWithChildDataWithSourceGenerator                                  |   1,185.1 ns |     14.84 ns |      13.15 ns |   1,180.8 ns | 0.1831 | 0.0019 |     768 B |
| ApiResponseWithChildDataWithoutSourceGenerator                               |   1,165.9 ns |      8.11 ns |       6.33 ns |   1,165.9 ns | 0.1831 | 0.0019 |     768 B |
| LoggingSystemTextJsonStructWithNoSourceGeneratorSerializer                   | 182,459.9 ns |  3,648.26 ns |   7,201.32 ns | 180,282.0 ns | 0.7324 |      - |    3137 B |
| LoggingSystemTextJsonStructSerializer                                        | 166,917.6 ns |  5,316.59 ns |  14,908.30 ns | 167,023.8 ns | 0.7324 |      - |    3090 B |
| LoggingSystemTextJsonStructCustomSerializer                                  | 126,622.6 ns |  5,454.86 ns |  14,654.13 ns | 125,858.5 ns | 0.3052 |      - |    1521 B |
| LoggingSystemTextJsonStructGenericWithNoSourceGeneratorSerializer            | 674,410.5 ns | 52,719.00 ns | 148,694.87 ns | 658,165.5 ns | 1.9531 |      - |   10152 B |
| LoggingSystemTextJsonStructGenericSerializer                                 | 666,969.4 ns | 47,085.65 ns | 131,255.97 ns | 654,047.1 ns | 1.9531 |      - |   10104 B |
| LoggingSystemTextJsonStructGenericCustomSerializer                           | 502,177.8 ns | 22,254.97 ns |  63,853.69 ns | 499,705.4 ns | 0.7324 |      - |    3460 B |


.NET 9

| Method                                                                       | Mean          | Error      | StdDev     | Median        | Gen0   | Gen1   | Allocated |
|----------------------------------------------------------------------------- |--------------:|-----------:|-----------:|--------------:|-------:|-------:|----------:|
| NewtonsoftJsonSerializerWithClassImplementation                              |   754.7099 ns |  5.7517 ns |  4.8029 ns |   753.4324 ns | 0.3920 | 0.0019 |    3280 B |
| SystemTextJsonSerializerWithClassImplementation                              |   358.7245 ns |  4.0726 ns |  3.9998 ns |   357.4543 ns | 0.1183 |      - |     992 B |
| SystemTextJsonSerializerSourceGeneratorDefaultWithClassImplementation        |   232.1981 ns |  4.6508 ns |  8.3864 ns |   231.3850 ns | 0.0811 |      - |     680 B |
| SystemTextJsonSerializerSourceGeneratorMetadataWithClassImplementation       |   369.7578 ns |  7.3495 ns |  7.2182 ns |   368.7545 ns | 0.1183 |      - |     992 B |
| SystemTextJsonSerializerSourceGeneratorSerializationWithClassImplementation  |   221.8668 ns |  3.7053 ns |  3.4660 ns |   220.6962 ns | 0.0811 |      - |     680 B |
| NewtonsoftJsonSerializerWithRecordImplementation                             |   782.5686 ns |  9.6638 ns |  8.0697 ns |   779.2210 ns | 0.3920 | 0.0019 |    3280 B |
| SystemTextJsonSerializerWithRecordImplementation                             |   369.9993 ns |  7.0123 ns |  7.5031 ns |   368.5910 ns | 0.1183 |      - |     992 B |
| SystemTextJsonSerializerSourceGeneratorDefaultWithRecordImplementation       |   220.3926 ns |  3.1246 ns |  2.6091 ns |   218.9118 ns | 0.0811 |      - |     680 B |
| SystemTextJsonSerializerSourceGeneratorMetadataWithRecordImplementation      |   355.8741 ns |  7.0547 ns |  7.2446 ns |   352.3096 ns | 0.1183 |      - |     992 B |
| SystemTextJsonSerializerSourceGeneratorSerializationWithRecordImplementation |   220.8532 ns |  1.3025 ns |  1.0876 ns |   220.2909 ns | 0.0811 |      - |     680 B |
| NewtonsoftJsonSerializerWithInterface                                        |   772.9635 ns | 13.4846 ns | 12.6135 ns |   775.3174 ns | 0.3920 | 0.0019 |    3280 B |
| SystemTextJsonSerializerWithInterface                                        |    67.4523 ns |  1.2236 ns |  1.1446 ns |    66.9024 ns | 0.0134 |      - |     112 B |
| NewtonsoftJsonDeserializerWithClassImplementation                            | 1,297.0477 ns |  9.1508 ns |  8.1119 ns | 1,296.1020 ns | 0.4482 | 0.0038 |    3760 B |
| SystemTextJsonDeserializerWithClassImplementation                            |   703.5851 ns | 12.2904 ns | 10.8951 ns |   700.4071 ns | 0.1116 |      - |     936 B |
| SystemTextJsonDeserializerSourceGeneratorDefaultWithClassImplementation      |   675.5980 ns | 10.2436 ns |  9.5819 ns |   670.2450 ns | 0.1116 |      - |     936 B |
| SystemTextJsonDeserializerSourceGeneratorMetadataWithClassImplementation     |   663.4055 ns |  2.3181 ns |  1.8098 ns |   663.5779 ns | 0.1116 |      - |     936 B |
| NewtonsoftJsonDeserializerWithRecordImplementation                           | 1,546.5691 ns |  5.8691 ns |  5.4900 ns | 1,545.5220 ns | 0.6065 | 0.0057 |    5088 B |
| SystemTextJsonDeserializerWithRecordImplementation                           |   854.8085 ns | 17.0229 ns | 22.1346 ns |   841.8783 ns | 0.1650 |      - |    1384 B |
| SystemTextJsonDeserializerSourceGeneratorDefaultWithRecordImplementation     |   918.7501 ns |  4.9081 ns |  4.3509 ns |   919.4936 ns | 0.1564 |      - |    1312 B |
| SystemTextJsonDeserializerSourceGeneratorMetadataWithRecordImplementation    |   913.5225 ns |  4.3717 ns |  3.8754 ns |   912.7337 ns | 0.1564 |      - |    1312 B |
| ApiResponseWithChildDataWithSourceGenerator                                  |   235.0098 ns |  0.9333 ns |  0.8273 ns |   234.6210 ns | 0.0918 |      - |     768 B |
| ApiResponseWithChildDataWithoutSourceGenerator                               |   234.8341 ns |  0.4426 ns |  0.4140 ns |   234.6460 ns | 0.0918 |      - |     768 B |
| LoggingSystemTextJsonStructWithNoSourceGeneratorSerializer                   |     0.0000 ns |  0.0000 ns |  0.0000 ns |     0.0000 ns |      - |      - |         - |
| LoggingSystemTextJsonStructSerializer                                        |     0.0000 ns |  0.0000 ns |  0.0000 ns |     0.0000 ns |      - |      - |         - |
| LoggingSystemTextJsonStructCustomSerializer                                  |    90.3843 ns |  0.3136 ns |  0.2780 ns |    90.4183 ns | 0.0191 |      - |     160 B |
| LoggingSystemTextJsonStructGenericWithNoSourceGeneratorSerializer            |     0.0000 ns |  0.0000 ns |  0.0000 ns |     0.0000 ns |      - |      - |         - |
| LoggingSystemTextJsonStructGenericSerializer                                 |     0.0000 ns |  0.0000 ns |  0.0000 ns |     0.0000 ns |      - |      - |         - |
| LoggingSystemTextJsonStructGenericCustomSerializer                           |   280.7309 ns |  1.1890 ns |  1.0540 ns |   280.6666 ns | 0.1001 |      - |     840 B |

