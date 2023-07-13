| Method                                           | Length |          Mean |      Error |     StdDev |        Median |    Gen0 |   Gen1 | Allocated |
|--------------------------------------------------|------- |--------------:|-----------:|-----------:|--------------:|--------:|-------:|----------:|
| GuidGetHashCodeWithNameAndId                     |     10 |     148.82 ns |   0.899 ns |   0.841 ns |     148.57 ns |  0.0515 |      - |     216 B |
| GuidGetHashCodeWithToStringAndId                 |     10 |     229.40 ns |   1.045 ns |   0.926 ns |     229.10 ns |  0.0782 |      - |     328 B |
| GuidGetHashCodeWithNameHashCodeAndIdHashCode     |     10 |      23.94 ns |   0.107 ns |   0.095 ns |      23.90 ns |       - |      - |         - |
| GuidGetListHashCodeWithNameAndId                 |     10 |   1,485.49 ns |   3.645 ns |   3.409 ns |   1,486.20 ns |  0.5150 |      - |    2160 B |
| GuidGetListHashCodeWithNameHashCodeAndIdHashCode |     10 |     267.36 ns |   0.698 ns |   0.583 ns |     267.39 ns |       - |      - |         - |
| GuidGetHashCodeWithNameAndId                     |    100 |     151.77 ns |   2.067 ns |   1.934 ns |     151.54 ns |  0.0515 |      - |     216 B |
| GuidGetHashCodeWithToStringAndId                 |    100 |     229.69 ns |   0.671 ns |   0.595 ns |     229.58 ns |  0.0782 |      - |     328 B |
| GuidGetHashCodeWithNameHashCodeAndIdHashCode     |    100 |      31.43 ns |   2.071 ns |   6.106 ns |      35.85 ns |       - |      - |         - |
| GuidGetListHashCodeWithNameAndId                 |    100 |  15,260.82 ns |  55.959 ns |  46.728 ns |  15,262.57 ns |  5.1575 |      - |   21601 B |
| GuidGetListHashCodeWithNameHashCodeAndIdHashCode |    100 |   3,395.31 ns |   9.378 ns |   8.313 ns |   3,392.56 ns |       - |      - |         - |
| GuidGetHashCodeWithNameAndId                     |   1000 |     146.68 ns |   0.483 ns |   0.452 ns |     146.60 ns |  0.0515 |      - |     216 B |
| GuidGetHashCodeWithToStringAndId                 |   1000 |     222.69 ns |   3.430 ns |   3.040 ns |     221.85 ns |  0.0782 | 0.0002 |     328 B |
| GuidGetHashCodeWithNameHashCodeAndIdHashCode     |   1000 |      24.29 ns |   0.492 ns |   0.656 ns |      23.95 ns |       - |      - |         - |
| GuidGetListHashCodeWithNameAndId                 |   1000 | 276,172.38 ns | 928.982 ns | 725.288 ns | 275,985.61 ns | 51.2695 |      - |  216011 B |
| GuidGetListHashCodeWithNameHashCodeAndIdHashCode |   1000 |  26,237.21 ns |  98.095 ns |  86.959 ns |  26,213.10 ns |       - |      - |         - |


| Method                                          | Length |         Mean |      Error |     StdDev |    Gen0 | Allocated |
|-------------------------------------------------|------- |-------------:|-----------:|-----------:|--------:|----------:|
| IntGetHashCodeWithNameAndId                     |     10 |     44.60 ns |   0.306 ns |   0.286 ns |  0.0114 |      48 B |
| IntGetHashCodeWithToStringAndId                 |     10 |    124.00 ns |   0.833 ns |   0.739 ns |  0.0381 |     160 B |
| IntGetHashCodeWithNameHashCodeAndIdHashCode     |     10 |     22.93 ns |   0.480 ns |   0.401 ns |       - |         - |
| IntGetListHashCodeWithNameAndId                 |     10 |    459.72 ns |   2.496 ns |   2.084 ns |  0.1221 |     512 B |
| IntGetListHashCodeWithNameHashCodeAndIdHashCode |     10 |    295.10 ns |   0.476 ns |   0.371 ns |       - |         - |
| IntGetHashCodeWithNameAndId                     |    100 |     44.53 ns |   0.891 ns |   1.190 ns |  0.0114 |      48 B |
| IntGetHashCodeWithToStringAndId                 |    100 |    118.94 ns |   1.260 ns |   1.401 ns |  0.0381 |     160 B |
| IntGetHashCodeWithNameHashCodeAndIdHashCode     |    100 |     23.08 ns |   0.156 ns |   0.130 ns |       - |         - |
| IntGetListHashCodeWithNameAndId                 |    100 |  5,663.27 ns | 113.299 ns | 100.436 ns |  1.8387 |    7712 B |
| IntGetListHashCodeWithNameHashCodeAndIdHashCode |    100 |  2,466.83 ns |   5.095 ns |   4.255 ns |       - |         - |
| IntGetHashCodeWithNameAndId                     |   1000 |     42.95 ns |   0.205 ns |   0.171 ns |  0.0114 |      48 B |
| IntGetHashCodeWithToStringAndId                 |   1000 |    117.27 ns |   0.532 ns |   0.415 ns |  0.0381 |     160 B |
| IntGetHashCodeWithNameHashCodeAndIdHashCode     |   1000 |     22.79 ns |   0.032 ns |   0.028 ns |       - |         - |
| IntGetListHashCodeWithNameAndId                 |   1000 | 58,624.40 ns | 264.873 ns | 247.762 ns | 19.0430 |   79724 B |
| IntGetListHashCodeWithNameHashCodeAndIdHashCode |   1000 | 25,108.00 ns |  93.304 ns |  82.712 ns |       - |         - |
