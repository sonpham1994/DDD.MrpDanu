| Method                                                       | Length |       Mean |      Error |     StdDev |    Median |     Gen0 |   Gen1 | Allocated |
|--------------------------------------------------------------|------- |-----------:|-----------:|-----------:|----------:|---------:|-------:|----------:|
| IEnumerableWithToList                                        |   1000 |  117.40 us |   2.286 us |   2.632 us | 116.40 us |  38.3301 | 0.1221 | 117.51 KB |
| IEnumerableWithToArray                                       |   1000 |  101.16 us |   1.986 us |   3.581 us | 100.09 us |  38.3301 |      - | 117.52 KB |
| IEnumerableWithList                                          |   1000 |   85.96 us |   1.669 us |   1.561 us |  86.02 us |  38.3301 |      - | 117.46 KB |
| IEnumerableWithArrayAndFixedLength                           |   1000 |   96.21 us |   5.268 us |  15.116 us |  93.35 us |  35.5225 |      - | 109.09 KB |
| List                                                         |   1000 |  103.96 us |   3.457 us |   9.918 us | 102.00 us |  38.3301 |      - | 117.46 KB |
| ListNotReturn                                                |   1000 |  104.63 us |   3.885 us |  11.146 us | 104.01 us |  38.3301 |      - | 117.46 KB |
| ListWithTrimExcess                                           |   1000 |  100.54 us |   3.446 us |  10.161 us |  98.51 us |  38.3301 |      - | 117.46 KB |
| ListWithDefiningCapacity                                     |   1000 | 55.80 us   | 0.276 us   | 0.245 us   | 26.6724   | 0.0610   | 109.12 KB |
| ListWithExceedingCapacity                                    |   1000 |   55.60 us |   0.279 us |   0.248 us |   30.5176 |   0.0610 | 124.87 KB |
| Array                                                        |   1000 |   80.36 us |   1.862 us |   5.312 us |  78.51 us |  35.5225 |      - | 109.09 KB |
| IReadOnlyCollection                                          |   1000 |   99.90 us |   4.694 us |  13.692 us |  96.52 us |  38.3301 |      - | 117.46 KB |
| IReadOnlyList                                                |   1000 |   85.43 us |   1.162 us |   0.970 us |  85.21 us |  38.3301 |      - | 117.46 KB |
| IReadOnlyCollectionWithToListFromIEnumerable                 |   1000 |  111.53 us |   2.171 us |   5.719 us | 110.66 us |  38.3301 |      - | 117.51 KB |
| IReadOnlyListWithToListFromIEnumerable                       |   1000 |  111.36 us |   2.204 us |   4.975 us | 109.98 us |  38.3301 |      - | 117.51 KB |
| IReadOnlyCollectionWithToArrayFromIEnumerable                |   1000 |  102.45 us |   2.038 us |   3.926 us | 100.82 us |  38.3301 |      - | 117.52 KB |
| IReadOnlyListWithToArrayFromIEnumerable                      |   1000 |  101.20 us |   2.015 us |   3.138 us |  99.70 us |  38.3301 |      - | 117.52 KB |
| IReadOnlyCollectionWithToListFromIEnumerableWithoutDefering  |   1000 |   95.50 us |   3.713 us |  10.593 us |  96.03 us |  40.8936 | 125.33 KB |
| IReadOnlyListWithToListFromIEnumerableWithoutDefering        |   1000 |   92.20 us |   1.785 us |   1.833 us |  91.44 us |  40.8936 | 125.33 KB |
| IReadOnlyCollectionWithToArrayFromIEnumerableWithoutDefering |   1000 |   98.27 us |   3.742 us |  10.616 us |  95.89 us |  40.7715 |  125.3 KB |
| IReadOnlyListWithToArrayFromIEnumerableWithoutDefering       |   1000 |   98.79 us |   4.448 us |  12.400 us |  94.32 us |  40.7715 |  125.3 KB |
| IReadOnlyCollectionWithTrimExcess                            |   1000 |   86.15 us |   1.581 us |   3.300 us |  84.40 us |  38.3301 |      - | 117.46 KB |
| IReadOnlyListWithTrimExcess                                  |   1000 |   83.01 us |   0.866 us |   0.723 us |  83.11 us |  38.3301 |      - | 117.46 KB |
| IReadOnlyCollectionWithArrayAndFixedLength                   |   1000 |   76.99 us |   1.467 us |   2.196 us |  76.04 us |  35.5225 |      - | 109.09 KB |
| IReadOnlyListWithArrayAndFixedLength                         |   1000 |   71.70 us |  11.571 us |  34.116 us |  77.69 us |  35.5835 |      - | 109.09 KB |
| IReadOnlyCollectionWithToArray                               |   1000 |  132.84 us |   7.140 us |  21.052 us | 134.50 us |  40.7715 |      - |  125.3 KB |
| IReadOnlyListWithToArray                                     |   1000 |  102.00 us |   2.336 us |   6.589 us | 100.65 us |  40.7715 |      - |  125.3 KB |

### Explanation
- The reason why IReadOnlyCollection method outperform IReadOnlyCollectionWithToArray in terms of performance because
IReadOnlyCollection just return a List, whereas IReadOnlyCollectionWithToArray convert from List to Array
