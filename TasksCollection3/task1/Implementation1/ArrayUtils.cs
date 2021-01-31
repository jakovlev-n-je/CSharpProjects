using System;

namespace Implementation1
{
    public class ArrayUtils
    {
        public static int[,] GenerateRandomArray(int n)
        {
            int[,] array = new int[n, n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = random.Next(0, 8);
                }
            }
            return array;
        }

        public static int[] FindElementsSum(int[,] array)
        {
            int[] result = FindRows(array);
            for (int i = 0; i < result.Length; i++)
            {
                int sum = 0;
                if (result[i] != -1)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        sum += array[i, j];
                    }
                    result[i] = sum;
                }
            }
            return result;
        }

        private static int[] FindRows(int[,] array)
        {
            int[] result = new int[array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                result[i] = array[i, i] == 0 ? 1 : -1;
            }
            return result;
        }
    }
}
