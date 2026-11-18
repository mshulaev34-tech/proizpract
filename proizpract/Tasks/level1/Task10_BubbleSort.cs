using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",10)]
    internal class Task10_BubbleSort
    {
        public string Title => "Сортировка пузырьком + вставками";

        public string Description => "отсортировать массив двумя алгоритмами.";

        public string[] Requirements => new[] { "Реализовать BubbleSort и InsertionSort отдельными методами.",
            "Оба — без LINQ и Array.Sort",
            "Замерить время каждого через Stopwatch на массиве 10 000 элементов.",
            "Вывести оба времени"
        };
        public void Run()
        {
            int size = 10000;
            int[] originalArray = GenerateRandomArray(size, 1, 100000);

            int[] arrayForBubble = (int[])originalArray.Clone();
            int[] arrayForInsertion = (int[])originalArray.Clone();

            Console.WriteLine($"Массив из {size} элементов готов к сортировке...");
            Console.WriteLine();

            long bubbleTime = MeasureTime(() => BubbleSort(arrayForBubble));
            Console.WriteLine($"Bubble Sort время: {bubbleTime} мс");

            long insertionTime = MeasureTime(() => InsertionSort(arrayForInsertion));
            Console.WriteLine($"Insertion Sort время: {insertionTime} мс");
            Console.WriteLine();
            Console.WriteLine($"Bubble Sort отсортирован корректно: {IsSorted(arrayForBubble)}");
            Console.WriteLine($"Insertion Sort отсортирован корректно: {IsSorted(arrayForInsertion)}");
        }
        private bool IsSorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1]) return false;
            }
            return true;
        }
        private void InsertionSort(int[] array)
        {
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                int key = array[i];
                int j = i - 1;
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }
        private void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        private long MeasureTime(Action sortMethod)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            sortMethod();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        private int[] GenerateRandomArray(int size, int min, int max)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(min, max);
            }
            return array;
        }
    }
}
