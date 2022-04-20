# C# Collection Benchmarks

One of the most fundamental questions developers must ask themselves repeatedly when coding is: which data structure should I use? However, more subtle questions that is just as important include:
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

Or, more simply, using LINQ: `convertToJson(db.GetItems().Where(filter));`

What type should `db.GetItems()` return? What types should `filter()` accept and return? How about `convertToJson()`? The answers to those questions can have varying impacts to safety and performance that are often not clear. Additionally, those coming from a Java background may be surprised to learn how things work in C#, where the best answer in Java is _not_ the best answer in C#.

This project contains microbenchmarks that are meant to help answer these questions.

## Benchmarks

All benchmarks in this project use [BenchmarkDotNet](https://benchmarkdotnet.org/) and were executed on a MacBook Pro 2019 16-inch model, plugged in, with no activities other than the benchmarks.

### Iterate list

Arguably the most common pattern you'll find in a program is to create a list of items in a single thread and then iterate over those unchanging items. The included `IterationBenchy` tests the performance and memory characteristics of various collection types when used in this pattern for both a list of 100 items and a list of 1000 items.

#### Benchmark Results

BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.3.1 (21E258) [Darwin 21.4.0]
Intel Core i9-9980HK CPU 2.40GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

|                                           Method |    N |         Mean |        Error |       StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------------------------------------- |----- |-------------:|-------------:|-------------:|-------:|-------:|----------:|
|                             IterateAsMutableList |  100 |     84.07 ns |     1.616 ns |     1.511 ns |      - |      - |         - |
|                            IterateAsMutableIList |  100 |    829.74 ns |    12.892 ns |    12.059 ns | 0.0048 |      - |      40 B |
|                             IterateAsICollection |  100 |    819.82 ns |     4.759 ns |     3.974 ns | 0.0048 |      - |      40 B |
|                             IterateAsIEnumerable |  100 |    742.90 ns |     14.16 ns |     13.91 ns | 0.0048 |      - |      40 B |
|                            IterateAsMutableArray |  100 |     93.69 ns |     0.890 ns |     0.789 ns | 0.0985 |      - |     824 B |
|                     IterateAsIReadOnlyCollection |  100 |    770.14 ns |     6.420 ns |     6.005 ns | 0.0076 |      - |      64 B |
|                           IterateAsReadOnlyArray |  100 |    471.35 ns |     9.156 ns |     8.993 ns | 0.1049 |      - |     880 B |
|  IterateAsImmutableListCastToIReadOnlyCollection |  100 |  6,062.43 ns |    61.322 ns |    57.361 ns | 0.5875 |      - |   4,920 B |
|                          IterateAsIImmutableList |  100 |  5,973.13 ns |    64.099 ns |    59.958 ns | 0.5875 |      - |   4,920 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection |  100 |    563.09 ns |    10.553 ns |    15.135 ns | 0.1049 |      - |     880 B |
|                          IterateAsImmutableArray |  100 |    114.64 ns |     0.820 ns |     0.767 ns | 0.0985 |      - |     824 B |
|                             IterateAsMutableList | 1000 |    710.46 ns |     8.362 ns |     6.982 ns |      - |      - |         - |
|                            IterateAsMutableIList | 1000 |  7,699.07 ns |   103.605 ns |    86.515 ns |      - |      - |      40 B |
|                             IterateAsICollection | 1000 |  7,729.34 ns |    77.363 ns |    72.365 ns |      - |      - |      40 B |
|                             IterateAsIEnumerable | 1000 |  7,544.20 ns |    150.63 ns |    311.08 ns |      - |      - |      40 B |
|                            IterateAsMutableArray | 1000 |    695.10 ns |    10.599 ns |     9.914 ns | 0.9584 |      - |   8,024 B |
|                     IterateAsIReadOnlyCollection | 1000 |  7,442.05 ns |    93.337 ns |    87.307 ns | 0.0076 |      - |      64 B |
|                           IterateAsReadOnlyArray | 1000 |  4,251.99 ns |    28.607 ns |    23.888 ns | 0.9613 |      - |   8,080 B |
|  IterateAsImmutableListCastToIReadOnlyCollection | 1000 | 60,691.57 ns | 1,213.058 ns | 1,245.721 ns | 5.7373 | 0.4272 |  48,120 B |
|                          IterateAsIImmutableList | 1000 | 59,903.85 ns | 1,163.738 ns | 1,245.187 ns | 5.7373 | 0.4272 |  48,120 B |
| IterateAsImmutableArrayCastToIReadOnlyCollection | 1000 |  4,884.19 ns |    44.780 ns |    41.887 ns | 0.9613 |      - |   8,080 B |
|                          IterateAsImmutableArray | 1000 |    735.22 ns |    14.121 ns |    20.698 ns | 0.9584 |      - |   8,024 B |

#### Observations

* The interface is more than just a type change, **it's a behavior change**
  * Iterating through `IList` or `ICollection` is 10x slower than iterating through `List`
* Iterating over `IEnumerable` vs. `ICollection` is the same
* Iterating over `IReadOnlyCollection` vs. `ICollection` is the same
* Converting a `List` to a mutable array provides no performance benefit and duplicates the memory
* Converting the `List` to a read-only array vs. `IReadOnlyCollection` (`Array.AsReadOnly(data.ToArray)` vs. `List.AsReadOnly`) is nearly 40% faster but at the expense of duplicating memory
* Using `ImmutableList` is slow and uses 6x the memory
* Iterating over `IImmutableList` vs. `IReadOnlyCollection` for `ImmutableList` is the same
* Converting to and using `ImmutableArray` is faster than converting to and using read-only array with slightly less memory overhead _but only if not cast to `IReadOnlyCollection`!_

#### Conclusions

* **For public APIs**: for best safety, lowest memory usage, and best performance, use `IReadOnlyCollection` / `List.AsReadOnly`.
* **For private APIs**: for best performance, if read-only safety is not important, keep as `List`
* **For private APIs**: for best safety and best performance, use `ImmutableArray` if iterating many times, or `IReadOnlyCollection` / `List.AsReadOnly` if iterating minimally
* Do not use `ImmutableList`

---------------------------

### Aggregate list using LINQ

Another common pattern is similar to the iteration example above, but instead of simple iteration with `foreach`, the program performs a LINQ operation on the collection. The included `LinqBenchy` tests the performance and memory characteristics of various collection types when used in this pattern for both a list of 100 items and a list of 1000 items.

#### Benchmark Results

BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.3.1 (21E258) [Darwin 21.4.0]
Intel Core i9-9980HK CPU 2.40GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

|                                             Method |    N |        Mean |     Error |    StdDev |      Median |  Gen 0 |  Gen 1 | Allocated |
|--------------------------------------------------- |----- |------------:|----------:|----------:|------------:|-------:|-------:|----------:|
|                             AggregateAsMutableList |  100 |    744.8 ns |  14.14 ns |  13.89 ns |    739.6 ns | 0.0048 |      - |      40 B |
|                            AggregateAsMutableIList |  100 |    733.9 ns |   5.50 ns |   4.87 ns |    732.6 ns | 0.0048 |      - |      40 B |
|                      AggregateAsMutableICollection |  100 |    721.5 ns |   4.07 ns |   3.80 ns |    720.5 ns | 0.0048 |      - |      40 B |
|                             AggregateAsIEnumerable |  100 |    726.6 ns |   5.10 ns |   4.53 ns |    724.5 ns | 0.0048 |      - |      40 B |
|                            AggregateAsMutableArray |  100 |    590.9 ns |  10.98 ns |  10.79 ns |    593.0 ns | 0.1020 |      - |     856 B |
|                     AggregateAsIReadOnlyCollection |  100 |    754.5 ns |  12.97 ns |  12.13 ns |    756.2 ns | 0.0076 |      - |      64 B |
|                           AggregateAsReadOnlyArray |  100 |    561.1 ns |   3.46 ns |   3.07 ns |    560.6 ns | 0.1049 |      - |     880 B |
|  AggregateAsImmutableListCastToIReadOnlyCollection |  100 |  3,910.4 ns |  40.72 ns |  38.09 ns |  3,904.1 ns | 0.5875 |      - |   4,920 B |
|                          AggregateAsIImmutableList |  100 |  3,954.7 ns |  57.94 ns |  59.50 ns |  3,950.4 ns | 0.5875 |      - |   4,920 B |
| AggregateAsImmutableArrayCastToIReadOnlyCollection |  100 |    606.2 ns |   6.51 ns |   6.09 ns |    603.8 ns | 0.1049 |      - |     880 B |
|                          AggregateAsImmutableArray |  100 |    239.1 ns |   2.09 ns |   1.95 ns |    238.6 ns | 0.0982 |      - |     824 B |
|                             AggregateAsMutableList | 1000 |  7,009.6 ns |  51.98 ns |  43.41 ns |  7,014.7 ns |      - |      - |      40 B |
|                            AggregateAsMutableIList | 1000 |  6,994.6 ns |  21.75 ns |  18.16 ns |  6,998.2 ns |      - |      - |      40 B |
|                      AggregateAsMutableICollection | 1000 |  6,975.2 ns |  75.48 ns |  70.61 ns |  6,952.9 ns |      - |      - |      40 B |
|                             AggregateAsIEnumerable | 1000 |  7,275.0 ns | 101.48 ns |  84.74 ns |  7,313.5 ns |      - |      - |      40 B |
|                            AggregateAsMutableArray | 1000 |  5,554.5 ns |  59.35 ns |  52.61 ns |  5,531.7 ns | 0.9613 |      - |   8,056 B |
|                     AggregateAsIReadOnlyCollection | 1000 |  7,072.0 ns |  35.33 ns |  29.50 ns |  7,073.5 ns | 0.0076 |      - |      64 B |
|                           AggregateAsReadOnlyArray | 1000 |  5,552.7 ns |  84.43 ns |  78.98 ns |  5,525.8 ns | 0.9613 |      - |   8,080 B |
|  AggregateAsImmutableListCastToIReadOnlyCollection | 1000 | 37,583.0 ns | 242.63 ns | 215.08 ns | 37,590.4 ns | 5.7373 | 0.4272 |  48,120 B |
|                          AggregateAsIImmutableList | 1000 | 37,762.3 ns | 370.28 ns | 328.25 ns | 37,659.5 ns | 5.7373 | 0.4272 |  48,120 B |
| AggregateAsImmutableArrayCastToIReadOnlyCollection | 1000 |  5,600.3 ns |  67.80 ns |  60.10 ns |  5,606.4 ns | 0.9613 |      - |   8,080 B |
|                          AggregateAsImmutableArray | 1000 |  2,148.6 ns |  39.12 ns |  92.98 ns |  2,112.7 ns | 0.9575 |      - |   8,024 B |

#### Observations

* The difference that was present in the Iteration benchmark between interface and implementation types isn't there
* `ImmutableList` is still the worst
* `ImmutableArray` is the best performance of all (as long as not cast to `IReadOnlyCollection`)

#### Conclusions

The same as in the Iteration benchmark, except here there's no penalty for defining your APIs with `IReadOnlyCollection` / `ICollection` / `IEnumerable` if keeping the collection as a `List`. Also, always prefer `ToImmutableArray` over `ToArray` as both memory consumption and performance is better.
