namespace Implementation
{
    public class ArrayQueue : IPriorityQueue
    {
        private readonly Element[] _items;

        public int Size { get; set; }

        public ArrayQueue(int capacity)
        {
            _items = new Element[capacity];
            Size = 0;
        }

        public Element ExtractMax()
        {
            Element max = _items[Size - 1];
            _items[Size - 1] = null;
            Size--;
            return max;
        }

        public void Increase(int value, int priority)
        {
            for (int i = 0; i < Size; i++)
            {
                if (_items[i].Value == value)
                {
                    _items[i].Priority += priority;
                    break;
                }
            }
            Sort();
        }


        public void Insert(int value, int priority)
        {
            _items[Size] = new Element(value, priority);
            Size++;
            Sort();
        }

        private void Sort()
        {
            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;
                for (int i = 1; i < Size; i++)
                {
                    if (_items[i].Priority < _items[i - 1].Priority)
                    {
                        Swap(i, i - 1);
                        isSorted = false;
                    }
                }
            }
        }

        private void Swap(int i, int j)
        {
            Element tmp = _items[i];
            _items[i] = _items[j];
            _items[j] = tmp;
        }
    }
}
