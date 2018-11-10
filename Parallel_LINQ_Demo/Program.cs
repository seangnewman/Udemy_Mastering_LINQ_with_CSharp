using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_LINQ_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //AsParallel_And_ParallelQuery();

            //Cancellations_And_Exceptions();

            //Merge_Options();

            CustomAggregation();
        }

        private static void CustomAggregation()
        {
            //var sum = Enumerable.Range(1, 1000).Sum();

            //var sum = Enumerable.Range(1, 1000)
            //                    .Aggregate(0, (value, accumulator) => value + accumulator);

            var sum = ParallelEnumerable.Range(1, 1000)
                                        .Aggregate(
                                                    0,
                                                    (partialAccumulator, element) => partialAccumulator += element,
                                                    (total, subtotal) => total += subtotal,
                                                    i => i
                                                    );
            Console.WriteLine($"Sum = {sum}");
        }

        private static void Merge_Options()
        {
            var numbers = Enumerable.Range(1, 20).ToArray();

            var results = numbers.AsParallel()
                                 .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                                 .Select(x =>
                                 {
                                     var result = Math.Log10(x);
                                     Console.Write($"P {result}\t");
                                     return result;
                                 });

            foreach (var result in results)
            {
                Console.Write($"C {result}\t");
            }
        }

        private static void AsParallel_And_ParallelQuery()
        {
            const int count = 50;

            var items = Enumerable.Range(1, count).ToArray();

            var results = new int[count];

            items.AsParallel().ForAll(x =>
            {
                double newValue = Math.Pow(x, 3);
                Console.Write($"{newValue} ({Task.CurrentId})\t");
                results[x - 1] = (int)newValue;

            });

            Console.WriteLine();

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item}");
            //}

            Console.WriteLine();

            var cubes = items.AsParallel()
                              .AsOrdered()
                              .Select(x => x * x * x);

            foreach (var item in cubes)
            {
                Console.WriteLine($"{item}");
            }
        }

        private static void Cancellations_And_Exceptions()
        {
            var cts = new CancellationTokenSource();

            var p_items = ParallelEnumerable.Range(1, 20);

            var p_results = p_items.WithCancellation(cts.Token).Select(i =>
            {
                double result = Math.Log10(i);

                //if(result > 1)
                //   {
                //       throw new InvalidOperationException();
                //   }


                Console.WriteLine($"i={i}, tid = {Task.CurrentId}");
                return result;
            });

            try
            {
                foreach (var c in p_results)
                {
                    if (c > 1)
                    {
                        cts.Cancel();
                    }
                    Console.WriteLine($"result = {c}");
                }
            }
            catch (AggregateException ae)
            {

                ae.Handle(e =>
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    return true;
                });
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation Cancelled");
            }
        }
    }
}
