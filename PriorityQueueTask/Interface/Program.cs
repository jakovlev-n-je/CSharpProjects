using Implementation;
using System;

namespace Interface
{
    public class Program
    {
        public static void Main()
        {
            Benchmark benchmark = new Benchmark();
            Console.WriteLine("Heap | ExtractMax: ");
            benchmark.CalculateExtractMaxTicks(QueueType.HeapQueue);
            PrintResults(benchmark.CalculateExtractMaxTicks(QueueType.HeapQueue));
            Console.WriteLine("\nArray | ExtractMax: ");
            benchmark.CalculateExtractMaxTicks(QueueType.ArrayQueue);
            PrintResults(benchmark.CalculateExtractMaxTicks(QueueType.ArrayQueue));
            Console.WriteLine("\nHeap | Increase: ");
            benchmark.CalculateIncreaseTicks(QueueType.HeapQueue);
            PrintResults(benchmark.CalculateIncreaseTicks(QueueType.HeapQueue));
            Console.WriteLine("\nArray | Increase: ");
            benchmark.CalculateIncreaseTicks(QueueType.ArrayQueue);
            PrintResults(benchmark.CalculateIncreaseTicks(QueueType.ArrayQueue));
            Console.WriteLine("\nHeap | Insert: ");
            benchmark.CalculateInsertTicks(QueueType.HeapQueue);
            PrintResults(benchmark.CalculateInsertTicks(QueueType.HeapQueue));
            Console.WriteLine("\nArray | Insert: ");
            benchmark.CalculateInsertTicks(QueueType.ArrayQueue);
            PrintResults(benchmark.CalculateInsertTicks(QueueType.ArrayQueue));
            Console.ReadKey();
        }

        public static void PrintResults(BenchmarkResult[] results)
        {
            foreach (BenchmarkResult result in results)
            {
                Console.WriteLine($"Количество тиков в среднем: {result.AverageTicks} | Элементов в очереди: {result.QueueCapacity}");
            }
        }
    }
}
