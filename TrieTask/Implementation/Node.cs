namespace Implementation
{
    public class Node
    {
        public char Key { get; set; }

        public Node[] Subnodes { get; set; }

        public int Value { get; set; }

        public Node(char symbol = '\0', int value = 0)
        {
            Key = symbol;
            Subnodes = new Node[32];
            Value = value;
        }
    }
}
