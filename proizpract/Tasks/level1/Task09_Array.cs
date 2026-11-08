using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1")]
    internal class Task09_Array
    {
        public string Title => " Массивы: min/max/sum/avg";

        public string Description => "для массива чисел найти минимум, максимум, сумму и среднее.";

        public string[] Requirements => new[] { "Заполнить массив (например, случайными числами 1–100, размер 20)",
            "Реализовать без LINQ (Min/Max/Sum/Average запрещены).",
            "Оформить 4 отдельных метода.",
            "Пустой массив — обработать (вернуть ошибку или 0, но не падать)."
        };

        public void Run()
        {
            int[] array = GenerateArray(20, 1, 100);

            Console.WriteLine("Массив:");
            PrintArray(array);

            Console.WriteLine($"\nМинимум: {GetMin(array)}");
            Console.WriteLine($"Максимум: {GetMax(array)}");
            Console.WriteLine($"Сумма: {GetSum(array)}");
            Console.WriteLine($"Среднее: {GetAverage(array):F2}");
        }

        private int[] GenerateArray(int size, int min, int max)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(min, max + 1);
            }
            return array;
        }

        private void PrintArray(int[] array)
        {
            Console.Write("[");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                if (i < array.Length - 1) Console.Write(", ");
            }
            Console.WriteLine("]");
        }

        private int GetMin(int[] array)
        {
            if (array.Length == 0) return 0;
            int min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min) min = array[i];
            }
            return min;
        }

        private int GetMax(int[] array)
        {
            if (array.Length == 0) return 0;
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max) max = array[i];
            }
            return max;
        }

        private int GetSum(int[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }

        private double GetAverage(int[] array)
        {
            if (array.Length == 0) return 0;
            return (double)GetSum(array) / array.Length;
        }
    }
}
