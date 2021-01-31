using System;

namespace Implementation1
{
    public class ArrayUtils
    {
        public static int[,] GenerateRandomArray(int n, int m, int a, int b)
        {
            int[,] arr = new int[n, m];
            Random rnd = new Random();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = rnd.Next(a, b);
                }
            }
            return arr;
        }
    }
}
