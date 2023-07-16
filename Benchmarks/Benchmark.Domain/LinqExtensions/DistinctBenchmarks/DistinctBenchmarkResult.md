### Guids
| Method                                             | Length |            Mean |         Error |        StdDev |    Gen0 | Allocated |
|----------------------------------------------------|------- |----------------:|--------------:|--------------:|--------:|----------:|
| DistinctWithoutDuplication                         |     10 |        471.5 ns |       4.90 ns |       4.58 ns |  0.1812 |     760 B |
| CheckingDuplicationBeforeExecuteWithoutDuplication |     10 |      1,245.2 ns |       6.41 ns |       5.35 ns |       - |         - |
| DistinctWithDuplication                            |     10 |        524.3 ns |       4.72 ns |       4.41 ns |  0.1812 |     760 B |
| CheckingDuplicationBeforeExecuteWithDuplication    |     10 |      1,700.3 ns |       5.56 ns |       4.93 ns |  0.1888 |     792 B |
| DistinctWithoutDuplication                         |    100 |      3,825.0 ns |      31.48 ns |      29.45 ns |  1.1673 |    4888 B |
| CheckingDuplicationBeforeExecuteWithoutDuplication |    100 |    114,802.6 ns |   1,882.65 ns |   1,849.02 ns |       - |         - |
| DistinctWithDuplication                            |    100 |      3,736.4 ns |      67.62 ns |      63.26 ns |  1.1673 |    4888 B |
| CheckingDuplicationBeforeExecuteWithDuplication    |    100 |     92,240.5 ns |   1,023.00 ns |     906.87 ns |  1.0986 |    4920 B |
| DistinctWithoutDuplication                         |   1000 |     39,978.1 ns |     760.84 ns |     747.25 ns | 11.2305 |   47176 B |
| CheckingDuplicationBeforeExecuteWithoutDuplication |   1000 | 11,315,608.0 ns | 147,837.49 ns | 131,054.08 ns |       - |      15 B |
| DistinctWithDuplication                            |   1000 |     39,656.4 ns |     727.97 ns |   1,175.53 ns | 11.2305 |   47176 B |
| CheckingDuplicationBeforeExecuteWithDuplication    |   1000 |  8,498,859.1 ns |  59,101.47 ns |  55,283.56 ns |       - |   47223 B |

### Entity
| Method                                                   | Length |            Mean |        Error |       StdDev |   Gen0 | Allocated |
|----------------------------------------------------------|------- |----------------:|-------------:|-------------:|-------:|----------:|
| EntityDistinctWithoutDuplication                         |     10 |        800.3 ns |      3.52 ns |      3.29 ns | 0.1373 |     576 B |
| EntityCheckingDuplicationBeforeExecuteWithoutDuplication |     10 |      1,524.1 ns |      0.96 ns |      0.80 ns |      - |         - |
| EntityDistinctWithoutDuplication                         |    100 |      6,497.6 ns |     25.06 ns |     23.44 ns | 0.7629 |    3216 B |
| EntityCheckingDuplicationBeforeExecuteWithoutDuplication |    100 |    139,929.9 ns |    464.77 ns |    434.75 ns |      - |         - |
| EntityDistinctWithoutDuplication                         |   1000 |     64,705.8 ns |    344.25 ns |    322.01 ns | 7.2021 |   30336 B |
| EntityCheckingDuplicationBeforeExecuteWithoutDuplication |   1000 | 13,188,575.2 ns | 39,861.39 ns | 35,336.08 ns |      - |      15 B |

| Method                                                   | Length |            Mean |        Error |       StdDev |   Gen0 | Allocated |
|----------------------------------------------------------|------- |----------------:|-------------:|-------------:|-------:|----------:|
| EntityDistinctWithDuplication                            |     10 |        835.5 ns |      3.98 ns |      3.53 ns | 0.1373 |     576 B |
| EntityCheckingDuplicationBeforeExecuteWithDuplication    |     10 |      2,288.1 ns |      6.01 ns |      5.33 ns | 0.1373 |     576 B |
| EntityDistinctWithDuplication                            |    100 |      6,766.1 ns |     18.11 ns |     16.94 ns | 0.7629 |    3216 B |
| EntityCheckingDuplicationBeforeExecuteWithDuplication    |    100 |    110,319.2 ns |    218.18 ns |    170.34 ns | 0.7324 |    3216 B |
| EntityDistinctWithDuplication                            |   1000 |     63,237.7 ns |    346.77 ns |    270.73 ns | 7.2021 |   30336 B |
| EntityCheckingDuplicationBeforeExecuteWithDuplication    |   1000 | 10,056,182.5 ns | 33,335.36 ns | 31,181.92 ns |      - |   30351 B |

| Method                                                                    | Length |           Mean |        Error |       StdDev |   Gen0 | Allocated |
|---------------------------------------------------------------------------|------- |---------------:|-------------:|-------------:|-------:|----------:|
| EntityCheckingDuplicationWithCheckingTypeBeforeExecuteWithoutDuplication  |     10 |       491.3 ns |      1.72 ns |      1.44 ns |      - |         - |
| EntityCheckingDuplicationWithCheckingTypeBeforeExecuteWithDuplication     |     10 |     1,281.8 ns |      9.41 ns |      7.34 ns | 0.1373 |     576 B |
| EntityCheckingDuplicationWithCheckingTypeBeforeExecuteWithoutDuplication  |    100 |    52,492.4 ns |     98.11 ns |     81.93 ns |      - |         - |
| EntityCheckingDuplicationWithCheckingTypeBeforeExecuteWithDuplication     |    100 |    47,984.6 ns |     89.04 ns |     83.28 ns | 0.7324 |    3216 B |
| EntityCheckingDuplicationWithCheckingTypeBeforeExecuteWithoutDuplication  |   1000 | 5,144,514.6 ns | 17,011.43 ns | 14,205.32 ns |      - |       7 B |
| EntityCheckingDuplicationWithCheckingTypeBeforeExecuteWithDuplication     |   1000 | 3,634,657.3 ns |  4,750.06 ns |  4,210.80 ns | 3.9063 |   30340 B |
