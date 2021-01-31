using System.Drawing;

namespace Implementation
{
    public class Element
    {
        public Color Color { get; set; }

        public int Value { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Element(int x, int y, int value, Color color)
        {
            X = x;
            Y = y;
            Value = value;
            Color = color;
        }
    }
}
