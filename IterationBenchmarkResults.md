# Iteration Benchmark Results

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

| **Method**                              | **Runtime** | **N** | **Mean**     | **Error**  | **StdDev** | **Median**   | **Ratio** | **RatioSD** | **Allocated** | **Alloc Ratio** |
|-----------------------------------------|-------------|-------|--------------|------------|------------|--------------|-----------|-------------|---------------|-----------------|
| IterateAsSpan                           | .NET 6.0    | 100   | 30.88 ns     | 0.462 ns   | 0.432 ns   | 30.83 ns     | 1.00      | 0.02        | -             | NA              |
| IterateAsSpan                           | .NET 7.0    | 100   | 26.48 ns     | 0.080 ns   | 0.067 ns   | 26.45 ns     | 0.86      | 0.01        | -             | NA              |
| IterateAsSpan                           | .NET 8.0    | 100   | 29.63 ns     | 0.436 ns   | 0.386 ns   | 29.71 ns     | 0.96      | 0.02        | -             | NA              |
| IterateAsSpan                           | .NET 9.0    | 100   | 27.66 ns     | 0.578 ns   | 1.126 ns   | 27.87 ns     | 0.90      | 0.04        | -             | NA              |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsMutableList                    | .NET 6.0    | 100   | 58.06 ns     | 1.181 ns   | 1.494 ns   | 57.39 ns     | 1.00      | 0.04        | -             | NA              |
| IterateAsMutableList                    | .NET 7.0    | 100   | 42.62 ns     | 0.439 ns   | 0.411 ns   | 42.46 ns     | 0.73      | 0.02        | -             | NA              |
| IterateAsMutableList                    | .NET 8.0    | 100   | 38.02 ns     | 0.706 ns   | 1.218 ns   | 38.26 ns     | 0.66      | 0.03        | -             | NA              |
| IterateAsMutableList                    | .NET 9.0    | 100   | 32.84 ns     | 0.609 ns   | 0.570 ns   | 32.44 ns     | 0.57      | 0.02        | -             | NA              |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsMutableIList                   | .NET 6.0    | 100   | 516.94 ns    | 0.804 ns   | 0.671 ns   | 517.28 ns    | 1.00      | 0.00        | 40 B          | 1.00            |
| IterateAsMutableIList                   | .NET 7.0    | 100   | 517.34 ns    | 1.564 ns   | 1.386 ns   | 516.64 ns    | 1.00      | 0.00        | 40 B          | 1.00            |
| IterateAsMutableIList                   | .NET 8.0    | 100   | 212.00 ns    | 2.890 ns   | 2.703 ns   | 212.69 ns    | 0.41      | 0.01        | 40 B          | 1.00            |
| IterateAsMutableIList                   | .NET 9.0    | 100   | 212.29 ns    | 4.034 ns   | 3.961 ns   | 213.28 ns    | 0.41      | 0.01        | 40 B          | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsMutableICollection             | .NET 6.0    | 100   | 517.10 ns    | 0.526 ns   | 0.466 ns   | 517.24 ns    | 1.00      | 0.00        | 40 B          | 1.00            |
| IterateAsMutableICollection             | .NET 7.0    | 100   | 516.59 ns    | 0.427 ns   | 0.334 ns   | 516.58 ns    | 1.00      | 0.00        | 40 B          | 1.00            |
| IterateAsMutableICollection             | .NET 8.0    | 100   | 217.40 ns    | 3.019 ns   | 2.824 ns   | 218.55 ns    | 0.42      | 0.01        | 40 B          | 1.00            |
| IterateAsMutableICollection             | .NET 9.0    | 100   | 212.53 ns    | 4.165 ns   | 3.896 ns   | 214.06 ns    | 0.41      | 0.01        | 40 B          | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsIEnumerable                    | .NET 6.0    | 100   | 352.66 ns    | 0.916 ns   | 0.812 ns   | 352.40 ns    | 1.00      | 0.00        | -             | NA              |
| IterateAsIEnumerable                    | .NET 7.0    | 100   | 352.38 ns    | 0.881 ns   | 0.781 ns   | 352.28 ns    | 1.00      | 0.00        | -             | NA              |
| IterateAsIEnumerable                    | .NET 8.0    | 100   | 74.97 ns     | 0.308 ns   | 0.288 ns   | 75.03 ns     | 0.21      | 0.00        | -             | NA              |
| IterateAsIEnumerable                    | .NET 9.0    | 100   | 85.81 ns     | 0.540 ns   | 0.505 ns   | 85.73 ns     | 0.24      | 0.00        | -             | NA              |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsIReadOnlyCollection            | .NET 6.0    | 100   | 517.07 ns    | 0.508 ns   | 0.396 ns   | 517.11 ns    | 1.00      | 0.00        | 40 B          | 1.00            |
| IterateAsIReadOnlyCollection            | .NET 7.0    | 100   | 516.47 ns    | 0.345 ns   | 0.288 ns   | 516.39 ns    | 1.00      | 0.00        | 40 B          | 1.00            |
| IterateAsIReadOnlyCollection            | .NET 8.0    | 100   | 217.08 ns    | 3.376 ns   | 3.157 ns   | 216.28 ns    | 0.42      | 0.01        | 40 B          | 1.00            |
| IterateAsIReadOnlyCollection            | .NET 9.0    | 100   | 209.76 ns    | 4.185 ns   | 5.442 ns   | 211.48 ns    | 0.41      | 0.01        | 40 B          | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsIReadOnlyCollection_AsReadOnly | .NET 6.0    | 100   | 521.80 ns    | 0.541 ns   | 0.452 ns   | 521.93 ns    | 1.00      | 0.00        | 64 B          | 1.00            |
| IterateAsIReadOnlyCollection_AsReadOnly | .NET 7.0    | 100   | 520.48 ns    | 0.534 ns   | 0.417 ns   | 520.46 ns    | 1.00      | 0.00        | 64 B          | 1.00            |
| IterateAsIReadOnlyCollection_AsReadOnly | .NET 8.0    | 100   | 221.98 ns    | 4.469 ns   | 5.320 ns   | 222.69 ns    | 0.43      | 0.01        | 64 B          | 1.00            |
| IterateAsIReadOnlyCollection_AsReadOnly | .NET 9.0    | 100   | 216.18 ns    | 2.688 ns   | 2.514 ns   | 215.43 ns    | 0.41      | 0.00        | 64 B          | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsMutableArray                   | .NET 6.0    | 100   | 68.10 ns     | 1.395 ns   | 2.292 ns   | 66.99 ns     | 1.00      | 0.05        | 824 B         | 1.00            |
| IterateAsMutableArray                   | .NET 7.0    | 100   | 57.60 ns     | 1.166 ns   | 2.073 ns   | 56.34 ns     | 0.85      | 0.04        | 824 B         | 1.00            |
| IterateAsMutableArray                   | .NET 8.0    | 100   | 58.88 ns     | 0.086 ns   | 0.076 ns   | 58.86 ns     | 0.87      | 0.03        | 824 B         | 1.00            |
| IterateAsMutableArray                   | .NET 9.0    | 100   | 59.06 ns     | 1.201 ns   | 1.684 ns   | 58.60 ns     | 0.87      | 0.04        | 824 B         | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsReadOnlyArray                  | .NET 6.0    | 100   | 268.93 ns    | 5.250 ns   | 6.639 ns   | 270.83 ns    | 1.00      | 0.04        | 856 B         | 1.00            |
| IterateAsReadOnlyArray                  | .NET 7.0    | 100   | 251.26 ns    | 1.139 ns   | 1.065 ns   | 251.46 ns    | 0.93      | 0.02        | 856 B         | 1.00            |
| IterateAsReadOnlyArray                  | .NET 8.0    | 100   | 98.94 ns     | 0.071 ns   | 0.059 ns   | 98.92 ns     | 0.37      | 0.01        | 856 B         | 1.00            |
| IterateAsReadOnlyArray                  | .NET 9.0    | 100   | 107.29 ns    | 0.292 ns   | 0.273 ns   | 107.16 ns    | 0.40      | 0.01        | 856 B         | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsReadOnlyArray_AsReadOnly       | .NET 6.0    | 100   | 277.73 ns    | 1.510 ns   | 1.412 ns   | 277.58 ns    | 1.00      | 0.01        | 880 B         | 1.00            |
| IterateAsReadOnlyArray_AsReadOnly       | .NET 7.0    | 100   | 262.71 ns    | 4.282 ns   | 4.005 ns   | 261.30 ns    | 0.95      | 0.01        | 880 B         | 1.00            |
| IterateAsReadOnlyArray_AsReadOnly       | .NET 8.0    | 100   | 104.19 ns    | 0.309 ns   | 0.289 ns   | 104.06 ns    | 0.38      | 0.00        | 880 B         | 1.00            |
| IterateAsReadOnlyArray_AsReadOnly       | .NET 9.0    | 100   | 112.12 ns    | 0.252 ns   | 0.236 ns   | 112.06 ns    | 0.40      | 0.00        | 880 B         | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsImmutableList                  | .NET 6.0    | 100   | 2,434.57 ns  | 13.960 ns  | 12.376 ns  | 2,434.38 ns  | 1.00      | 0.01        | 4848 B        | 1.00            |
| IterateAsImmutableList                  | .NET 7.0    | 100   | 1,993.25 ns  | 5.711 ns   | 5.062 ns   | 1,992.12 ns  | 0.82      | 0.00        | 4848 B        | 1.00            |
| IterateAsImmutableList                  | .NET 8.0    | 100   | 1,634.79 ns  | 6.213 ns   | 5.508 ns   | 1,634.34 ns  | 0.67      | 0.00        | 4848 B        | 1.00            |
| IterateAsImmutableList                  | .NET 9.0    | 100   | 1,669.80 ns  | 5.269 ns   | 4.928 ns   | 1,667.73 ns  | 0.69      | 0.00        | 4824 B        | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsImmutableArray                 | .NET 6.0    | 100   | 82.82 ns     | 1.692 ns   | 1.738 ns   | 82.26 ns     | 1.00      | 0.03        | 824 B         | 1.00            |
| IterateAsImmutableArray                 | .NET 7.0    | 100   | 69.61 ns     | 0.332 ns   | 0.311 ns   | 69.67 ns     | 0.84      | 0.02        | 824 B         | 1.00            |
| IterateAsImmutableArray                 | .NET 8.0    | 100   | 65.07 ns     | 0.127 ns   | 0.106 ns   | 65.06 ns     | 0.79      | 0.02        | 824 B         | 1.00            |
| IterateAsImmutableArray                 | .NET 9.0    | 100   | 63.76 ns     | 1.249 ns   | 1.282 ns   | 64.63 ns     | 0.77      | 0.02        | 824 B         | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsList_FromEnumerable            | .NET 6.0    | 100   | 882.1 ns     | 12.69 ns   | 11.87 ns   |              | 1.00      | 0.02        | 2192 B        | 1.00            |
| IterateAsList_FromEnumerable            | .NET 7.0    | 100   | 794.5 ns     | 5.63 ns    | 5.26 ns    |              | 0.90      | 0.01        | 2192 B        | 1.00            |
| IterateAsList_FromEnumerable            | .NET 8.0    | 100   | 543.2 ns     | 2.49 ns    | 2.21 ns    |              | 0.62      | 0.01        | 2192 B        | 1.00            |
| IterateAsList_FromEnumerable            | .NET 9.0    | 100   | 560.0 ns     | 0.90 ns    | 0.75 ns    |              | 0.64      | 0.01        | 2192 B        | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsArray_FromEnumerable           | .NET 6.0    | 100   | 840.8 ns     | 6.34 ns    | 5.93 ns    |              | 1.00      | 0.01        | 2080 B        | 1.00            |
| IterateAsArray_FromEnumerable           | .NET 7.0    | 100   | 816.0 ns     | 5.35 ns    | 5.01 ns    |              | 0.97      | 0.01        | 2080 B        | 1.00            |
| IterateAsArray_FromEnumerable           | .NET 8.0    | 100   | 583.7 ns     | 2.16 ns    | 2.02 ns    |              | 0.69      | 0.01        | 2080 B        | 1.00            |
| IterateAsArray_FromEnumerable           | .NET 9.0    | 100   | 442.7 ns     | 1.00 ns    | 0.89 ns    |              | 0.53      | 0.00        | 824 B         | 0.40            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsSpan                           | .NET 6.0    | 1000  | 253.06 ns    | 0.136 ns   | 0.106 ns   | 253.03 ns    | 1.00      | 0.00        | -             | NA              |
| IterateAsSpan                           | .NET 7.0    | 1000  | 252.96 ns    | 0.166 ns   | 0.139 ns   | 252.96 ns    | 1.00      | 0.00        | -             | NA              |
| IterateAsSpan                           | .NET 8.0    | 1000  | 253.25 ns    | 0.455 ns   | 0.380 ns   | 253.07 ns    | 1.00      | 0.00        | -             | NA              |
| IterateAsSpan                           | .NET 9.0    | 1000  | 252.55 ns    | 0.095 ns   | 0.080 ns   | 252.55 ns    | 1.00      | 0.00        | -             | NA              |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsMutableList                    | .NET 6.0    | 1000  | 544.19 ns    | 1.799 ns   | 1.594 ns   | 544.69 ns    | 1.00      | 0.00        | -             | NA              |
| IterateAsMutableList                    | .NET 7.0    | 1000  | 408.46 ns    | 0.277 ns   | 0.231 ns   | 408.42 ns    | 0.75      | 0.00        | -             | NA              |
| IterateAsMutableList                    | .NET 8.0    | 1000  | 302.80 ns    | 0.215 ns   | 0.180 ns   | 302.82 ns    | 0.56      | 0.00        | -             | NA              |
| IterateAsMutableList                    | .NET 9.0    | 1000  | 281.19 ns    | 3.467 ns   | 3.243 ns   | 280.79 ns    | 0.52      | 0.01        | -             | NA              |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsMutableIList                   | .NET 6.0    | 1000  | 5,053.30 ns  | 5.094 ns   | 3.977 ns   | 5,052.57 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| IterateAsMutableIList                   | .NET 7.0    | 1000  | 5,053.79 ns  | 11.534 ns  | 9.631 ns   | 5,049.86 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| IterateAsMutableIList                   | .NET 8.0    | 1000  | 2,089.76 ns  | 41.717 ns  | 62.440 ns  | 2,113.19 ns  | 0.41      | 0.01        | 40 B          | 1.00            |
| IterateAsMutableIList                   | .NET 9.0    | 1000  | 2,051.52 ns  | 40.683 ns  | 55.688 ns  | 2,065.85 ns  | 0.41      | 0.01        | 40 B          | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsMutableICollection             | .NET 6.0    | 1000  | 5,349.49 ns  | 28.405 ns  | 26.570 ns  | 5,340.14 ns  | 1.00      | 0.01        | 40 B          | 1.00            |
| IterateAsMutableICollection             | .NET 7.0    | 1000  | 5,051.51 ns  | 5.377 ns   | 4.490 ns   | 5,051.74 ns  | 0.94      | 0.00        | 40 B          | 1.00            |
| IterateAsMutableICollection             | .NET 8.0    | 1000  | 2,082.11 ns  | 40.522 ns  | 52.690 ns  | 2,102.62 ns  | 0.39      | 0.01        | 40 B          | 1.00            |
| IterateAsMutableICollection             | .NET 9.0    | 1000  | 2,067.07 ns  | 38.562 ns  | 41.261 ns  | 2,077.67 ns  | 0.39      | 0.01        | 40 B          | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsIEnumerable                    | .NET 6.0    | 1000  | 3,461.86 ns  | 11.190 ns  | 10.467 ns  | 3,455.55 ns  | 1.00      | 0.00        | -             | NA              |
| IterateAsIEnumerable                    | .NET 7.0    | 1000  | 3,459.33 ns  | 9.083 ns   | 7.584 ns   | 3,456.33 ns  | 1.00      | 0.00        | -             | NA              |
| IterateAsIEnumerable                    | .NET 8.0    | 1000  | 712.16 ns    | 0.450 ns   | 0.399 ns   | 712.17 ns    | 0.21      | 0.00        | -             | NA              |
| IterateAsIEnumerable                    | .NET 9.0    | 1000  | 804.53 ns    | 0.487 ns   | 0.380 ns   | 804.59 ns    | 0.23      | 0.00        | -             | NA              |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsIReadOnlyCollection            | .NET 6.0    | 1000  | 5,317.66 ns  | 2.910 ns   | 2.430 ns   | 5,318.48 ns  | 1.00      | 0.00        | 40 B          | 1.00            |
| IterateAsIReadOnlyCollection            | .NET 7.0    | 1000  | 5,062.28 ns  | 13.900 ns  | 12.322 ns  | 5,056.46 ns  | 0.95      | 0.00        | 40 B          | 1.00            |
| IterateAsIReadOnlyCollection            | .NET 8.0    | 1000  | 2,084.06 ns  | 41.413 ns  | 71.436 ns  | 2,112.87 ns  | 0.39      | 0.01        | 40 B          | 1.00            |
| IterateAsIReadOnlyCollection            | .NET 9.0    | 1000  | 2,079.89 ns  | 21.549 ns  | 20.157 ns  | 2,080.81 ns  | 0.39      | 0.00        | 40 B          | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsIReadOnlyCollection_AsReadOnly | .NET 6.0    | 1000  | 5,058.37 ns  | 3.644 ns   | 3.043 ns   | 5,058.30 ns  | 1.00      | 0.00        | 64 B          | 1.00            |
| IterateAsIReadOnlyCollection_AsReadOnly | .NET 7.0    | 1000  | 5,053.69 ns  | 2.345 ns   | 1.958 ns   | 5,053.69 ns  | 1.00      | 0.00        | 64 B          | 1.00            |
| IterateAsIReadOnlyCollection_AsReadOnly | .NET 8.0    | 1000  | 2,084.38 ns  | 41.629 ns  | 67.223 ns  | 2,116.14 ns  | 0.41      | 0.01        | 64 B          | 1.00            |
| IterateAsIReadOnlyCollection_AsReadOnly | .NET 9.0    | 1000  | 2,022.82 ns  | 40.127 ns  | 98.432 ns  | 2,070.55 ns  | 0.40      | 0.02        | 64 B          | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsMutableArray                   | .NET 6.0    | 1000  | 633.11 ns    | 3.479 ns   | 2.905 ns   | 632.94 ns    | 1.00      | 0.01        | 8024 B        | 1.00            |
| IterateAsMutableArray                   | .NET 7.0    | 1000  | 554.04 ns    | 10.862 ns  | 17.541 ns  | 542.54 ns    | 0.88      | 0.03        | 8024 B        | 1.00            |
| IterateAsMutableArray                   | .NET 8.0    | 1000  | 551.26 ns    | 10.777 ns  | 15.108 ns  | 541.08 ns    | 0.87      | 0.02        | 8024 B        | 1.00            |
| IterateAsMutableArray                   | .NET 9.0    | 1000  | 560.91 ns    | 11.252 ns  | 11.555 ns  | 568.70 ns    | 0.89      | 0.02        | 8024 B        | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsReadOnlyArray                  | .NET 6.0    | 1000  | 2,568.00 ns  | 18.399 ns  | 17.210 ns  | 2,569.02 ns  | 1.00      | 0.01        | 8056 B        | 1.00            |
| IterateAsReadOnlyArray                  | .NET 7.0    | 1000  | 2,352.37 ns  | 48.516 ns  | 143.051 ns | 2,237.52 ns  | 0.92      | 0.06        | 8056 B        | 1.00            |
| IterateAsReadOnlyArray                  | .NET 8.0    | 1000  | 932.18 ns    | 1.973 ns   | 1.749 ns   | 931.67 ns    | 0.36      | 0.00        | 8056 B        | 1.00            |
| IterateAsReadOnlyArray                  | .NET 9.0    | 1000  | 994.82 ns    | 1.354 ns   | 1.200 ns   | 995.02 ns    | 0.39      | 0.00        | 8056 B        | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsReadOnlyArray_AsReadOnly       | .NET 6.0    | 1000  | 2,602.39 ns  | 21.141 ns  | 17.654 ns  | 2,601.35 ns  | 1.00      | 0.01        | 8080 B        | 1.00            |
| IterateAsReadOnlyArray_AsReadOnly       | .NET 7.0    | 1000  | 2,400.21 ns  | 17.938 ns  | 15.902 ns  | 2,398.24 ns  | 0.92      | 0.01        | 8080 B        | 1.00            |
| IterateAsReadOnlyArray_AsReadOnly       | .NET 8.0    | 1000  | 932.76 ns    | 2.643 ns   | 2.472 ns   | 931.99 ns    | 0.36      | 0.00        | 8080 B        | 1.00            |
| IterateAsReadOnlyArray_AsReadOnly       | .NET 9.0    | 1000  | 1,002.36 ns  | 1.778 ns   | 1.485 ns   | 1,002.63 ns  | 0.39      | 0.00        | 8080 B        | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsImmutableList                  | .NET 6.0    | 1000  | 25,314.39 ns | 310.091 ns | 290.060 ns | 25,245.90 ns | 1.00      | 0.02        | 48048 B       | 1.00            |
| IterateAsImmutableList                  | .NET 7.0    | 1000  | 20,151.93 ns | 176.362 ns | 156.340 ns | 20,069.56 ns | 0.80      | 0.01        | 48048 B       | 1.00            |
| IterateAsImmutableList                  | .NET 8.0    | 1000  | 15,758.35 ns | 270.583 ns | 253.104 ns | 15,646.83 ns | 0.62      | 0.01        | 48048 B       | 1.00            |
| IterateAsImmutableList                  | .NET 9.0    | 1000  | 16,314.04 ns | 19.442 ns  | 17.235 ns  | 16,313.49 ns | 0.64      | 0.01        | 48024 B       | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsImmutableArray                 | .NET 6.0    | 1000  | 666.71 ns    | 13.268 ns  | 22.168 ns  | 656.16 ns    | 1.00      | 0.05        | 8024 B        | 1.00            |
| IterateAsImmutableArray                 | .NET 7.0    | 1000  | 567.88 ns    | 11.302 ns  | 22.043 ns  | 552.33 ns    | 0.85      | 0.04        | 8024 B        | 1.00            |
| IterateAsImmutableArray                 | .NET 8.0    | 1000  | 557.30 ns    | 11.009 ns  | 16.478 ns  | 546.48 ns    | 0.84      | 0.04        | 8024 B        | 1.00            |
| IterateAsImmutableArray                 | .NET 9.0    | 1000  | 561.36 ns    | 11.084 ns  | 16.247 ns  | 552.05 ns    | 0.84      | 0.04        | 8024 B        | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsList_FromEnumerable            | .NET 6.0    | 1000  | 7,603.1 ns   | 38.32 ns   | 35.84 ns   |              | 1.00      | 0.01        | 16600 B       | 1.00            |
| IterateAsList_FromEnumerable            | .NET 7.0    | 1000  | 6,986.6 ns   | 21.40 ns   | 20.02 ns   |              | 0.92      | 0.00        | 16600 B       | 1.00            |
| IterateAsList_FromEnumerable            | .NET 8.0    | 1000  | 4,497.8 ns   | 7.20 ns    | 6.38 ns    |              | 0.59      | 0.00        | 16600 B       | 1.00            |
| IterateAsList_FromEnumerable            | .NET 9.0    | 1000  | 4,535.4 ns   | 15.91 ns   | 14.88 ns   |              | 0.60      | 0.00        | 16600 B       | 1.00            |
|                                         |             |       |              |            |            |              |           |             |               |                 |
| IterateAsArray_FromEnumerable           | .NET 6.0    | 1000  | 6,961.1 ns   | 27.98 ns   | 23.37 ns   |              | 1.00      | 0.00        | 16608 B       | 1.00            |
| IterateAsArray_FromEnumerable           | .NET 7.0    | 1000  | 6,659.4 ns   | 8.01 ns    | 6.69 ns    |              | 0.96      | 0.00        | 16608 B       | 1.00            |
| IterateAsArray_FromEnumerable           | .NET 8.0    | 1000  | 4,583.1 ns   | 10.59 ns   | 9.91 ns    |              | 0.66      | 0.00        | 16608 B       | 1.00            |
| IterateAsArray_FromEnumerable           | .NET 9.0    | 1000  | 3,783.1 ns   | 11.13 ns   | 9.87 ns    |              | 0.54      | 0.00        | 8024 B        | 0.48            |

## Conclusions

Based on the benchmark results, here are the key findings for collection iteration performance:

### 1. Span<T> - Fastest Overall
* Consistently performs best across .NET versions
* Around 30ns for 100 items and 250ns for 1000 items
* Zero allocations
* Best choice when direct memory access is possible

### 2. List<T> (Mutable List) - Second Best
* Very good performance, especially in newer .NET versions
* Significant improvements from .NET 6 to 9 (43% faster in .NET 9)
* Zero allocations
* Good choice when Span<T> isn't possible

### 3. Array Operations
* Both mutable and immutable arrays perform well
* However, they allocate memory (824B+ depending on size)
* Good for scenarios where allocation isn't critical

### 4. Interface-based Iterations
* IEnumerable<T>:
    * Major improvements in .NET 8/9 (76-79% faster than .NET 6/7)
* IList<T>, ICollection<T>, IReadOnlyCollection<T>:
    * ~60% performance improvement in .NET 8/9
* Interface-based approaches generally allocate some memory (40-64 bytes)

### 5. ImmutableList - Slowest
* 10-20x slower than other approaches
* Highest memory allocation (4.8KB+ depending on size)
* Should be avoided in performance-critical scenarios

### 6. Materializing IEnumerables
* Array is generally faster than List when materializing from IEnumerable
* Memory allocations are similar until .NET 9.0
* .NET 9.0 shows a significant improvement for Array:
   * Reduced memory allocation (60% less)
   * Better performance (21% faster for N=1000)
* Both methods show substantial improvements in newer .NET versions
* The performance gap widens with larger collections

## Recommendations

1. Use Span<T> when possible for best performance
2. List<T> is a good general-purpose choice
3. Avoid ImmutableList for performance-critical loops
4. If using interfaces, prefer newer .NET versions (8+) for significant performance benefits
5. Consider memory allocations if working in a memory-sensitive environment
6. When "materializing" an IEnumerable, ToArray() is generally better than ToList(), especially in .NET 9

The performance differences become more pronounced with larger collections (1000 items vs 100 items), making the choice more important for larger datasets.