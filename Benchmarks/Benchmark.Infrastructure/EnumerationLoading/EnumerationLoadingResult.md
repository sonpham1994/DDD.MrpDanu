|                                              Method |     Mean |     Error |    StdDev |    Gen0 | Allocated |
|---------------------------------------------------- |---------:|----------:|----------:|--------:|----------:|
|                          GetMaterialWithLazyLoading | 4.905 ms | 0.0961 ms | 0.1633 ms | 39.0625 | 127.57 KB |
|      GetMaterialWithoutLazyLoadingAndUseEnumeration | 1.278 ms | 0.0254 ms | 0.0674 ms | 27.3438 |  84.42 KB |
|                     GetMaterialWithLazyLoadingTwice | 9.282 ms | 0.1844 ms | 0.3509 ms | 78.1250 | 254.86 KB |
| GetMaterialWithoutLazyLoadingAndUseEnumerationTwice | 2.593 ms | 0.0518 ms | 0.1082 ms | 54.6875 | 168.85 KB |