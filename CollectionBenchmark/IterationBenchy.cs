using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CollectionBenchmark
{
    [MemoryDiagnoser]
    public class IterationBenchy
    {
        [Params(100, 1000)] public int N;

        private List<string> data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new List<string>(N);
            for (int i = 0; i < N; ++i)
            {
                data.Add("item" + 1);
            }
        }

#if NET6_0_OR_GREATER
        [Benchmark]
        public int IterateAsSpan()
        {
            List<string> lst = data;

            var accumulator = 0;
            foreach (string s in CollectionsMarshal.AsSpan(lst)) accumulator++;
            return accumulator;
        }
#endif

        [Benchmark]
        public int IterateAsMutableList()
        {
            List<string> lst = data;

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        // [Benchmark]
        // public int IterateAsMutableIList()
        // {
        //     IList<string> lst = data;
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
        //
        // [Benchmark]
        // public int IterateAsMutableICollection()
        // {
        //     ICollection<string> lst = data;
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
        //
        // [Benchmark]
        // public int IterateAsIEnumerable()
        // {
        //     IEnumerable<string> lst = data;
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
        //
        // [Benchmark]
        // public int IterateAsMutableArray()
        // {
        //     string[] lst = data.ToArray();
        //
        //     var x = lst.ElementAtOrDefault(1);
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
        //
        // [Benchmark]
        // public int IterateAsIReadOnlyCollection()
        // {
        //     IReadOnlyCollection<string> lst = data.AsReadOnly();
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
        //
        // [Benchmark]
        // public int IterateAsReadOnlyArray()
        // {
        //     IReadOnlyCollection<string> lst = Array.AsReadOnly(data.ToArray());
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
        //
        // [Benchmark]
        // public int IterateAsImmutableListCastToIReadOnlyCollection()
        // {
        //     IReadOnlyCollection<string> lst = data.ToImmutableList();
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
        //
        // [Benchmark]
        // public int IterateAsIImmutableList()
        // {
        //     IImmutableList<string> lst = data.ToImmutableList();
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
        //
        // [Benchmark]
        // public int IterateAsImmutableArrayCastToIReadOnlyCollection()
        // {
        //     IReadOnlyCollection<string> lst = data.ToImmutableArray();
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
        //
        // [Benchmark]
        // public int IterateAsImmutableArray()
        // {
        //     ImmutableArray<string> lst = data.ToImmutableArray();
        //
        //     var accumulator = 0;
        //     foreach (string s in lst) accumulator++;
        //     return accumulator;
        // }
    }
}