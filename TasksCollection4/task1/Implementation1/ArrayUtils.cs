using System;

namespace Implementation1
{
    public class ArrayUtils
    {
        public static int[] ConcatenateInterleavedArrays(int[] first, int[] second)
        {
            int[] result = new int[first.Length + second.Length];
            int j = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (i % 2 == 0)
                {
                    result[i] = first[j];
                }
                else
                {
                    result[i] = second[j];
                    j++;
                }
            }
            return result;
        }

        public static int[] FindMinRow(int[,] array)
        {
            int minSum = CalculateRowSum(array, 0);
            int minRowIndex = 0;
            for (int i = 1; i < array.GetLength(0); i++)
            {
                int current = CalculateRowSum(array, i);
                if (current < minSum)
                {
                    minSum = current;
                    minRowIndex = i;
                }
            }
            return GetRowFromArray(array, minRowIndex);
        }

        public static int[] FindMaxRow(int[,] array)
        {
            int maxSum = CalculateRowSum(array, 0);
            int maxRowIndex = 0;
            for (int i = 1; i < array.GetLength(0); i++)
            {
                int current = CalculateRowSum(array, i);
                if (current > maxSum)
                {
                    maxSum = current;
                    maxRowIndex = i;
                }
            }
            return GetRowFromArray(array, maxRowIndex);
        }

        public static int[,] GenerateRandomArray(int n, int m)
        {
            Random random = new Random();
            int[,] result = new int[n, m];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = random.Next(0, 10);
                }
            }
            return result;
        }

        private static int[] GetRowFromArray(int[,] array, int rowIndex)
        {
            int[] result = new int[array.GetLength(1)];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = array[rowIndex, i];
            }
            return result;
        }

        private static int CalculateRowSum(int[,] array, int row)
        {
            int sum = 0;
            for (int i = 0; i < array.GetLength(1); i++)
            {
                sum += array[row, i];
            }
            return sum;
        }
    }
}
