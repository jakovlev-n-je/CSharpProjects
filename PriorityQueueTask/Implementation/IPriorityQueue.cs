namespace Implementation
{
    public interface IPriorityQueue
    {
        Element ExtractMax();

        void Increase(int value, int priority);

        void Insert(int value, int priority);
    }
}
