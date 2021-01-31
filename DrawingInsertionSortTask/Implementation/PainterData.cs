using System.Drawing;

namespace Implementation
{
    public class PainterData
    {
        public Bitmap Bitmap { get; set; }

        public Rectangle ClientRectangle { get; set; }

        public Graphics GraphicsBitmap { get; set; }

        public Graphics GraphicsScreen { get; set; }

        public PainterData(Bitmap bitmap, Rectangle clientRectangle, Graphics graphicsBitmap, Graphics graphicsScreen)
        {
            Bitmap = bitmap;
            ClientRectangle = clientRectangle;
            GraphicsBitmap = graphicsBitmap;
            GraphicsScreen = graphicsScreen;
        }
    }
}
