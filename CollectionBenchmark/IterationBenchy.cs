using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;

namespace CollectionBenchmark
{
    [MemoryDiagnoser(false)]
    public class IterationBenchy
    {
        [Params(100, 1000)] public int N;

        private List<string> dataAsList;
        private IEnumerable<string> dataAsIEnumerable;

        [GlobalSetup]
        public void GlobalSetup()
        {
            dataAsList = new List<string>(N);
            for (int i = 0; i < N; ++i)
            {
                dataAsList.Add("item");
            }

            dataAsIEnumerable = GetEnumerable();
        }

        private IEnumerable <string> GetEnumerable()
        {
            for (int i = 0; i < N; ++i)
            {
                yield return "item";
            }
        }

#if NET6_0_OR_GREATER
        [Benchmark]
        public int IterateAsSpan()
        {
            List<string> lst = dataAsList;

            var accumulator = 0;
            foreach (string s in CollectionsMarshal.AsSpan(lst)) accumulator++;
            return accumulator;
        }
#endif

        [Benchmark]
        public int IterateAsMutableList()
        {
            List<string> lst = dataAsList;

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsMutableIList()
        {
            IList<string> lst = dataAsList;

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsMutableICollection()
        {
            ICollection<string> lst = dataAsList;

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsIEnumerable()
        {
            IEnumerable<string> lst = dataAsIEnumerable;

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsIReadOnlyCollection()
        {
            IReadOnlyCollection<string> lst = dataAsList;

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsIReadOnlyCollection_AsReadOnly()
        {
            IReadOnlyCollection<string> lst = dataAsList.AsReadOnly();

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsMutableArray()
        {
            string[] lst = dataAsList.ToArray();

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsReadOnlyArray()
        {
            IReadOnlyCollection<string> lst = dataAsList.ToArray();

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsReadOnlyArray_AsReadOnly()
        {
            IReadOnlyCollection<string> lst = Array.AsReadOnly(dataAsList.ToArray());

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsImmutableList()
        {
            ImmutableList<string> lst = dataAsList.ToImmutableList();

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }

        [Benchmark]
        public int IterateAsImmutableArray()
        {
            ImmutableArray<string> lst = dataAsList.ToImmutableArray();

            var accumulator = 0;
            foreach (string s in lst) accumulator++;
            return accumulator;
        }
    }
}
