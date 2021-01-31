namespace Implementation
{
    public class BenchmarkResult
    {
        public int QueueCapacity { get; set; }

        public long AverageTicks { get; set; }

        public BenchmarkResult(int capacity, long averageTicks)
        {
            QueueCapacity = capacity;
            AverageTicks = averageTicks;
        }
    }
}
