using BenchmarkDotNet.Running;

namespace CollectionBenchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BenchmarkRunner.Run<IterationBenchy>();
            //BenchmarkRunner.Run<LinqBenchy>(args: args);

            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args);
        }
    }
}