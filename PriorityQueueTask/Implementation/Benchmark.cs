using System;
using System.Diagnostics;

namespace Implementation
{
    public class Benchmark
    {
        public Stopwatch Stopwatch { get; set; }

        public Benchmark()
        {
            Stopwatch = new Stopwatch();
        }

        public BenchmarkResult[] CalculateExtractMaxTicks(QueueType type)
        {
            BenchmarkResult[] results = new BenchmarkResult[10];
            IPriorityQueue queue;
            for (int i = 1; i <= 10; i++)
            {
                int capacity = i * 100;
                if (type == QueueType.ArrayQueue)
                {
                    queue = new ArrayQueue(capacity);
                }
                else
                {
                    queue = new HeapQueue(capacity);
                }
                FillQueue(queue, capacity);
                long ticks = 0;
                for (int j = 0; j < 100; j++)
                {
                    Stopwatch.Reset();
                    Stopwatch.Start();
                    queue.ExtractMax();
                    Stopwatch.Stop();
                    ticks += Stopwatch.ElapsedTicks;

                }
                results[i - 1] = new BenchmarkResult(capacity, ticks / 100);
            }
            return results;
        }

        public BenchmarkResult[] CalculateIncreaseTicks(QueueType type)
        {
            Stopwatch.Reset();
            BenchmarkResult[] results = new BenchmarkResult[10];
            IPriorityQueue queue;
            for (int i = 1; i <= 10; i++)
            {
                int capacity = i * 100;
                if (type == QueueType.ArrayQueue)
                {
                    queue = new ArrayQueue(capacity);
                }
                else
                {
                    queue = new HeapQueue(capacity);
                }
                FillQueue(queue, capacity - 1);
                queue.Insert(10, 10);
                long ticks = 0;
                for (int j = 0; j < 100; j++)
                {
                    Stopwatch.Start();
                    queue.Increase(10, 1);
                    Stopwatch.Stop();
                    ticks += Stopwatch.ElapsedTicks;
                }
                results[i - 1] = new BenchmarkResult(capacity, ticks / 100);
            }
            return results;
        }

        public BenchmarkResult[] CalculateInsertTicks(QueueType type)
        {
            Stopwatch.Reset();
            BenchmarkResult[] results = new BenchmarkResult[10];
            Random random = new Random();
            IPriorityQueue queue;
            for (int i = 1; i <= 10; i++)
            {
                int capacity = i * 100;
                if (type == QueueType.ArrayQueue)
                {
                    queue = new ArrayQueue(capacity + 100);
                }
                else
                {
                    queue = new HeapQueue(capacity + 100);
                }
                long ticks = 0;
                FillQueue(queue, capacity);
                for (int j = 0; j < 100; j++)
                {
                    int value = random.Next(0, capacity * 10);
                    int priority = random.Next(0, capacity * 10);
                    Stopwatch.Start();
                    queue.Insert(value, priority);
                    Stopwatch.Stop();
                    ticks += Stopwatch.ElapsedTicks;
                }
                results[i - 1] = new BenchmarkResult(capacity, ticks / 100);
            }
            return results;
        }

        private void FillQueue(IPriorityQueue queue, int endIndex)
        {
            Random random = new Random();
            int interval = endIndex * 10;
            for (int i = 0; i < endIndex; i++)
            {
                queue.Insert(random.Next(0, interval), random.Next(0, interval));
            }
        }
    }
}
