# LINQ Benchmark Results

.NET 6.0 was used as the baseline in all tests.

```
BenchmarkDotNet v0.14.0, macOS Sequoia 15.3.2 (24D81) [Darwin 24.3.0]
Apple M3 Pro, 1 CPU, 12 logical and 12 physical cores
.NET SDK 9.0.203
  [Host]   : .NET 9.0.4 (9.0.425.16305), Arm64 RyuJIT AdvSIMD
  .NET 6.0 : .NET 6.0.36 (6.0.3624.51421), Arm64 RyuJIT AdvSIMD
  .NET 7.0 : .NET 7.0.20 (7.0.2024.26716), Arm64 RyuJIT AdvSIMD
  .NET 8.0 : .NET 8.0.15 (8.0.1525.16413), Arm64 RyuJIT AdvSIMD
  .NET 9.0 : .NET 9.0.4 (9.0.425.16305), Arm64 RyuJIT AdvSIMD
```

| **Method**                                | **Runtime** | **N** | **Mean**     | **Error**  | **StdDev** | **Median**   | **Ratio** | **RatioSD** | **Allocated** | **Alloc Ratio** |
|-------------------------------------------|-------------|-------|--------------|------------|------------|--------------|-----------|-------------|---------------|-----------------|
| AggregateAsMutableList                    | .NET 6.0    | 100   | 452.85 ns    | 2.172 ns   | 1.696 ns   | 452.36 ns    | 1.00      | 0.01        | 40 B          | 1.00            |
| AggregateAsMutableList                    | .NET 7.0    | 100   | 463.02 ns    | 0.488 ns   | 0.408 ns   | 462.88 ns    | 1.02      | 0.00        | 40 B          | 1.00            |
| AggregateAsMutableList                    | .NET 8.0    | 100   | 163.22 ns    | 3.266 ns   | 8.940 ns   | 162.59 ns    | 0.36      | 0.02        | 40 B          | 1.00            |
| AggregateAsMutableList                    | .NET 9.0    | 100   | 27.98 ns     | 0.591 ns   | 0.867 ns   | 27.47 ns     | 0.06      | 0.00        | -             | 0.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsMutableIList                   | .NET 6.0    | 100   | 459.48 ns    | 4.080 ns   | 3.816 ns   | 458.69 ns    | 1.00      | 0.01        | 40 B          | 1.00            |
| AggregateAsMutableIList                   | .NET 7.0    | 100   | 463.39 ns    | 1.014 ns   | 0.948 ns   | 462.85 ns    | 1.01      | 0.01        | 40 B          | 1.00            |
| AggregateAsMutableIList                   | .NET 8.0    | 100   | 164.16 ns    | 3.313 ns   | 9.290 ns   | 166.16 ns    | 0.36      | 0.02        | 40 B          | 1.00            |
| AggregateAsMutableIList                   | .NET 9.0    | 100   | 29.10 ns     | 0.600 ns   | 1.097 ns   | 29.10 ns     | 0.06      | 0.00        | -             | 0.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsMutableICollection             | .NET 6.0    | 100   | 462.44 ns    | 2.258 ns   | 2.112 ns   | 462.30 ns    | 1.00      | 0.01        | 40 B          | 1.00            |
| AggregateAsMutableICollection             | .NET 7.0    | 100   | 463.57 ns    | 0.156 ns   | 0.122 ns   | 463.57 ns    | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsMutableICollection             | .NET 8.0    | 100   | 162.59 ns    | 3.391 ns   | 9.997 ns   | 161.88 ns    | 0.35      | 0.02        | 40 B          | 1.00            |
| AggregateAsMutableICollection             | .NET 9.0    | 100   | 28.74 ns     | 0.593 ns   | 1.142 ns   | 28.45 ns     | 0.06      | 0.00        | -             | 0.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsIEnumerable                    | .NET 6.0    | 100   | 326.68 ns    | 0.861 ns   | 0.719 ns   | 326.49 ns    | 1.00      | 0.00        | -             | NA              |
| AggregateAsIEnumerable                    | .NET 7.0    | 100   | 325.62 ns    | 0.383 ns   | 0.299 ns   | 325.52 ns    | 1.00      | 0.00        | -             | NA              |
| AggregateAsIEnumerable                    | .NET 8.0    | 100   | 87.75 ns     | 0.172 ns   | 0.153 ns   | 87.76 ns     | 0.27      | 0.00        | -             | NA              |
| AggregateAsIEnumerable                    | .NET 9.0    | 100   | 99.32 ns     | 0.590 ns   | 0.523 ns   | 99.37 ns     | 0.30      | 0.00        | -             | NA              |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsIReadOnlyCollection            | .NET 6.0    | 100   | 463.39 ns    | 0.550 ns   | 0.459 ns   | 463.34 ns    | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsIReadOnlyCollection            | .NET 7.0    | 100   | 464.08 ns    | 1.283 ns   | 1.200 ns   | 463.39 ns    | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsIReadOnlyCollection            | .NET 8.0    | 100   | 164.27 ns    | 3.400 ns   | 10.025 ns  | 165.17 ns    | 0.35      | 0.02        | 40 B          | 1.00            |
| AggregateAsIReadOnlyCollection            | .NET 9.0    | 100   | 28.83 ns     | 0.596 ns   | 0.892 ns   | 28.86 ns     | 0.06      | 0.00        | -             | 0.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsIReadOnlyCollection_AsReadOnly | .NET 6.0    | 100   | 467.90 ns    | 0.430 ns   | 0.335 ns   | 467.88 ns    | 1.00      | 0.00        | 64 B          | 1.00            |
| AggregateAsIReadOnlyCollection_AsReadOnly | .NET 7.0    | 100   | 467.00 ns    | 0.433 ns   | 0.361 ns   | 466.89 ns    | 1.00      | 0.00        | 64 B          | 1.00            |
| AggregateAsIReadOnlyCollection_AsReadOnly | .NET 8.0    | 100   | 168.56 ns    | 5.606 ns   | 16.530 ns  | 163.84 ns    | 0.36      | 0.04        | 64 B          | 1.00            |
| AggregateAsIReadOnlyCollection_AsReadOnly | .NET 9.0    | 100   | 160.91 ns    | 3.106 ns   | 6.947 ns   | 161.05 ns    | 0.34      | 0.01        | 64 B          | 1.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsMutableArray                   | .NET 6.0    | 100   | 346.91 ns    | 0.964 ns   | 0.855 ns   | 346.99 ns    | 1.00      | 0.00        | 856 B         | 1.00            |
| AggregateAsMutableArray                   | .NET 7.0    | 100   | 335.05 ns    | 1.065 ns   | 0.944 ns   | 334.63 ns    | 0.97      | 0.00        | 856 B         | 1.00            |
| AggregateAsMutableArray                   | .NET 8.0    | 100   | 99.51 ns     | 0.157 ns   | 0.122 ns   | 99.51 ns     | 0.29      | 0.00        | 856 B         | 1.00            |
| AggregateAsMutableArray                   | .NET 9.0    | 100   | 57.30 ns     | 0.351 ns   | 0.274 ns   | 57.33 ns     | 0.17      | 0.00        | 824 B         | 0.96            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsReadOnlyArray                  | .NET 6.0    | 100   | 346.88 ns    | 0.744 ns   | 0.660 ns   | 346.83 ns    | 1.00      | 0.00        | 856 B         | 1.00            |
| AggregateAsReadOnlyArray                  | .NET 7.0    | 100   | 334.45 ns    | 0.234 ns   | 0.196 ns   | 334.40 ns    | 0.96      | 0.00        | 856 B         | 1.00            |
| AggregateAsReadOnlyArray                  | .NET 8.0    | 100   | 99.95 ns     | 0.292 ns   | 0.228 ns   | 99.94 ns     | 0.29      | 0.00        | 856 B         | 1.00            |
| AggregateAsReadOnlyArray                  | .NET 9.0    | 100   | 56.74 ns     | 0.407 ns   | 0.318 ns   | 56.69 ns     | 0.16      | 0.00        | 824 B         | 0.96            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsReadOnlyArray_AsReadOnly       | .NET 6.0    | 100   | 351.04 ns    | 0.552 ns   | 0.489 ns   | 351.00 ns    | 1.00      | 0.00        | 880 B         | 1.00            |
| AggregateAsReadOnlyArray_AsReadOnly       | .NET 7.0    | 100   | 337.87 ns    | 0.906 ns   | 0.803 ns   | 337.45 ns    | 0.96      | 0.00        | 880 B         | 1.00            |
| AggregateAsReadOnlyArray_AsReadOnly       | .NET 8.0    | 100   | 112.00 ns    | 6.329 ns   | 17.537 ns  | 103.39 ns    | 0.32      | 0.05        | 880 B         | 1.00            |
| AggregateAsReadOnlyArray_AsReadOnly       | .NET 9.0    | 100   | 102.09 ns    | 0.291 ns   | 0.243 ns   | 102.02 ns    | 0.29      | 0.00        | 880 B         | 1.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsImmutableList                  | .NET 6.0    | 100   | 2,624.92 ns  | 12.093 ns  | 11.312 ns  | 2,621.53 ns  | 1.00      | 0.01        | 4920 B        | 1.00            |
| AggregateAsImmutableList                  | .NET 7.0    | 100   | 2,389.11 ns  | 11.574 ns  | 9.665 ns   | 2,388.14 ns  | 0.91      | 0.01        | 4920 B        | 1.00            |
| AggregateAsImmutableList                  | .NET 8.0    | 100   | 1,376.38 ns  | 8.904 ns   | 8.329 ns   | 1,371.92 ns  | 0.52      | 0.00        | 4920 B        | 1.00            |
| AggregateAsImmutableList                  | .NET 9.0    | 100   | 1,406.24 ns  | 10.699 ns  | 10.008 ns  | 1,401.75 ns  | 0.54      | 0.00        | 4920 B        | 1.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsImmutableArray                 | .NET 6.0    | 100   | 169.38 ns    | 0.475 ns   | 0.444 ns   | 169.44 ns    | 1.00      | 0.00        | 824 B         | 1.00            |
| AggregateAsImmutableArray                 | .NET 7.0    | 100   | 158.39 ns    | 1.561 ns   | 1.460 ns   | 157.82 ns    | 0.94      | 0.01        | 824 B         | 1.00            |
| AggregateAsImmutableArray                 | .NET 8.0    | 100   | 57.73 ns     | 0.377 ns   | 0.353 ns   | 57.68 ns     | 0.34      | 0.00        | 824 B         | 1.00            |
| AggregateAsImmutableArray                 | .NET 9.0    | 100   | 63.11 ns     | 0.563 ns   | 0.526 ns   | 63.31 ns     | 0.37      | 0.00        | 824 B         | 1.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsMutableList                    | .NET 6.0    | 1000  | 4,520.72 ns  | 3.905 ns   | 3.261 ns   | 4,520.41 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsMutableList                    | .NET 7.0    | 1000  | 4,524.02 ns  | 4.589 ns   | 4.068 ns   | 4,525.06 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsMutableList                    | .NET 8.0    | 1000  | 1,763.34 ns  | 99.437 ns  | 293.192 ns | 1,951.83 ns  | 0.39      | 0.06        | 40 B          | 1.00            |
| AggregateAsMutableList                    | .NET 9.0    | 1000  | 254.23 ns    | 0.190 ns   | 0.148 ns   | 254.23 ns    | 0.06      | 0.00        | -             | 0.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsMutableIList                   | .NET 6.0    | 1000  | 4,523.04 ns  | 7.415 ns   | 5.789 ns   | 4,520.09 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsMutableIList                   | .NET 7.0    | 1000  | 4,518.54 ns  | 1.354 ns   | 1.131 ns   | 4,518.48 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsMutableIList                   | .NET 8.0    | 1000  | 1,724.43 ns  | 109.463 ns | 322.753 ns | 1,925.41 ns  | 0.38      | 0.07        | 40 B          | 1.00            |
| AggregateAsMutableIList                   | .NET 9.0    | 1000  | 254.49 ns    | 0.831 ns   | 0.694 ns   | 254.32 ns    | 0.06      | 0.00        | -             | 0.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsMutableICollection             | .NET 6.0    | 1000  | 4,529.69 ns  | 21.707 ns  | 18.127 ns  | 4,520.49 ns  | 1.00      | 0.01        | 40 B          | 1.00            |
| AggregateAsMutableICollection             | .NET 7.0    | 1000  | 4,518.13 ns  | 0.668 ns   | 0.592 ns   | 4,518.14 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsMutableICollection             | .NET 8.0    | 1000  | 1,849.86 ns  | 59.266 ns  | 174.746 ns | 1,896.47 ns  | 0.41      | 0.04        | 40 B          | 1.00            |
| AggregateAsMutableICollection             | .NET 9.0    | 1000  | 259.53 ns    | 5.102 ns   | 8.092 ns   | 254.21 ns    | 0.06      | 0.00        | -             | 0.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsIEnumerable                    | .NET 6.0    | 1000  | 3,196.93 ns  | 1.179 ns   | 0.920 ns   | 3,196.91 ns  | 1.00      | 0.00        | -             | NA              |
| AggregateAsIEnumerable                    | .NET 7.0    | 1000  | 3,452.88 ns  | 3.056 ns   | 2.386 ns   | 3,451.95 ns  | 1.08      | 0.00        | -             | NA              |
| AggregateAsIEnumerable                    | .NET 8.0    | 1000  | 815.89 ns    | 0.668 ns   | 0.522 ns   | 815.88 ns    | 0.26      | 0.00        | -             | NA              |
| AggregateAsIEnumerable                    | .NET 9.0    | 1000  | 964.20 ns    | 19.094 ns  | 17.861 ns  | 969.79 ns    | 0.30      | 0.01        | -             | NA              |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsIReadOnlyCollection            | .NET 6.0    | 1000  | 4,529.48 ns  | 4.489 ns   | 3.748 ns   | 4,528.83 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsIReadOnlyCollection            | .NET 7.0    | 1000  | 4,523.27 ns  | 4.012 ns   | 3.350 ns   | 4,523.61 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| AggregateAsIReadOnlyCollection            | .NET 8.0    | 1000  | 1,726.86 ns  | 79.705 ns  | 235.011 ns | 1,795.86 ns  | 0.38      | 0.05        | 40 B          | 1.00            |
| AggregateAsIReadOnlyCollection            | .NET 9.0    | 1000  | 254.61 ns    | 1.021 ns   | 0.797 ns   | 254.26 ns    | 0.06      | 0.00        | -             | 0.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsIReadOnlyCollection_AsReadOnly | .NET 6.0    | 1000  | 4,526.99 ns  | 4.135 ns   | 3.666 ns   | 4,526.38 ns  | 1.00      | 0.00        | 64 B          | 1.00            |
| AggregateAsIReadOnlyCollection_AsReadOnly | .NET 7.0    | 1000  | 4,522.49 ns  | 1.956 ns   | 1.527 ns   | 4,522.01 ns  | 1.00      | 0.00        | 64 B          | 1.00            |
| AggregateAsIReadOnlyCollection_AsReadOnly | .NET 8.0    | 1000  | 1,674.94 ns  | 71.815 ns  | 211.748 ns | 1,692.52 ns  | 0.37      | 0.05        | 64 B          | 1.00            |
| AggregateAsIReadOnlyCollection_AsReadOnly | .NET 9.0    | 1000  | 1,653.63 ns  | 68.872 ns  | 203.072 ns | 1,620.65 ns  | 0.37      | 0.04        | 64 B          | 1.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsMutableArray                   | .NET 6.0    | 1000  | 3,386.23 ns  | 5.077 ns   | 4.240 ns   | 3,384.92 ns  | 1.00      | 0.00        | 8056 B        | 1.00            |
| AggregateAsMutableArray                   | .NET 7.0    | 1000  | 3,276.08 ns  | 11.551 ns  | 9.018 ns   | 3,271.85 ns  | 0.97      | 0.00        | 8056 B        | 1.00            |
| AggregateAsMutableArray                   | .NET 8.0    | 1000  | 968.57 ns    | 1.887 ns   | 1.576 ns   | 968.44 ns    | 0.29      | 0.00        | 8056 B        | 1.00            |
| AggregateAsMutableArray                   | .NET 9.0    | 1000  | 554.45 ns    | 10.871 ns  | 15.240 ns  | 546.32 ns    | 0.16      | 0.00        | 8024 B        | 1.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsReadOnlyArray                  | .NET 6.0    | 1000  | 3,382.43 ns  | 4.389 ns   | 3.891 ns   | 3,382.11 ns  | 1.00      | 0.00        | 8056 B        | 1.00            |
| AggregateAsReadOnlyArray                  | .NET 7.0    | 1000  | 3,263.40 ns  | 2.799 ns   | 2.185 ns   | 3,263.14 ns  | 0.96      | 0.00        | 8056 B        | 1.00            |
| AggregateAsReadOnlyArray                  | .NET 8.0    | 1000  | 971.81 ns    | 1.827 ns   | 1.427 ns   | 971.77 ns    | 0.29      | 0.00        | 8056 B        | 1.00            |
| AggregateAsReadOnlyArray                  | .NET 9.0    | 1000  | 556.59 ns    | 11.093 ns  | 16.940 ns  | 549.45 ns    | 0.16      | 0.00        | 8024 B        | 1.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsReadOnlyArray_AsReadOnly       | .NET 6.0    | 1000  | 3,388.48 ns  | 2.327 ns   | 1.943 ns   | 3,388.99 ns  | 1.00      | 0.00        | 8080 B        | 1.00            |
| AggregateAsReadOnlyArray_AsReadOnly       | .NET 7.0    | 1000  | 3,277.36 ns  | 3.067 ns   | 2.561 ns   | 3,277.84 ns  | 0.97      | 0.00        | 8080 B        | 1.00            |
| AggregateAsReadOnlyArray_AsReadOnly       | .NET 8.0    | 1000  | 974.36 ns    | 2.158 ns   | 1.802 ns   | 974.20 ns    | 0.29      | 0.00        | 8080 B        | 1.00            |
| AggregateAsReadOnlyArray_AsReadOnly       | .NET 9.0    | 1000  | 869.18 ns    | 1.589 ns   | 1.327 ns   | 869.53 ns    | 0.26      | 0.00        | 8080 B        | 1.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsImmutableList                  | .NET 6.0    | 1000  | 26,100.84 ns | 174.427 ns | 163.159 ns | 26,070.60 ns | 1.00      | 0.01        | 48120 B       | 1.00            |
| AggregateAsImmutableList                  | .NET 7.0    | 1000  | 23,276.77 ns | 182.081 ns | 170.319 ns | 23,257.61 ns | 0.89      | 0.01        | 48120 B       | 1.00            |
| AggregateAsImmutableList                  | .NET 8.0    | 1000  | 12,396.56 ns | 17.537 ns  | 14.645 ns  | 12,400.90 ns | 0.47      | 0.00        | 48120 B       | 1.00            |
| AggregateAsImmutableList                  | .NET 9.0    | 1000  | 13,079.90 ns | 11.012 ns  | 9.196 ns   | 13,080.86 ns | 0.50      | 0.00        | 48120 B       | 1.00            |
|                                           |             |       |              |            |            |              |           |             |               |                 |
| AggregateAsImmutableArray                 | .NET 6.0    | 1000  | 1,633.38 ns  | 7.648 ns   | 6.780 ns   | 1,632.44 ns  | 1.00      | 0.01        | 8024 B        | 1.00            |
| AggregateAsImmutableArray                 | .NET 7.0    | 1000  | 1,609.19 ns  | 6.440 ns   | 5.709 ns   | 1,610.60 ns  | 0.99      | 0.01        | 8024 B        | 1.00            |
| AggregateAsImmutableArray                 | .NET 8.0    | 1000  | 578.07 ns    | 3.391 ns   | 3.172 ns   | 576.59 ns    | 0.35      | 0.00        | 8024 B        | 1.00            |
| AggregateAsImmutableArray                 | .NET 9.0    | 1000  | 557.82 ns    | 3.711 ns   | 2.898 ns   | 557.65 ns    | 0.34      | 0.00        | 8024 B        | 1.00            |

## Conclusions

Based on the benchmark results, here are the key conclusions:

1. Runtime Evolution:
- .NET 8.0 and 9.0 show significant performance improvements over .NET 6.0 and 7.0 across all tests
- The most dramatic improvements are seen in .NET 9.0, particularly for mutable collection operations

2. Best Performing Approaches:
- For N=100 and N=1000, the fastest operations are:
  - `AggregateAsMutableList`, `AggregateAsMutableIList`, `AggregateAsMutableICollection`, and `AggregateAsIReadOnlyCollection` in .NET 9.0 (~28-29ns for N=100, ~254-260ns for N=1000)
  - These methods also have zero allocations in .NET 9.0

3. Collection Type Performance:
- Mutable collections (List, IList, ICollection) perform best overall
- IEnumerable operations are moderately performant but not the fastest
- Array-based operations are generally faster than list-based operations
- ImmutableList performs significantly worse than other collections (~1,400ns for N=100, ~13,000ns for N=1000 even in .NET 9.0)

4. Memory Allocations:
- Most collection operations allocate either 40B or 64B for small operations
- Array-based operations have larger allocations (824B-880B for N=100, 8024B-8080B for N=1000)
- ImmutableList has the highest memory allocation (4920B for N=100, 48120B for N=1000)

Recommendations:
1. Use mutable collections (List<T>, IList<T>, ICollection<T>) when possible, especially in .NET 9.0
2. Avoid ImmutableList for performance-critical operations
3. If immutability is required, prefer ImmutableArray over ImmutableList
4. Upgrade to .NET 8.0 or 9.0 for significant performance benefits
5. Consider array-based operations when memory allocation is less critical than raw performance

The most efficient overall approach would be using mutable collections in .NET 9.0, which provides the best performance and zero allocations for many operations.