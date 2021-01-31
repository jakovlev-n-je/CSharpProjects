using System;
using System.Drawing;
using System.Threading;

namespace Implementation
{
    public class ArrayUtils
    {
        Painter Painter { get; set; }

        public ArrayUtils(Painter painter)
        {
            Painter = painter;
        }

        public void Sort(Element[] elements)
        {
            for (int i = 1; i < elements.Length; i++)
            {
                Element key = elements[i];
                int j = i;
                while (j >= 1 && elements[j - 1].Value > key.Value)
                {
                    Change(j - 1, j, i, j);
                    Swap(j - 1, j);
                    j--;
                }
                elements[j] = key;
            }
        }

        private void Change(int n1, int n2, int n, int m)
        {
            Painter.Elements[n1].Color = Color.Red;
            Painter.Elements[n2].Color = Color.Red;
            int x1 = Painter.Elements[n1].X;
            int y1 = Painter.Elements[n1].Y;
            int y2 = Painter.Elements[n2].Y;
            double x;
            for (int t = 1; t <= 15; t++)
            {
                x = (y2 - y1) * t / 15;
                Painter.Elements[n1].Y = y1 + (int)(x);
                switch (t)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        x = 40 * t / 4;
                        Painter.Elements[n2].X = x1 - (int)(x);
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        x = (y1 - y2) * (t - 4) / 7;
                        Painter.Elements[n2].Y = y2 + (int)(x);
                        break;
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        x = 40 * (t - 11) / 4;
                        Painter.Elements[n2].X = (int)(x1 - 40 + x);
                        break;
                }
                Painter.Paint(n, m);
                Thread.Sleep(10);
            }
            Painter.Elements[n1].Color = Color.Black;
            Painter.Elements[n2].Color = Color.Black;
            Painter.Paint(n, m);
        }

        public static Element[] GenerateElements()
        {
            Element[] elements = new Element[10];
            Random random = new Random();
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = new Element(80, 40 + 40 * i, random.Next(100), Color.Black);
            }
            return elements;
        }

        private void Swap(int i, int j)
        {
            Element tmp = Painter.Elements[i];
            Painter.Elements[i] = Painter.Elements[j];
            Painter.Elements[j] = tmp;
        }
    }
}
