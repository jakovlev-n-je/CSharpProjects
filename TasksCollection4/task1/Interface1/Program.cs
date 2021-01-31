using Implementation1;
using System;

namespace Interface1
{
    public class Program
    {
        public static void Main()
        {
            Console.Write("Введите число строк в исходной матрице: ");
            int n = Convert.ToInt32(Console.ReadLine());
            if (n < 1)
            {
                Console.WriteLine("\nЧисло строк в исходной таблице должно быть положительным! Попробуйте еще раз!\n");
                Main();
            }
            Console.Write("\nВведите число столбцов в исходной матрице: ");
            int m = Convert.ToInt32(Console.ReadLine());
            if (m < 1)
            {
                Console.WriteLine("\nЧисло столбцов в исходной таблице должно быть положительным! Попробуйте еще раз!\n");
                Main();
            }
            int[,] array = ArrayUtils.GenerateRandomArray(n, m);
            Console.WriteLine("\nИсходная матрица:");
            PrintTwoDimensionalArray(array);
            Console.WriteLine("\nСтрока с минимальной суммой элементов:");
            int[] minRow = ArrayUtils.FindMinRow(array);
            PrintOneDimensionalArray(minRow);
            Console.WriteLine("\nСтрока с максимальной суммой элементов:");
            int[] maxRow = ArrayUtils.FindMaxRow(array);
            PrintOneDimensionalArray(maxRow);
            Console.WriteLine("\nСтрока с чередующимися элементами:");
            PrintOneDimensionalArray(ArrayUtils.ConcatenateInterleavedArrays(minRow, maxRow));
            Console.Write("\nВведите 0, чтобы завершить работу программы и 1, чтобы продолжить: ");
            switch (Console.ReadLine())
            {
                case "0":
                    return;
                case "1":
                    Console.WriteLine();
                    Main();
                    break;
                default:
                    Console.Write("\nНеизвестная команда! Нажмите любую клавишу чтобы завершить работу программы...");
                    Console.ReadKey();
                    return;
            }
        }

        public static void PrintOneDimensionalArray(int[] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }

        public static void PrintTwoDimensionalArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
