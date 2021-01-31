using System;
using System.Drawing;

namespace Implementation
{
    public class Painter
    {
        public PainterData Data { get; set; }

        public Element[] Elements { get; set; }

        public Painter(PainterData data, Element[] elements)
        {
            Data = data;
            Elements = elements;
        }

        public void Paint(int n, int m)
        {
            const int indent = 15;
            string number;
            SizeF size;
            Data.GraphicsBitmap.Clear(Color.White);
            Pen pen = new Pen(Color.Black, 1);
            Font font = new Font("Microsoft Sans Serif", 12F);
            for (int i = 0; i < Elements.Length; i++)
            {
                pen.Color = Elements[i].Color;
                Data.GraphicsBitmap.DrawEllipse(pen, Elements[i].X - indent,
                Elements[i].Y - indent, 2 * indent, 2 * indent);
                number = Convert.ToString(Elements[i].Value);
                size = Data.GraphicsBitmap.MeasureString(number, font);
                Data.GraphicsBitmap.DrawString(number, font, Brushes.Black,
                Elements[i].X - size.Width / 2,
                Elements[i].Y - size.Height / 2);
            }
            if (n > 0)
            {
                pen.Color = Color.Black;
                number = "i = " + Convert.ToString(n);
                size = Data.GraphicsBitmap.MeasureString(number, font);
                Data.GraphicsBitmap.DrawString(number, font, Brushes.Black, 110,
                Elements[n].Y - size.Height / 2);
            }
            if (m > 0)
            {
                pen.Color = Color.Red;
                number = "j = " + Convert.ToString(m);
                size = Data.GraphicsBitmap.MeasureString(number, font);
                Data.GraphicsBitmap.DrawString(number, font, Brushes.Black, 110,
                Elements[m].Y - size.Height / 2);
            }
            Data.GraphicsScreen.DrawImage(Data.Bitmap, Data.ClientRectangle);
        }
    }
}
