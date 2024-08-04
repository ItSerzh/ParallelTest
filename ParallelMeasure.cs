using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTest
{
    internal class ParallelMeasure(ArrayGenerator generator, IOutput output)
    {
        private int[] array;
        private const int N = -10;
        object syncFlow = new ();

        public void Measure()
        {
            output.Write($"{"Sync", N}  |{"Parallel", N}  |{"ParallelLinq", N}");
            output.Write(string.Empty);
            array = generator.GetArray(100_000);
            RunForCurrentArray();
            array = generator.GetArray(1_000_000);
            RunForCurrentArray();
            array = generator.GetArray(10_000_000);
            RunForCurrentArray();
            array = generator.GetArray(500_000_000);
            RunForCurrentArray();
            array = generator.GetArray(1_000_000_000);
            RunForCurrentArray();
        }
        
        private void RunForCurrentArray()
        {
            output.Write($"Start test with array[{array.Length:N0}]", ConsoleColor.Blue);
            var syncTime = TestOne(TestSync);
            var parallelTime = TestOne(TestParallel);
            var linqTime = TestOne(TestParallelLinq);
            output.Write($"{syncTime, N}  |{parallelTime, N}  |{linqTime, N}");
        }

        private long TestSync(bool showResult = false)
        {
            var sum = 0L;
            for (var i = 0; i< array.Length; i++)
            {
                sum += array[i];
            }

            if (showResult)
            {
                output.Write($"Sum:{sum}");
            }

            return sum;
        }

        private long TestParallel(bool showResult)
        {
            long sum = 0;
            Parallel.For(0, array.Length, parallelOptions: new ParallelOptions() { MaxDegreeOfParallelism = 3 },  i =>
            {
                Interlocked.Add(ref sum, array[i]);
                //lock(syncFlow)
                //{
                //    sum += array[i];
                //}
            });

            if (showResult)
            {
                Console.WriteLine($"Sum:{sum}");
            }
            return sum;
        }

        private long TestParallelLinq(bool showResult)
        {
            var sum = array.AsParallel().Sum(a => (long)a);
            if (showResult)
            {
                Console.WriteLine($"Sum:{sum}");
            }

            return sum;
        }

        private double TestOne(Func<bool, long> func, bool showResult = false) 
        {
            var sw = new Stopwatch();
            sw.Start();
            func(showResult);
            sw.Stop();
            return sw.Elapsed.TotalSeconds;
        }
    }
}
