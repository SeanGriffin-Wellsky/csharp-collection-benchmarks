using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Jobs;

namespace CollectionBenchmark
{
    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.Net90)]
    [MemoryDiagnoser(false)]
    public class LinqBenchy
    {
        [Params(100, 1000)] public int N;

        private List<long> dataAsList;
        private IEnumerable<long> dataAsIEnumerable;

        [GlobalSetup]
        public void GlobalSetup()
        {
            dataAsList = new List<long>(N);
            for (int i = 0; i < N; ++i)
            {
                dataAsList.Add(i);
            }

            dataAsIEnumerable = GetEnumerable();
        }

        private IEnumerable<long> GetEnumerable()
        {
            for (int i = 0; i < N; ++i)
            {
                yield return i;
            }
        }

        [Benchmark]
        public long AggregateAsMutableList()
        {
            List<long> lst = dataAsList;
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsMutableIList()
        {
            IList<long> lst = dataAsList;
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsMutableICollection()
        {
            ICollection<long> lst = dataAsList;
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsIEnumerable()
        {
            IEnumerable<long> lst = dataAsIEnumerable;
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsIReadOnlyCollection()
        {
            IReadOnlyCollection<long> lst = dataAsList;
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsIReadOnlyCollection_AsReadOnly()
        {
            IReadOnlyCollection<long> lst = dataAsList.AsReadOnly();
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsMutableArray()
        {
            long[] lst = dataAsList.ToArray();
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsReadOnlyArray()
        {
            IReadOnlyCollection<long> lst = dataAsList.ToArray();
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsReadOnlyArray_AsReadOnly()
        {
            IReadOnlyCollection<long> lst = Array.AsReadOnly(dataAsList.ToArray());
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsImmutableList()
        {
            ImmutableList<long> lst = dataAsList.ToImmutableList();
            return lst.Aggregate((x, y) => x + y);
        }

        [Benchmark]
        public long AggregateAsImmutableArray()
        {
            ImmutableArray<long> lst = dataAsList.ToImmutableArray();
            return lst.Aggregate((x, y) => x + y);
        }
    }
}
