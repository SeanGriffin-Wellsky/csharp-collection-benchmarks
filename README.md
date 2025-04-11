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

## Benchmark Results

See [IterationBenchmarkResults](IterationBenchmarkResults.md) for the results and conclusions for collection iteration.

See [LinqBenchmarkResults](LinqBenchmarkResults.md) for the results and conclusions for collection aggregation using LINQ.

## Old benchmarks

An older version of this project had results from .NET Core 3.1 and were ran on an older machine. See [OldBenchmarkResults](OldBenchmarkResults.md) for those historical results.