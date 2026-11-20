using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 22)]
    internal class Task22_ParamsSum : ITask
    {
        public string Title => "params-метод + перегрузка";

        public string Description => "Написание метода суммы с params и его перегрузки для коллекции.";

        public string[] Requirements => new string[]
        {
            "Sum(params int[] numbers) — сумма произвольного числа чисел",
            "Перегрузка Sum(IEnumerable<int> numbers) — для коллекции",
            "Продемонстрировать: Sum(1, 2, 3), Sum(new[] {1, 2, 3}), Sum(), Sum(list)",
            "Пустой вызов — вернуть 0"
        };

        public void Run()
        {
            Console.WriteLine("=== Демонстрация четырёх вариантов вызова ===");
            Console.WriteLine($"Sum(1, 2, 3)               = {Sum(1, 2, 3)}");
            Console.WriteLine($"Sum(new[] {{1, 2, 3}})     = {Sum(new[] { 1, 2, 3 })}");
            Console.WriteLine($"Sum()                      = {Sum()}");

            List<int> numbersList = new List<int> { 10, 20, 30 };
            Console.WriteLine($"Sum(List<int> {{10,20,30}}) = {Sum(numbersList)}");

            Console.Write("\nВведите числа через пробел (или оставьте пустым): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"Пустой ввод → Sum() = {Sum()}");
            }
            else
            {
                string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int[] numbers = new int[parts.Length];
                for (int i = 0; i < parts.Length; i++)
                {
                    if (!int.TryParse(parts[i], out numbers[i]))
                    {
                        Console.WriteLine($"Не удалось распарсить \"{parts[i]}\" как число.");
                        return;
                    }
                }
                Console.WriteLine($"Sum(введённые числа) = {Sum(numbers)}");
            }
        }

        /// <summary>
        /// Calculates the sum of an arbitrary number of integers passed as arguments.
        /// The 'params' keyword allows calling this method as Sum(1, 2, 3).
        /// </summary>
        public int Sum(params int[] numbers)
        {
            return Sum((IEnumerable<int>)numbers);
        }

        /// <summary>
        /// Calculates the sum of integers from any IEnumerable collection.
        /// This is a true overload (different signature from params int[]).
        /// </summary>
        public int Sum(IEnumerable<int> numbers)
        {
            if (numbers == null)
            {
                return 0;
            }

            int total = 0;
            foreach (int number in numbers)
            {
                total += number;
            }
            return total;
        }
    }
}