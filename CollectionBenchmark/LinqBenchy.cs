using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CollectionBenchmark
{
    [MemoryDiagnoser(false)]
    public class LinqBenchy
    {
        [Params(100, 1000)] public int N;

        private List<long> data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new List<long>(N);
            for (int i = 0; i < N; ++i)
            {
                data.Add(i);
            }
        }

        [Benchmark]
        public long AggregateAsMutableList()
        {
            List<long> lst = data;
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsMutableIList()
        {
            IList<long> lst = data;
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsMutableICollection()
        {
            ICollection<long> lst = data;
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsIEnumerable()
        {
            IEnumerable<long> lst = data;
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsMutableArray()
        {
            long[] lst = data.ToArray();
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsIReadOnlyCollection()
        {
            IReadOnlyCollection<long> lst = data.AsReadOnly();
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsReadOnlyArray()
        {
            IReadOnlyCollection<long> lst = Array.AsReadOnly(data.ToArray());
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsImmutableListCastToIReadOnlyCollection()
        {
            IReadOnlyCollection<long> lst = data.ToImmutableList();
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsIImmutableList()
        {
            IImmutableList<long> lst = data.ToImmutableList();
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsImmutableArrayCastToIReadOnlyCollection()
        {
            IReadOnlyCollection<long> lst = data.ToImmutableArray();
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsImmutableArray()
        {
            ImmutableArray<long> lst = data.ToImmutableArray();
            return lst.Aggregate((x, y) => x + y);
        }
    }
}