namespace Implementation
{
    public class Element
    {
        public int Priority { get; set; }

        public int Value { get; set; }

        public Element(int value, int priority)
        {
            Value = value;
            Priority = priority;
        }
    }
}
