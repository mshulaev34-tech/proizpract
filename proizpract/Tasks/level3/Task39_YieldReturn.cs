using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proizpract.Tasks.Level3
{
    [Common.TaskInfoAttribute("Level-3", 39)]
    internal class Task39_YieldReturn : ITask
    {
        public string Title => "yield return — генератор чётных чисел";

        public string Description => "Реализация ленивого генератора чётных чисел через yield return.";

        public string[] Requirements => new string[]
        {
            "Метод IEnumerable<int> GetEvenNumbers(int max) — чётные числа до max",
            "Реализовать через yield return, без промежуточного списка",
            "Продемонстрировать работу в foreach",
            "Показать ленивость — сообщение внутри генератора печатается по одному"
        };

        public void Run()
        {
            Console.WriteLine("=== Демонстрация ленивого выполнения ===");
            Console.WriteLine("Генератор создаётся, но ещё НЕ выполняется.\n");

            IEnumerable<int> evenNumbers = GetEvenNumbers(20);
            Console.WriteLine("Переменная evenNumbers создана. Генератор пока не запущен.\n");

            Console.WriteLine("--- Начинаем foreach ---");
            foreach (int number in evenNumbers)
            {
                Console.WriteLine($"  Получено число: {number}");
            }
            Console.WriteLine("--- foreach завершён ---\n");

            Console.WriteLine("=== Демонстрация Take(3) — ленивость в действии ===");
            Console.WriteLine("Берём только 3 первых числа из бесконечного генератора:\n");

            IEnumerable<int> infiniteEvens = GetEvenNumbers(int.MaxValue);
            var firstThree = infiniteEvens.Take(3);

            Console.WriteLine("--- Запрашиваем 3 элемента ---");
            foreach (int number in firstThree)
            {
                Console.WriteLine($"  Получено число: {number}");
            }
            Console.WriteLine("--- Остальные миллионы чисел НЕ вычислялись! ---\n");

            Console.WriteLine("=== Сумма чётных чисел до 100 ===");
            int sum = GetEvenNumbers(100).Sum();
            Console.WriteLine($"Сумма: {sum}\n");

            Console.Write("Введите максимальное значение для генератора: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int userMax) && userMax > 0)
            {
                Console.WriteLine($"Чётные числа до {userMax}:");
                foreach (int number in GetEvenNumbers(userMax))
                {
                    Console.Write($"{number} ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }

        /// <summary>
        /// Generates even numbers from 0 up to (but not including) max using yield return.
        /// This is a lazy generator — values are produced on-demand, one at a time,
        /// without creating an intermediate collection in memory.
        /// </summary>
        private IEnumerable<int> GetEvenNumbers(int max)
        {
            Console.WriteLine($"  [Генератор] Запущен с max = {max}");
            for (int i = 0; i < max; i += 2)
            {
                Console.WriteLine($"  [Генератор] Выдаю число {i}");
                yield return i;
            }
            Console.WriteLine($"  [Генератор] Завершил работу");
        }
    }
}