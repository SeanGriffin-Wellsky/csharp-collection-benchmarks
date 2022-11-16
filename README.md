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

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.6.1 (21G217) [Darwin 21.6.0]

Intel Core i9-9980HK CPU 2.40GHz, 1 CPU, 16 logical and 8 physical cores

.NET SDK=7.0.100<br>
  [Host]     : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2<br>
  Job-VFPNOY : .NET Core 3.1.20 (CoreCLR 4.700.21.47003, CoreFX 4.700.21.47101), X64 RyuJIT AVX2<br>
  Job-OVRMSJ : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2<br>
  Job-FDUCSD : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2

  |                                           Method |       Runtime |    N |         Mean |        Error |      StdDev | Allocated |
  |------------------------------------------------- |-------------- |----- |-------------:|-------------:|------------:|----------:|
  |                             IterateAsMutableList | .NET Core 3.1 |  100 |     333.9 ns |      2.61 ns |     2.44 ns |         - |
  |                             IterateAsMutableList |      .NET 6.0 |  100 |     87.39 ns |     0.754 ns |    0.668 ns |         - |
  |                             IterateAsMutableList |      .NET 7.0 |  100 |     80.01 ns |     0.567 ns |    0.473 ns |         - |
  |                                                  |               |      |              |              |             |           |
  |                                IterateListAsSpan | .NET Core 3.1 |  N/A |          N/A |          N/A |         N/A |         - |
  |                                IterateListAsSpan |      .NET 6.0 |  100 |     34.64 ns |     0.451 ns |    0.376 ns |         - |
  |                                IterateListAsSpan |      .NET 7.0 |  100 |     33.45 ns |     0.336 ns |    0.298 ns |         - |
  |                                                  |               |      |              |              |             |           |                                                  |               |      |              |              |             |           |
  |                            IterateAsMutableIList | .NET Core 3.1 |  100 |     880.0 ns |      4.86 ns |     3.79 ns |      40 B |
  |                            IterateAsMutableIList |      .NET 6.0 |  100 |    907.83 ns |    10.521 ns |    9.327 ns |      40 B |
  |                            IterateAsMutableIList |      .NET 7.0 |  100 |    877.90 ns |     9.787 ns |    9.155 ns |      40 B |
  |                                                  |               |      |              |              |             |           |
  |                      IterateAsMutableICollection | .NET Core 3.1 |  100 |     897.0 ns |      7.09 ns |     6.63 ns |      40 B |
  |                      IterateAsMutableICollection |      .NET 6.0 |  100 |    913.09 ns |    13.996 ns |   13.092 ns |      40 B |
  |                      IterateAsMutableICollection |      .NET 7.0 |  100 |    872.09 ns |     6.881 ns |    5.746 ns |      40 B |
  |                                                  |               |      |              |              |             |           |
  |                             IterateAsIEnumerable | .NET Core 3.1 |  100 |     888.0 ns |      8.07 ns |     7.15 ns |      40 B |
  |                             IterateAsIEnumerable |      .NET 6.0 |  100 |    903.70 ns |     5.133 ns |    4.801 ns |      40 B |
  |                             IterateAsIEnumerable |      .NET 7.0 |  100 |    873.42 ns |     9.161 ns |    8.569 ns |      40 B |
  |                                                  |               |      |              |              |             |           |
  |                            IterateAsMutableArray | .NET Core 3.1 |  100 |     156.3 ns |      1.71 ns |     1.43 ns |     824 B |
  |                            IterateAsMutableArray |      .NET 6.0 |  100 |    141.37 ns |     1.833 ns |    1.625 ns |     824 B |
  |                            IterateAsMutableArray |      .NET 7.0 |  100 |    125.43 ns |     1.218 ns |    1.017 ns |     824 B |
  |                                                  |               |      |              |              |             |           |
  |                     IterateAsIReadOnlyCollection | .NET Core 3.1 |  100 |     835.7 ns |      7.41 ns |     6.94 ns |      64 B |
  |                     IterateAsIReadOnlyCollection |      .NET 6.0 |  100 |    909.55 ns |    14.062 ns |   12.466 ns |      64 B |
  |                     IterateAsIReadOnlyCollection |      .NET 7.0 |  100 |    870.09 ns |     6.275 ns |    5.240 ns |      64 B |
  |                                                  |               |      |              |              |             |           |
  |                           IterateAsReadOnlyArray | .NET Core 3.1 |  100 |     548.7 ns |      6.07 ns |     5.38 ns |     880 B |
  |                           IterateAsReadOnlyArray |      .NET 6.0 |  100 |    510.88 ns |     7.013 ns |    6.560 ns |     880 B |
  |                           IterateAsReadOnlyArray |      .NET 7.0 |  100 |    478.02 ns |     5.987 ns |    4.999 ns |     880 B |
  |                                                  |               |      |              |              |             |           |
  |  IterateAsImmutableListCastToIReadOnlyCollection | .NET Core 3.1 |  100 |  10,080.3 ns |    191.44 ns |   179.07 ns |    4920 B |
  |  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 6.0 |  100 |  6,509.74 ns |    62.137 ns |   58.123 ns |    4920 B |
  |  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 7.0 |  100 |  6,440.87 ns |    59.982 ns |   53.173 ns |    4920 B |
  |                                                  |               |      |              |              |             |           |
  |                          IterateAsIImmutableList | .NET Core 3.1 |  100 |   9,827.8 ns |     61.13 ns |    51.05 ns |    4920 B |
  |                          IterateAsIImmutableList |      .NET 6.0 |  100 |  6,469.47 ns |    61.308 ns |   54.348 ns |    4920 B |
  |                          IterateAsIImmutableList |      .NET 7.0 |  100 |  6,395.03 ns |    59.409 ns |   49.609 ns |    4920 B |
  |                                                  |               |      |              |              |             |           |
  | IterateAsImmutableArrayCastToIReadOnlyCollection | .NET Core 3.1 |  100 |     614.9 ns |      7.40 ns |     6.92 ns |     880 B |
  | IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 6.0 |  100 |    609.71 ns |     8.265 ns |    7.731 ns |     880 B |
  | IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 7.0 |  100 |    560.79 ns |    10.286 ns |   14.751 ns |     880 B |
  |                                                  |               |      |              |              |             |           |
  |                          IterateAsImmutableArray | .NET Core 3.1 |  100 |     149.6 ns |      2.69 ns |     2.52 ns |     824 B |
  |                          IterateAsImmutableArray |      .NET 6.0 |  100 |    130.68 ns |     1.506 ns |    1.335 ns |     824 B |
  |                          IterateAsImmutableArray |      .NET 7.0 |  100 |    126.70 ns |     1.682 ns |    1.491 ns |     824 B |
  |                                                  |               |      |              |              |             |           |
  |                             IterateAsMutableList | .NET Core 3.1 | 1000 |   3,168.5 ns |     46.34 ns |    43.35 ns |         - |
  |                             IterateAsMutableList |      .NET 6.0 | 1000 |    757.48 ns |     5.763 ns |    5.109 ns |         - |
  |                             IterateAsMutableList |      .NET 7.0 | 1000 |    727.42 ns |     5.946 ns |    4.965 ns |         - |
  |                                                  |               |      |              |              |             |           |
  |                                IterateListAsSpan | .NET Core 3.1 |  N/A |          N/A |          N/A |         N/A |         - |
  |                                IterateListAsSpan |      .NET 6.0 | 1000 |    253.82 ns |     2.112 ns |    1.764 ns |         - |
  |                                IterateListAsSpan |      .NET 7.0 | 1000 |    258.34 ns |     4.988 ns |    9.847 ns |         - |
  |                                                  |               |      |              |              |             |           |
  |                            IterateAsMutableIList | .NET Core 3.1 | 1000 |   8,575.3 ns |     49.48 ns |    38.63 ns |      40 B |
  |                            IterateAsMutableIList |      .NET 6.0 | 1000 |  8,699.82 ns |    95.371 ns |   84.544 ns |      40 B |
  |                            IterateAsMutableIList |      .NET 7.0 | 1000 |  8,202.01 ns |    67.443 ns |   56.318 ns |      40 B |
  |                                                  |               |      |              |              |             |           |
  |                      IterateAsMutableICollection | .NET Core 3.1 | 1000 |   8,600.0 ns |     80.31 ns |    71.19 ns |      40 B |
  |                      IterateAsMutableICollection |      .NET 6.0 | 1000 |  8,767.50 ns |   117.449 ns |  109.862 ns |      40 B |
  |                      IterateAsMutableICollection |      .NET 7.0 | 1000 |  8,275.36 ns |   128.342 ns |  120.051 ns |      40 B |
  |                                                  |               |      |              |              |             |           |
  |                             IterateAsIEnumerable | .NET Core 3.1 | 1000 |   8,610.6 ns |     77.57 ns |    68.76 ns |      40 B |
  |                             IterateAsIEnumerable |      .NET 6.0 | 1000 |  8,716.24 ns |    83.288 ns |   77.907 ns |      40 B |
  |                             IterateAsIEnumerable |      .NET 7.0 | 1000 |  8,275.69 ns |    92.752 ns |   82.222 ns |      40 B |
  |                                                  |               |      |              |              |             |           |
  |                            IterateAsMutableArray | .NET Core 3.1 | 1000 |     808.1 ns |      9.81 ns |     8.19 ns |    8024 B |
  |                            IterateAsMutableArray |      .NET 6.0 | 1000 |    840.51 ns |    16.081 ns |   31.742 ns |    8024 B |
  |                            IterateAsMutableArray |      .NET 7.0 | 1000 |    802.35 ns |    13.309 ns |   11.798 ns |    8024 B |
  |                                                  |               |      |              |              |             |           |
  |                     IterateAsIReadOnlyCollection | .NET Core 3.1 | 1000 |   8,200.6 ns |    127.99 ns |   119.72 ns |      64 B |
  |                     IterateAsIReadOnlyCollection |      .NET 6.0 | 1000 |  8,770.47 ns |   130.256 ns |  121.842 ns |      64 B |
  |                     IterateAsIReadOnlyCollection |      .NET 7.0 | 1000 |  8,549.36 ns |    90.578 ns |   80.295 ns |      64 B |
  |                                                  |               |      |              |              |             |           |
  |                           IterateAsReadOnlyArray | .NET Core 3.1 | 1000 |   5,076.5 ns |     64.05 ns |    56.78 ns |    8080 B |
  |                           IterateAsReadOnlyArray |      .NET 6.0 | 1000 |  4,562.04 ns |    86.463 ns |   99.570 ns |    8080 B |
  |                           IterateAsReadOnlyArray |      .NET 7.0 | 1000 |  4,181.67 ns |    55.298 ns |   46.176 ns |    8080 B |
  |                                                  |               |      |              |              |             |           |
  |  IterateAsImmutableListCastToIReadOnlyCollection | .NET Core 3.1 | 1000 |  99,576.8 ns |  1,438.09 ns | 1,274.83 ns |   48120 B |
  |  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 6.0 | 1000 | 72,208.33 ns |   722.256 ns |  675.599 ns |   48120 B |
  |  IterateAsImmutableListCastToIReadOnlyCollection |      .NET 7.0 | 1000 | 62,622.43 ns |   533.795 ns |  445.743 ns |   48120 B |
  |                                                  |               |      |              |              |             |           |
  |                          IterateAsIImmutableList | .NET Core 3.1 | 1000 |  98,902.6 ns |  1,153.25 ns | 1,022.33 ns |   48121 B |
  |                          IterateAsIImmutableList |      .NET 6.0 | 1000 | 63,337.56 ns |   505.807 ns |  422.371 ns |   48120 B |
  |                          IterateAsIImmutableList |      .NET 7.0 | 1000 | 62,532.72 ns | 1,021.021 ns |  905.108 ns |   48120 B |
  |                                                  |               |      |              |              |             |           |
  | IterateAsImmutableArrayCastToIReadOnlyCollection | .NET Core 3.1 | 1000 |   5,311.7 ns |     68.45 ns |    60.68 ns |    8080 B |
  | IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 6.0 | 1000 |  5,369.52 ns |    56.044 ns |   52.424 ns |    8080 B |
  | IterateAsImmutableArrayCastToIReadOnlyCollection |      .NET 7.0 | 1000 |  4,825.65 ns |    71.784 ns |   67.147 ns |    8080 B |
  |                                                  |               |      |              |              |             |           |
  |                          IterateAsImmutableArray | .NET Core 3.1 | 1000 |     810.0 ns |     12.69 ns |    11.25 ns |    8024 B |
  |                          IterateAsImmutableArray |      .NET 6.0 | 1000 |    806.74 ns |    12.938 ns |   11.469 ns |    8024 B |
  |                          IterateAsImmutableArray |      .NET 7.0 | 1000 |    796.22 ns |    13.536 ns |   12.661 ns |    8024 B |

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
* List iteration got much faster between .NET Core 3.1 and .NET 6
* .NET 7 is, on average, 4-5% faster than .NET 6
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
