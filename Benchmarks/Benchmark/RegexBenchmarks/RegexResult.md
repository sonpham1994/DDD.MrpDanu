| Method                                                 | Length | Mean            | Error        | StdDev        | Median          | Allocated |
|------------------------------------------------------- |--    |----------------:|-------------:|--------------:|----------------:|----------:|
| RegexWithEmailPattern                                  | 10   |    33,319.51 ns |   401.757 ns |    356.147 ns |    33,307.81 ns |         - |
| RegexWithEmailPatternStatic                            | 10   |     1,243.69 ns |     4.286 ns |      3.346 ns |     1,242.77 ns |         - |
| RegexWithEmailPatternForCalculatingMaximumTimeout      | 1    |        88.69 ns |     0.510 ns |      0.452 ns |        88.49 ns |         - |
| RegexWithUniqueCodePatternForCalculatingMaximumTimeout | 1    |     3,732.86 ns |    23.551 ns |     19.666 ns |     3,726.17 ns |         - |
| RegexWithWebsitePatternForCalculatingMaximumTimeout    | 1    |       152.16 ns |     0.986 ns |      0.874 ns |       151.78 ns |         - |
| RegexWithEmailPattern                                  | 100  |   363,746.32 ns | 8,187.282 ns | 22,958.015 ns |   352,742.25 ns |         - |
| RegexWithEmailPatternStatic                            | 100  |    12,407.50 ns |   245.608 ns |    241.219 ns |    12,282.45 ns |         - |
| RegexWithEmailPatternForCalculatingMaximumTimeout      | 1    |        87.28 ns |     0.761 ns |      0.675 ns |        87.04 ns |         - |
| RegexWithUniqueCodePatternForCalculatingMaximumTimeout | 1    |     3,811.78 ns |    21.557 ns |     18.001 ns |     3,810.02 ns |         - |
| RegexWithWebsitePatternForCalculatingMaximumTimeout    | 1    |       134.03 ns |     0.496 ns |      0.440 ns |       133.99 ns |         - |
| RegexWithEmailPattern                                  | 1000 | 3,327,535.10 ns | 7,404.700 ns |  5,781.103 ns | 3,327,714.22 ns |       4 B |
| RegexWithEmailPatternStatic                            | 1000 |   122,011.35 ns |   726.838 ns |    644.323 ns |   121,885.58 ns |         - |
| RegexWithEmailPatternForCalculatingMaximumTimeout      | 1    |        86.98 ns |     1.000 ns |      0.781 ns |        86.83 ns |         - |
| RegexWithUniqueCodePatternForCalculatingMaximumTimeout | 1    |     3,727.29 ns |    19.495 ns |     16.279 ns |     3,722.86 ns |         - |
| RegexWithWebsitePatternForCalculatingMaximumTimeout    | 1    |       154.35 ns |     2.714 ns |      2.406 ns |       152.88 ns |         - |
