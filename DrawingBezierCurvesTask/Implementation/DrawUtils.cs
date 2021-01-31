using System;
using System.Drawing;
using System.Collections.Generic;

namespace Impementation
{
    public class DrawUtils
    {
        public static void Draw(List<Point> startPoints, Graphics g)
        {
            List<Point> endPoints = new List<Point>();
            for (double t = 0; t <= 1; t += 0.00001)
                endPoints.Add(CalculateBezierFunction(t, startPoints));
            DrawCurve(endPoints, g);
            g.Dispose();
        }

        private static void DrawCurve(List<Point> points, Graphics g)
        {
            for (int i = 1; i < points.Count; i++)
            {
                int x0 = points[i - 1].X;
                int y0 = points[i - 1].Y;
                int x1 = points[i].X;
                int y1 = points[i].Y;
                g.DrawLine(new Pen(Color.Black, 3), new Point(x0, y0), new Point(x1, y1));
            }
            g.Dispose();
        }

        private static Point CalculateBezierFunction(double t, List<Point> points)
        {
            double x = 0;
            double y = 0;
            int n = points.Count - 1;
            for (int i = 0; i <= n; i++)
            {
                x += GetFactorial(n) / (GetFactorial(i) * GetFactorial(n - i)) * points[i].X * Math.Pow(t, i) * Math.Pow(1 - t, n - i);
                y += GetFactorial(n) / (GetFactorial(i) * GetFactorial(n - i)) * points[i].Y * Math.Pow(t, i) * Math.Pow(1 - t, n - i);
            }
            return new Point((int)x, (int)y);
        }

        private static double GetFactorial(double value)
        {
            if (value < 0)
            {
                throw new Exception();
            }
            return value == 0 ? 1 : value * GetFactorial(value - 1);
        }
    }
}

