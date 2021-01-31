using Implementation1;
using System;

namespace Interface1
{
    public class Program
    {
        public static void Main()
        {
            Console.Write("Введите размерность матрицы: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int[,] array = ArrayUtils.GenerateRandomArray(n);
            Console.WriteLine("\nСгенерированная матрица: \n");
            PrintResultArray(array);
            Console.WriteLine("\nСумма строк, где элементы, расположенные на главной диагонали, равны нулю: ");
            PrintResultArray(ArrayUtils.FindElementsSum(array));
            Console.ReadKey();
        }

        public static void PrintResultArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                string sum = array[i] == -1 ? "элемент, расположенный на главной диагонали, не равен нулю" : array[i].ToString();
                Console.Write($"\nНомер строки: {i}; Сумма элементов: {sum} ");
            }
            Console.WriteLine();
        }

        public static void PrintResultArray(int[,] array)
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
