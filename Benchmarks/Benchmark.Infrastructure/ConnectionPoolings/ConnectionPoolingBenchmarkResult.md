| Method                                       | Length |           Mean |        Error |        StdDev |      Gen0 |   Allocated |
|----------------------------------------------|------- |---------------:|-------------:|--------------:|----------:|------------:|
| PerformConnectionPooling                     |    100 |       261.4 us |      1.13 us |       1.00 us |   15.6250 |    64.06 KB |
| PerformNoConnectionPooling                   |    100 | 2,344,204.6 us | 46,446.95 us |  60,394.15 us | 1000.0000 |  5907.23 KB |
| PerformConnectionPoolingWithDbContext        |    100 |    21,412.2 us |    244.91 us |     204.51 us | 1562.5000 |  6494.98 KB |
| PerformNoConnectionPoolingWithDbContext      |    100 | 2,334,487.1 us | 19,174.13 us |  17,935.49 us | 3000.0000 | 12387.99 KB |
| PerformQueryConnectionPoolingWithDbContext   |    100 |   267,486.6 us |  3,159.49 us |   2,638.32 us | 1500.0000 |  7373.61 KB |
| PerformQueryNoConnectionPoolingWithDbContext |    100 | 2,576,014.2 us | 28,430.57 us |  26,593.97 us | 3000.0000 | 13261.84 KB |
| PerformQueryConnectionPoolingWithDapper      |    100 |   226,404.0 us |  4,483.23 us |   3,974.27 us |         - |   559.93 KB |
| PerformQueryNoConnectionPoolingWithDapper    |    100 | 2,712,622.2 us | 52,924.11 us | 116,169.70 us | 1000.0000 |  6421.29 KB |