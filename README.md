# C# Collection Benchmarks

One of the most fundamental questions developers must ask themselves repeatedly when coding is: which data structure should I use? However, more subtle questions that are just as important include:
* Which _type_ should I use for that data structure?
* Do I need to create defensive copies for safety?
* What's the difference between read-only collections and immutable collections, and which should I use?
* Do my answers change if I'm dealing with concurrent access or modification?

An extremely common scenario in many programs is to create a collection of items as an output of some operation and then pass that collection off to some other routine for consumption. In this situation, creating/mutating the collection is done once, and all consumption of the collection after that point is read-only.  For example, maybe you retrieve items from a DB, filter some of them down to a smaller set, transform them into JSON, and then return that JSON to an application.  In C#, this could be done in a couple ways:

```
var dbItems = db.GetItems();
var filteredItems = filter(dbItems);
var asJson = convertToJson(filteredItems);
return asJson;
```

Or, more simply, using LINQ: `return convertToJson(db.GetItems().Where(filter));`

What type should `db.GetItems()` return? What types should `filter()` accept and return? How about `convertToJson()`? The answers to those questions can have varying impacts to safety and performance that are often not clear. Additionally, those coming from a Java background may be surprised to learn how things work in C#, where the best answer in Java is _not_ the best answer in C#.

This project contains microbenchmarks that are meant to help answer these questions.

#### For Those Coming from Java

As an aside, before getting to the C# benchmarks below, a quick note about the difference between the .NET immutable collections and the Guava immutable collections often used in Java development, since these are quite different. The Guava immutable collections _disable_ mutation and are meant for very fast and efficient consumption, whether single-threaded or multi-threaded. The .NET immutable collections, however, _enable_ mutation but do so in such a way that the modification is efficiently returned as a separate collection instance. This difference makes the Guava immutable collections appropriate for **consumption but not production**, while the .NET immutable collections are appropriate for **thread-safe production but not necessarily efficient consumption**.

## Benchmarks

All benchmarks in this project use [BenchmarkDotNet](https://benchmarkdotnet.org/) and were executed on a MacBook Pro 2019 16-inch model, plugged in, with no activities other than the benchmarks.

---------------------------

### Iterate list

Arguably the most common pattern you'll find in a program is to create a list of items in a single thread and then iterate over those unchanging items. The included `IterationBenchy` tests the performance and memory characteristics of various collection types when used in this pattern for both a list of 100 items and a list of 1000 items.

#### Benchmark Results

BenchmarkDotNet v0.13.12, macOS Sonoma 14.4.1 (23E224) [Darwin 23.4.0]

Intel Core i9-9980HK CPU 2.40GHz, 1 CPU, 16 logical and 8 physical cores

.NET SDK=8.0.101<br>
  Job-VFPNOY : .NET Core 3.1.20 (CoreCLR 4.700.21.47003, CoreFX 4.700.21.47101), X64 RyuJIT AVX2<br>
  Job-VZMYMY : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT AVX2<br>
  Job-OVRMSJ : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2<br>
  Job-FDUCSD : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2<br>
  Job-HTCTKA : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2 

|                                           Method |       Runtime |    N |         Mean |        Error |       StdDev | Allocated |
|-------------------------------------------------:|--------------:|-----:|-------------:|-------------:|-------------:|----------:|
|                                    IterateAsSpan | .NET Core 3.1 |  N/A |          N/A |          N/A |          N/A |         - |
|                                    IterateAsSpan |      .NET 5.0 |  N/A |          N/A |          N/A |          N/A |         - |
|                                    IterateAsSpan |      .NET 6.0 |  100 |     33.11 ns |     0.674 ns |     0.776 ns |         - |
|                                    IterateAsSpan |      .NET 7.0 |  100 |     32.30 ns |     0.652 ns |     0.610 ns |         - |
|                                    IterateAsSpan |      .NET 8.0 |  100 |     31.98 ns |     0.665 ns |     0.932 ns |         - |
|                                                  |               |      |              |              |              |           |
|                             IterateAsMutableList | .NET Core 3.1 |  100 |     333.9 ns |      2.61 ns |      2.44 ns |         - |
|                             IterateAsMutableList |      .NET 5.0 |  100 |    305.71 ns |     6.064 ns |     6.227 ns |         - |
|                             IterateAsMutableList |      .NET 6.0 |  100 |     97.04 ns |     1.873 ns |     2.004 ns |         - |
|                             IterateAsMutableList |      .NET 7.0 |  100 |     79.60 ns |     1.551 ns |     1.787 ns |         - |
|                             IterateAsMutableList |      .NET 8.0 |  100 |     54.62 ns |     0.796 ns |     0.706 ns |         - |
|                                                  |               |      |              |              |              |           |
|                            IterateAsMutableIList | .NET Core 3.1 |  100 |     880.0 ns |      4.86 ns |      3.79 ns |      40 B |
|                            IterateAsMutableIList |      .NET 5.0 |  100 |    835.10 ns |    13.057 ns |    12.213 ns |      40 B |
|                            IterateAsMutableIList |      .NET 6.0 |  100 |    975.59 ns |    19.508 ns |    20.874 ns |      40 B |
|                            IterateAsMutableIList |      .NET 7.0 |  100 |    901.60 ns |    17.241 ns |    16.127 ns |      40 B |
|                            IterateAsMutableIList |      .NET 8.0 |  100 |    281.74 ns |     5.329 ns |     5.472 ns |      40 B |
|                                                  |               |      |              |              |              |           |
|                      IterateAsMutableICollection | .NET Core 3.1 |  100 |     897.0 ns |      7.09 ns |      6.63 ns |      40 B |
|                      IterateAsMutableICollection |      .NET 5.0 |  100 |    829.72 ns |    15.439 ns |    14.441 ns |      40 B |
|                      IterateAsMutableICollection |      .NET 6.0 |  100 |  1,021.42 ns |    20.238 ns |    48.097 ns |      40 B |
|                      IterateAsMutableICollection |      .NET 7.0 |  100 |    894.68 ns |    13.823 ns |    12.930 ns |      40 B |
|                      IterateAsMutableICollection |      .NET 8.0 |  100 |    281.64 ns |     5.578 ns |     6.640 ns |      40 B |
|                                                  |               |      |              |              |              |           |
|                             IterateAsIEnumerable | .NET Core 3.1 |  100 |     888.0 ns |      8.07 ns |      7.15 ns |      40 B |
|                             IterateAsIEnumerable |      .NET 5.0 |  100 |    835.58 ns |    14.329 ns |    13.403 ns |      40 B |
|                             IterateAsIEnumerable |      .NET 6.0 |  100 |  1,007.00 ns |    19.832 ns |    27.801 ns |      40 B |
|                             IterateAsIEnumerable |      .NET 7.0 |  100 |    903.82 ns |    17.124 ns |    19.033 ns |      40 B |
|                             IterateAsIEnumerable |      .NET 8.0 |  100 |    278.81 ns |     5.309 ns |     6.320 ns |      40 B |
|                                                  |               |      |              |              |              |           |
|                            IterateAsMutableArray | .NET Core 3.1 |  100 |     156.3 ns |      1.71 ns |      1.43 ns |     824 B |
|                            IterateAsMutableArray |      .NET 5.0 |  100 |    123.27 ns |     2.482 ns |     5.602 ns |     824 B |
|                            IterateAsMutableArray |      .NET 6.0 |  100 |    145.39 ns |     4.062 ns |    11.912 ns |     824 B |
|                            IterateAsMutableArray |      .NET 7.0 |  100 |    129.07 ns |     2.602 ns |     5.136 ns |     824 B |
|                            IterateAsMutableArray |      .NET 8.0 |  100 |    105.36 ns |     2.106 ns |     3.087 ns |     824 B |
|                                                  |               |      |              |              |              |           |
|                     IterateAsIReadOnlyCollection | .NET Core 3.1 |  100 |     835.7 ns |      7.41 ns |      6.94 ns |      64 B |
|                     IterateAsIReadOnlyCollection |      .NET 5.0 |  100 |    801.34 ns |    15.660 ns |    17.406 ns |      64 B |
|                     IterateAsIReadOnlyCollection |      .NET 6.0 |  100 |    928.58 ns |    16.061 ns |    13.412 ns |      64 B |
|                     IterateAsIReadOnlyCollection |      .NET 7.0 |  100 |    879.08 ns |    17.478 ns |    29.679 ns |      64 B |
|                     IterateAsIReadOnlyCollection |      .NET 8.0 |  100 |    345.50 ns |     6.365 ns |     6.536 ns |      64 B |
|                                                  |               |      |              |              |              |           |
|                           IterateAsReadOnlyArray | .NET Core 3.1 |  100 |     548.7 ns |      6.07 ns |      5.38 ns |     880 B |
|                           IterateAsReadOnlyArray |      .NET 5.0 |  100 |    548.88 ns |    10.817 ns |    19.506 ns |     880 B |
|                           IterateAsReadOnlyArray |      .NET 6.0 |  100 |    539.32 ns |    10.649 ns |    14.576 ns |     880 B |
|                           IterateAsReadOnlyArray |      .NET 7.0 |  100 |    493.62 ns |     9.862 ns |    17.530 ns |     880 B |
|                           IterateAsReadOnlyArray |      .NET 8.0 |  100 |    233.54 ns |     4.650 ns |     9.393 ns |     880 B |
|                                                  |               |      |              |              |              |           |
|  IterateAsImmutableListCastToIReadOnlyCollection | .NET Core 3.1 |  100 |  10,080.3 ns |    191.44 ns |    179.07 ns |    4920 B |
|  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 5.0 |  100 |  7,970.88 ns |   157.837 ns |   147.641 ns |    4920 B |
|  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 6.0 |  100 |  6,844.65 ns |   124.806 ns |   104.219 ns |    4920 B |
|  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 7.0 |  100 |  6,699.77 ns |   110.707 ns |   123.051 ns |    4920 B |
|  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 8.0 |  100 |  3,339.33 ns |    65.041 ns |    57.657 ns |    4920 B |
|                                                  |               |      |              |              |              |           |
|                          IterateAsIImmutableList | .NET Core 3.1 |  100 |   9,827.8 ns |     61.13 ns |     51.05 ns |    4920 B |
|                          IterateAsIImmutableList |      .NET 5.0 |  100 |  7,752.44 ns |   108.453 ns |    96.141 ns |    4920 B |
|                          IterateAsIImmutableList |      .NET 6.0 |  100 |  6,845.22 ns |    76.360 ns |    67.692 ns |    4920 B |
|                          IterateAsIImmutableList |      .NET 7.0 |  100 |  6,660.65 ns |   132.590 ns |   157.839 ns |    4920 B |
|                          IterateAsIImmutableList |      .NET 8.0 |  100 |  3,438.22 ns |    61.692 ns |    57.707 ns |    4920 B |
|                                                  |               |      |              |              |              |           |
| IterateAsImmutableArrayCastToIReadOnlyCollection | .NET Core 3.1 |  100 |     614.9 ns |      7.40 ns |      6.92 ns |     880 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 5.0 |  100 |    632.24 ns |    12.520 ns |    17.137 ns |     880 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 6.0 |  100 |    642.56 ns |    12.880 ns |    17.194 ns |     880 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 7.0 |  100 |    598.70 ns |    11.597 ns |    18.056 ns |     880 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 8.0 |  100 |    278.27 ns |     5.560 ns |    10.845 ns |     880 B |
|                                                  |               |      |              |              |              |           |
|                          IterateAsImmutableArray | .NET Core 3.1 |  100 |     149.6 ns |      2.69 ns |      2.52 ns |     824 B |
|                          IterateAsImmutableArray |      .NET 5.0 |  100 |    129.41 ns |     2.620 ns |     5.751 ns |     824 B |
|                          IterateAsImmutableArray |      .NET 6.0 |  100 |    130.14 ns |     2.593 ns |     4.404 ns |     824 B |
|                          IterateAsImmutableArray |      .NET 7.0 |  100 |    122.71 ns |     2.472 ns |     4.994 ns |     824 B |
|                          IterateAsImmutableArray |      .NET 8.0 |  100 |    114.83 ns |     2.294 ns |     3.062 ns |     824 B |
|                                                  |               |      |              |              |              |           |
|                                    IterateAsSpan | .NET Core 3.1 |  N/A |          N/A |          N/A |          N/A |         - |
|                                    IterateAsSpan |      .NET 5.0 |  N/A |          N/A |          N/A |          N/A |         - |
|                                    IterateAsSpan |      .NET 6.0 | 1000 |    245.31 ns |     4.869 ns |     9.496 ns |         - |
|                                    IterateAsSpan |      .NET 7.0 | 1000 |    246.00 ns |     4.897 ns |     6.014 ns |         - |
|                                    IterateAsSpan |      .NET 8.0 | 1000 |    246.96 ns |     4.966 ns |     6.457 ns |         - |
|                                                  |               |      |              |              |              |           |
|                             IterateAsMutableList | .NET Core 3.1 | 1000 |   3,168.5 ns |     46.34 ns |     43.35 ns |         - |
|                             IterateAsMutableList |      .NET 5.0 | 1000 |  3,064.51 ns |    52.985 ns |    49.562 ns |         - |
|                             IterateAsMutableList |      .NET 6.0 | 1000 |    872.42 ns |    15.731 ns |    16.832 ns |         - |
|                             IterateAsMutableList |      .NET 7.0 | 1000 |    716.98 ns |    14.367 ns |    15.969 ns |         - |
|                             IterateAsMutableList |      .NET 8.0 | 1000 |    484.34 ns |     9.332 ns |    17.980 ns |         - |
|                                                  |               |      |              |              |              |           |
|                            IterateAsMutableIList | .NET Core 3.1 | 1000 |   8,575.3 ns |     49.48 ns |     38.63 ns |      40 B |
|                            IterateAsMutableIList |      .NET 5.0 | 1000 |  8,149.08 ns |   160.072 ns |   249.213 ns |      40 B |
|                            IterateAsMutableIList |      .NET 6.0 | 1000 |  8,896.33 ns |   169.103 ns |   173.656 ns |      40 B |
|                            IterateAsMutableIList |      .NET 7.0 | 1000 |  8,384.64 ns |   153.449 ns |   220.072 ns |      40 B |
|                            IterateAsMutableIList |      .NET 8.0 | 1000 |  3,106.39 ns |    60.543 ns |    59.461 ns |      40 B |
|                                                  |               |      |              |              |              |           |
|                      IterateAsMutableICollection | .NET Core 3.1 | 1000 |   8,600.0 ns |     80.31 ns |     71.19 ns |      40 B |
|                      IterateAsMutableICollection |      .NET 5.0 | 1000 |  8,092.11 ns |   151.778 ns |   155.865 ns |      40 B |
|                      IterateAsMutableICollection |      .NET 6.0 | 1000 |  8,908.13 ns |   173.299 ns |   248.540 ns |      40 B |
|                      IterateAsMutableICollection |      .NET 7.0 | 1000 |  8,437.38 ns |   166.475 ns |   340.063 ns |      40 B |
|                      IterateAsMutableICollection |      .NET 8.0 | 1000 |  3,105.78 ns |    60.999 ns |    67.800 ns |      40 B |
|                                                  |               |      |              |              |              |           |
|                             IterateAsIEnumerable | .NET Core 3.1 | 1000 |   8,610.6 ns |     77.57 ns |     68.76 ns |      40 B |
|                             IterateAsIEnumerable |      .NET 5.0 | 1000 |  8,094.28 ns |   147.501 ns |   196.909 ns |      40 B |
|                             IterateAsIEnumerable |      .NET 6.0 | 1000 |  8,953.89 ns |   174.394 ns |   261.025 ns |      40 B |
|                             IterateAsIEnumerable |      .NET 7.0 | 1000 |  8,324.44 ns |   150.546 ns |   251.529 ns |      40 B |
|                             IterateAsIEnumerable |      .NET 8.0 | 1000 |  3,076.62 ns |    59.929 ns |    64.123 ns |      40 B |
|                                                  |               |      |              |              |              |           |
|                            IterateAsMutableArray | .NET Core 3.1 | 1000 |     808.1 ns |      9.81 ns |      8.19 ns |    8024 B |
|                            IterateAsMutableArray |      .NET 5.0 | 1000 |    746.47 ns |    14.822 ns |    18.745 ns |    8024 B |
|                            IterateAsMutableArray |      .NET 6.0 | 1000 |    791.07 ns |    13.542 ns |    18.537 ns |    8024 B |
|                            IterateAsMutableArray |      .NET 7.0 | 1000 |    781.98 ns |    15.651 ns |    21.940 ns |    8024 B |
|                            IterateAsMutableArray |      .NET 8.0 | 1000 |    743.54 ns |    14.167 ns |    12.559 ns |    8024 B |
|                                                  |               |      |              |              |              |           |
|                     IterateAsIReadOnlyCollection | .NET Core 3.1 | 1000 |   8,200.6 ns |    127.99 ns |    119.72 ns |      64 B |
|                     IterateAsIReadOnlyCollection |      .NET 5.0 | 1000 |  7,573.45 ns |   113.343 ns |    94.646 ns |      64 B |
|                     IterateAsIReadOnlyCollection |      .NET 6.0 | 1000 |  9,005.13 ns |   179.990 ns |   252.321 ns |      64 B |
|                     IterateAsIReadOnlyCollection |      .NET 7.0 | 1000 |  8,668.80 ns |   162.846 ns |   305.864 ns |      64 B |
|                     IterateAsIReadOnlyCollection |      .NET 8.0 | 1000 |  3,070.69 ns |    60.126 ns |    76.040 ns |      64 B |
|                                                  |               |      |              |              |              |           |
|                           IterateAsReadOnlyArray | .NET Core 3.1 | 1000 |   5,076.5 ns |     64.05 ns |     56.78 ns |    8080 B |
|                           IterateAsReadOnlyArray |      .NET 5.0 | 1000 |  4,867.89 ns |    96.912 ns |   156.496 ns |    8080 B |
|                           IterateAsReadOnlyArray |      .NET 6.0 | 1000 |  4,929.40 ns |    97.659 ns |   160.457 ns |    8080 B |
|                           IterateAsReadOnlyArray |      .NET 7.0 | 1000 |  4,475.06 ns |    86.615 ns |   134.849 ns |    8080 B |
|                           IterateAsReadOnlyArray |      .NET 8.0 | 1000 |  1,948.07 ns |    38.853 ns |    93.835 ns |    8080 B |
|                                                  |               |      |              |              |              |           |
|  IterateAsImmutableListCastToIReadOnlyCollection | .NET Core 3.1 | 1000 |  99,576.8 ns |  1,438.09 ns |  1,274.83 ns |   48120 B |
|  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 5.0 | 1000 | 74,719.02 ns | 1,458.366 ns | 1,844.366 ns |   48120 B |
|  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 6.0 | 1000 | 62,614.81 ns | 1,240.556 ns | 1,327.381 ns |   48120 B |
|  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 7.0 | 1000 | 65,955.88 ns | 1,314.931 ns | 1,565.332 ns |   48120 B |
|  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 8.0 | 1000 | 33,799.11 ns |   673.462 ns | 1,464.052 ns |   48120 B |
|                                                  |               |      |              |              |              |           |
|                          IterateAsIImmutableList | .NET Core 3.1 | 1000 |  98,902.6 ns |  1,153.25 ns |  1,022.33 ns |   48121 B |
|                          IterateAsIImmutableList |      .NET 5.0 | 1000 | 74,918.27 ns | 1,493.586 ns | 2,411.862 ns |   48120 B |
|                          IterateAsIImmutableList |      .NET 6.0 | 1000 | 65,774.12 ns | 1,305.544 ns | 2,753.835 ns |   48120 B |
|                          IterateAsIImmutableList |      .NET 7.0 | 1000 | 67,807.60 ns | 1,284.156 ns | 2,411.956 ns |   48120 B |
|                          IterateAsIImmutableList |      .NET 8.0 | 1000 | 31,823.79 ns |   632.937 ns | 1,173.190 ns |   48120 B |
|                                                  |               |      |              |              |              |           |
| IterateAsImmutableArrayCastToIReadOnlyCollection | .NET Core 3.1 | 1000 |   5,311.7 ns |     68.45 ns |     60.68 ns |    8080 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 5.0 | 1000 |  5,600.76 ns |   108.667 ns |   141.298 ns |    8080 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 6.0 | 1000 |  5,855.68 ns |   108.634 ns |   159.235 ns |    8080 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 7.0 | 1000 |  5,435.78 ns |   106.556 ns |   130.861 ns |    8080 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 8.0 | 1000 |  2,516.06 ns |    49.547 ns |    62.661 ns |    8080 B |
|                                                  |               |      |              |              |              |           |
|                          IterateAsImmutableArray | .NET Core 3.1 | 1000 |     810.0 ns |     12.69 ns |     11.25 ns |    8024 B |
|                          IterateAsImmutableArray |      .NET 5.0 | 1000 |    769.17 ns |    14.466 ns |    27.171 ns |    8024 B |
|                          IterateAsImmutableArray |      .NET 6.0 | 1000 |    824.53 ns |    18.319 ns |    52.560 ns |    8024 B |
|                          IterateAsImmutableArray |      .NET 7.0 | 1000 |    793.43 ns |    15.659 ns |    22.953 ns |    8024 B |
|                          IterateAsImmutableArray |      .NET 8.0 | 1000 |    760.47 ns |    15.204 ns |    26.226 ns |    8024 B |

#### Observations

* The interface is more than just a type change, **it's a behavior change**
  * Iterating through `IList` or `ICollection` is > 10x slower (on .NET 6 or 7) than iterating through `List`
  * The reason for this is `List.GetEnumerator` returns a List-specific concrete class with direct method calls while `IList.GetEnumerator` returns a generic `IEnumerator<T>` with virtual method calls.
* Iterating over `IEnumerable` vs. `ICollection` is the same
* Iterating over `IReadOnlyCollection` vs. `ICollection` is the same
* Converting a `List` to a mutable array provides no performance benefit and duplicates the memory allocation
* Converting the `List` to a read-only array vs. `IReadOnlyCollection` (`Array.AsReadOnly(data.ToArray)` vs. `List.AsReadOnly`) is nearly 40% faster but at the expense of duplicating memory allocation
* Using `ImmutableList` is slow and uses 6x the memory allocation
* Iterating over `IImmutableList` vs. `IReadOnlyCollection` for `ImmutableList` is the same
* Converting to and using `ImmutableArray` is faster than converting to and using read-only array with slightly less memory overhead _but only if not cast to `IReadOnlyCollection`!_
* List iteration got much faster between .NET Core 5 and .NET 6
* .NET 8 is considerably faster in most scenarios than all prior versions
* Converting a mutable List (that never changes) to a Span first is fastest of all

#### Conclusions

* **For public APIs**: for best safety, lowest memory usage, and best performance, use `IReadOnlyCollection` / `List.AsReadOnly`.
* **For private APIs**: for best performance, if read-only safety is not important, keep as `List`, and if you can guarantee the List doesn't change, consider converting to a Span (via `CollectionsMarshal.AsSpan`) first
* **For private APIs**: for best safety and best performance, use `ImmutableArray` if iterating many times, or `IReadOnlyCollection` / `List.AsReadOnly` if iterating minimally
* Do not use `ImmutableList` _for this use case_ (the ImmutableCollections are certainly appropriate when concurrent modification is needed)

---------------------------

### Aggregate list using LINQ

Another common pattern is similar to the iteration example above, but instead of simple iteration with `foreach`, the program performs a LINQ operation on the collection. The included `LinqBenchy` tests the performance and memory characteristics of various collection types when used in this pattern for both a list of 100 items and a list of 1000 items.

#### Benchmark Results

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.6.1 (21G217) [Darwin 21.6.0]

Intel Core i9-9980HK CPU 2.40GHz, 1 CPU, 16 logical and 8 physical cores

.NET SDK=7.0.100

  [Host]     : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2<br>
  Job-SYVTEW : .NET Core 3.1.20 (CoreCLR 4.700.21.47003, CoreFX 4.700.21.47101), X64 RyuJIT AVX2<br>
  Job-CLISTV : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2<br>
  Job-FLHOKN : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2

|                                             Method |       Runtime |    N |        Mean |     Error |    StdDev | Allocated |
|--------------------------------------------------- |-------------- |----- |------------:|----------:|----------:|----------:|
|                             AggregateAsMutableList | .NET Core 3.1 |  100 |    755.2 ns |   6.13 ns |   5.74 ns |      40 B |
|                             AggregateAsMutableList |      .NET 6.0 |  100 |    770.7 ns |   6.29 ns |   5.88 ns |      40 B |
|                             AggregateAsMutableList |      .NET 7.0 |  100 |    796.4 ns |  13.69 ns |  12.80 ns |      40 B |
|                                                    |               |      |             |           |           |           |
|                            AggregateAsMutableIList | .NET Core 3.1 |  100 |    755.4 ns |   6.27 ns |   5.56 ns |      40 B |
|                            AggregateAsMutableIList |      .NET 6.0 |  100 |    769.2 ns |   5.75 ns |   5.38 ns |      40 B |
|                            AggregateAsMutableIList |      .NET 7.0 |  100 |    787.3 ns |   7.13 ns |   5.95 ns |      40 B |
|                                                    |               |      |             |           |           |           |
|                      AggregateAsMutableICollection | .NET Core 3.1 |  100 |    755.1 ns |   9.67 ns |   9.04 ns |      40 B |
|                      AggregateAsMutableICollection |      .NET 6.0 |  100 |    767.5 ns |   8.16 ns |   7.64 ns |      40 B |
|                      AggregateAsMutableICollection |      .NET 7.0 |  100 |    786.4 ns |   6.86 ns |   6.41 ns |      40 B |
|                                                    |               |      |             |           |           |           |
|                             AggregateAsIEnumerable | .NET Core 3.1 |  100 |    752.4 ns |   5.58 ns |   4.66 ns |      40 B |
|                             AggregateAsIEnumerable |      .NET 6.0 |  100 |    765.5 ns |   5.42 ns |   5.07 ns |      40 B |
|                             AggregateAsIEnumerable |      .NET 7.0 |  100 |    783.9 ns |   6.26 ns |   5.55 ns |      40 B |
|                                                    |               |      |             |           |           |           |
|                            AggregateAsMutableArray | .NET Core 3.1 |  100 |    614.4 ns |   8.02 ns |   7.50 ns |     856 B |
|                            AggregateAsMutableArray |      .NET 6.0 |  100 |    618.0 ns |   5.68 ns |   5.32 ns |     856 B |
|                            AggregateAsMutableArray |      .NET 7.0 |  100 |    591.7 ns |   9.14 ns |   8.10 ns |     856 B |
|                                                    |               |      |             |           |           |           |
|                     AggregateAsIReadOnlyCollection | .NET Core 3.1 |  100 |    810.2 ns |  15.17 ns |  13.44 ns |      64 B |
|                     AggregateAsIReadOnlyCollection |      .NET 6.0 |  100 |    733.8 ns |   7.98 ns |   7.46 ns |      64 B |
|                     AggregateAsIReadOnlyCollection |      .NET 7.0 |  100 |    781.8 ns |  12.64 ns |  11.20 ns |      64 B |
|                                                    |               |      |             |           |           |           |
|                           AggregateAsReadOnlyArray | .NET Core 3.1 |  100 |    580.6 ns |   5.73 ns |   4.79 ns |     880 B |
|                           AggregateAsReadOnlyArray |      .NET 6.0 |  100 |    586.4 ns |   9.12 ns |   8.53 ns |     880 B |
|                           AggregateAsReadOnlyArray |      .NET 7.0 |  100 |    597.1 ns |   8.13 ns |   7.61 ns |     880 B |
|                                                    |               |      |             |           |           |           |
|  AggregateAsImmutableListCastToIReadOnlyCollection | .NET Core 3.1 |  100 |  5,244.2 ns |  46.73 ns |  39.02 ns |    4920 B |
|  AggregateAsImmutableListCastToIReadOnlyCollection |      .NET 6.0 |  100 |  4,121.0 ns |  33.36 ns |  26.04 ns |    4920 B |
|  AggregateAsImmutableListCastToIReadOnlyCollection |      .NET 7.0 |  100 |  4,189.0 ns |  28.81 ns |  24.06 ns |    4920 B |
|                                                    |               |      |             |           |           |           |
|                          AggregateAsIImmutableList | .NET Core 3.1 |  100 |  5,238.3 ns |  54.66 ns |  51.13 ns |    4920 B |
|                          AggregateAsIImmutableList |      .NET 6.0 |  100 |  4,155.4 ns |  31.51 ns |  27.93 ns |    4920 B |
|                          AggregateAsIImmutableList |      .NET 7.0 |  100 |  4,207.5 ns |  34.46 ns |  28.78 ns |    4920 B |
|                                                    |               |      |             |           |           |           |
| AggregateAsImmutableArrayCastToIReadOnlyCollection | .NET Core 3.1 |  100 |    650.1 ns |   7.28 ns |   6.81 ns |     880 B |
| AggregateAsImmutableArrayCastToIReadOnlyCollection |      .NET 6.0 |  100 |    668.7 ns |   9.49 ns |   8.88 ns |     880 B |
| AggregateAsImmutableArrayCastToIReadOnlyCollection |      .NET 7.0 |  100 |    641.6 ns |  11.48 ns |  10.18 ns |     880 B |
|                                                    |               |      |             |           |           |           |
|                          AggregateAsImmutableArray | .NET Core 3.1 |  100 |    256.5 ns |   3.16 ns |   2.64 ns |     824 B |
|                          AggregateAsImmutableArray |      .NET 6.0 |  100 |    260.1 ns |   3.45 ns |   3.06 ns |     824 B |
|                          AggregateAsImmutableArray |      .NET 7.0 |  100 |    248.8 ns |   3.06 ns |   2.71 ns |     824 B |
|                                                    |               |      |             |           |           |           |
|                             AggregateAsMutableList | .NET Core 3.1 | 1000 |  7,060.8 ns |  44.88 ns |  39.79 ns |      40 B |
|                             AggregateAsMutableList |      .NET 6.0 | 1000 |  7,559.2 ns | 148.54 ns | 193.14 ns |      40 B |
|                             AggregateAsMutableList |      .NET 7.0 | 1000 |  7,686.5 ns | 150.88 ns | 148.19 ns |      40 B |
|                                                    |               |      |             |           |           |           |
|                            AggregateAsMutableIList | .NET Core 3.1 | 1000 |  7,165.6 ns | 103.28 ns |  96.61 ns |      40 B |
|                            AggregateAsMutableIList |      .NET 6.0 | 1000 |  7,438.7 ns |  68.86 ns |  61.05 ns |      40 B |
|                            AggregateAsMutableIList |      .NET 7.0 | 1000 |  7,654.0 ns |  69.92 ns |  61.98 ns |      40 B |
|                                                    |               |      |             |           |           |           |
|                      AggregateAsMutableICollection | .NET Core 3.1 | 1000 |  7,503.1 ns | 101.75 ns |  95.18 ns |      40 B |
|                      AggregateAsMutableICollection |      .NET 6.0 | 1000 |  7,481.6 ns |  96.30 ns |  85.37 ns |      40 B |
|                      AggregateAsMutableICollection |      .NET 7.0 | 1000 |  7,641.9 ns |  65.00 ns |  50.74 ns |      40 B |
|                                                    |               |      |             |           |           |           |
|                             AggregateAsIEnumerable | .NET Core 3.1 | 1000 |  7,435.7 ns |  63.33 ns |  59.24 ns |      40 B |
|                             AggregateAsIEnumerable |      .NET 6.0 | 1000 |  7,460.6 ns |  98.28 ns |  91.93 ns |      40 B |
|                             AggregateAsIEnumerable |      .NET 7.0 | 1000 |  7,664.0 ns |  70.63 ns |  66.07 ns |      40 B |
|                                                    |               |      |             |           |           |           |
|                            AggregateAsMutableArray | .NET Core 3.1 | 1000 |  5,865.3 ns |  77.47 ns |  64.69 ns |    8056 B |
|                            AggregateAsMutableArray |      .NET 6.0 | 1000 |  5,940.8 ns |  66.55 ns |  62.25 ns |    8056 B |
|                            AggregateAsMutableArray |      .NET 7.0 | 1000 |  5,643.5 ns |  65.46 ns |  58.03 ns |    8056 B |
|                                                    |               |      |             |           |           |           |
|                     AggregateAsIReadOnlyCollection | .NET Core 3.1 | 1000 |  7,811.7 ns | 144.69 ns | 128.26 ns |      64 B |
|                     AggregateAsIReadOnlyCollection |      .NET 6.0 | 1000 |  6,950.4 ns |  44.28 ns |  34.57 ns |      64 B |
|                     AggregateAsIReadOnlyCollection |      .NET 7.0 | 1000 |  7,890.4 ns |  46.26 ns |  38.63 ns |      64 B |
|                                                    |               |      |             |           |           |           |
|                           AggregateAsReadOnlyArray | .NET Core 3.1 | 1000 |  5,415.1 ns |  39.51 ns |  32.99 ns |    8080 B |
|                           AggregateAsReadOnlyArray |      .NET 6.0 | 1000 |  5,963.2 ns | 111.81 ns | 104.59 ns |    8080 B |
|                           AggregateAsReadOnlyArray |      .NET 7.0 | 1000 |  5,441.7 ns |  72.09 ns |  67.43 ns |    8080 B |
|                                                    |               |      |             |           |           |           |
|  AggregateAsImmutableListCastToIReadOnlyCollection | .NET Core 3.1 | 1000 | 52,002.4 ns | 584.07 ns | 546.34 ns |   48120 B |
|  AggregateAsImmutableListCastToIReadOnlyCollection |      .NET 6.0 | 1000 | 39,917.2 ns | 592.72 ns | 525.43 ns |   48120 B |
|  AggregateAsImmutableListCastToIReadOnlyCollection |      .NET 7.0 | 1000 | 40,370.9 ns | 526.35 ns | 492.34 ns |   48120 B |
|                                                    |               |      |             |           |           |           |
|                          AggregateAsIImmutableList | .NET Core 3.1 | 1000 | 51,796.8 ns | 606.88 ns | 506.77 ns |   48120 B |
|                          AggregateAsIImmutableList |      .NET 6.0 | 1000 | 39,648.7 ns | 162.09 ns | 126.55 ns |   48120 B |
|                          AggregateAsIImmutableList |      .NET 7.0 | 1000 | 40,401.5 ns | 570.26 ns | 533.42 ns |   48120 B |
|                                                    |               |      |             |           |           |           |
| AggregateAsImmutableArrayCastToIReadOnlyCollection | .NET Core 3.1 | 1000 |  5,775.7 ns |  51.30 ns |  47.98 ns |    8080 B |
| AggregateAsImmutableArrayCastToIReadOnlyCollection |      .NET 6.0 | 1000 |  6,248.6 ns | 120.75 ns | 129.21 ns |    8080 B |
| AggregateAsImmutableArrayCastToIReadOnlyCollection |      .NET 7.0 | 1000 |  5,715.6 ns |  61.33 ns |  57.36 ns |    8080 B |
|                                                    |               |      |             |           |           |           |
|                          AggregateAsImmutableArray | .NET Core 3.1 | 1000 |  2,205.1 ns |  40.74 ns |  34.02 ns |    8024 B |
|                          AggregateAsImmutableArray |      .NET 6.0 | 1000 |  2,256.3 ns |  33.17 ns |  27.70 ns |    8024 B |
|                          AggregateAsImmutableArray |      .NET 7.0 | 1000 |  2,238.4 ns |  19.07 ns |  17.84 ns |    8024 B |

#### Observations

* The difference that was present in the Iteration benchmark between interface and implementation types isn't there; the implementation type has no speed advantage over the interface type
* `ImmutableList` is still the worst
* `ImmutableArray` is the best performance of all (as long as not cast to `IReadOnlyCollection`)

#### Conclusions

The same as in the Iteration benchmark, except here there's no penalty for defining your APIs with `IReadOnlyCollection` / `ICollection` / `IEnumerable` if keeping the collection as a `List`. Also, prefer `ToImmutableArray` over `Array.AsReadOnly(data.ToArray())` as both memory consumption and performance is better.
