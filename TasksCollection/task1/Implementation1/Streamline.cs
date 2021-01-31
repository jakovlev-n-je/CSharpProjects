namespace Implementation1
{
    public class Streamline
    {
        public static int[,] StreamlineArray(int[,] arr)
        {
            return StreamlineColumns(StreamlineRows(arr));
        }

        private static int[,] StreamlineColumns(int[,] arr)
        {
            for (int i = arr.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 0)
                    {
                        SwapColumns(arr, i, j);
                    }
                }
            }

            return arr;
        }

        private static int[,] StreamlineRows(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = arr.GetLength(1) - 1; j >= 0; j--)
                {
                    if (arr[i, j] == 0)
                    {
                        SwapRows(arr, i, j);
                    }
                }
            }

            return arr;
        }

        private static void SwapColumns(int[,] arr, int i, int j)
        {
            while (i + 1 < arr.GetLength(0) && arr[i + 1, j] != 0)
            {
                int tmp = arr[i + 1, j];
                arr[i + 1, j] = arr[i, j];
                arr[i, j] = tmp;
                i++;
            }
        }

        private static void SwapRows(int[,] arr, int i, int j)
        {
            while (j + 1 < arr.GetLength(1) && arr[i, j + 1] != 0)
            {
                int tmp = arr[i, j + 1];
                arr[i, j + 1] = arr[i, j];
                arr[i, j] = tmp;
                j++;
            }
        }
    }
}
