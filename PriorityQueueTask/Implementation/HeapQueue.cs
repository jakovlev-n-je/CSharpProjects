namespace Implementation
{
    public class HeapQueue : IPriorityQueue
    {
        private readonly Element[] _items;

        public int Size { get; set; }

        public HeapQueue(int capacity)
        {
            _items = new Element[capacity];
            Size = 0;
        }

        public Element ExtractMax()
        {
            Element max = _items[0];
            _items[0] = _items[Size - 1];
            _items[Size - 1] = null;
            Size--;
            Heapify();
            return max;
        }

        public void Increase(int value, int priority)
        {
            for (int i = 0; i < Size; i++)
            {
                if (_items[i].Value == value)
                {
                    _items[i].Priority += priority;
                    Balance(i, (i - 1) / 2);
                    break;
                }
            }
        }

        public void Insert(int value, int priority)
        {
            _items[Size] = new Element(value, priority);
            Balance(Size, (Size - 1) / 2);
            Size++;
        }

        private void Balance(int childIndex, int parentIndex)
        {
            while (childIndex > 0 && _items[parentIndex].Priority < _items[childIndex].Priority)
            {
                Swap(childIndex, parentIndex);
                childIndex = parentIndex;
                parentIndex = (childIndex - 1) / 2;
            }
        }

        private void Heapify()
        {
            int current = 0;
            int left;
            int right;
            int largest;
            while (current < Size)
            {
                left = 2 * current + 1;
                right = 2 * current + 2;
                largest = current;
                if (left < Size && _items[left].Priority > _items[largest].Priority)
                {
                    largest = left;
                }
                if (right < Size && _items[right].Priority > _items[largest].Priority)
                {
                    largest = right;
                }
                if (largest == current)
                {
                    break;
                }
                Swap(current, largest);
                current = largest;
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
