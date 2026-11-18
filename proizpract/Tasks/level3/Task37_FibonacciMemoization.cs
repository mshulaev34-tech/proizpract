using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace proizpract.Tasks.Level3
{
    [Common.TaskInfoAttribute("Level-3", 37)]
    internal class Task37_FibonacciMemoization : ITask
    {
        public string Title => "Мемоизация Фибоначчи через Dictionary";

        public string Description => "Сравнение обычной рекурсивной и мемоизированной версий функции Фибоначчи.";

        public string[] Requirements => new string[]
        {
            "Обычная рекурсивная версия (для сравнения на маленьком N)",
            "Мемоизированная версия с Dictionary<int, long>",
            "Замерить время обеих на N = 30–35 через Stopwatch",
            "Вывести оба времени и разницу"
        };

        private readonly Dictionary<int, long> _cache = new Dictionary<int, long>();

        public void Run()
        {
            Console.WriteLine("=== Демонстрация на малых N (проверка корректности) ===");
            for (int i = 0; i <= 10; i++)
            {
                long naive = FibonacciNaive(i);
                long memo = FibonacciMemoized(i);
                Console.WriteLine($"F({i,2}) = {memo} (совпадает: {naive == memo})");
            }

            Console.WriteLine("\n=== Замер производительности ===");
            int[] testValues = { 30, 35 };

            foreach (int n in testValues)
            {
                _cache.Clear();

                Console.WriteLine($"\n--- N = {n} ---");

                Stopwatch sw = Stopwatch.StartNew();
                long resultNaive = FibonacciNaive(n);
                sw.Stop();
                long timeNaive = sw.ElapsedMilliseconds;

                sw.Restart();
                long resultMemo = FibonacciMemoized(n);
                sw.Stop();
                long timeMemo = sw.ElapsedMilliseconds;

                Console.WriteLine($"Результат (обычная):     {resultNaive}");
                Console.WriteLine($"Результат (мемоизация):  {resultMemo}");
                Console.WriteLine($"Время (обычная):         {timeNaive} мс");
                Console.WriteLine($"Время (мемоизация):      {timeMemo} мс");
                Console.WriteLine($"Разница:                 {timeNaive - timeMemo} мс");

                if (timeMemo > 0)
                {
                    Console.WriteLine($"Ускорение:               ~{timeNaive / (double)timeMemo:F1}x");
                }
                else
                {
                    Console.WriteLine($"Ускорение:               слишком быстрое для измерения");
                }
            }

            Console.Write("\nВведите своё значение N (рекомендуется 30-40): ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int userN) && userN >= 0 && userN <= 90)
            {
                _cache.Clear();
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    long result = FibonacciMemoized(userN);
                    sw.Stop();
                    Console.WriteLine($"F({userN}) = {result}");
                    Console.WriteLine($"Время: {sw.ElapsedMilliseconds} мс");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Используйте число от 0 до 90.");
            }
        }

        /// <summary>
        /// Naive recursive Fibonacci implementation.
        /// Time complexity: O(2^n). Extremely slow for N > 35.
        /// </summary>
        private long FibonacciNaive(int n)
        {
            if (n < 0) throw new ArgumentException("N не может быть отрицательным.");
            if (n == 0) return 0;
            if (n == 1) return 1;
            return FibonacciNaive(n - 1) + FibonacciNaive(n - 2);
        }

        /// <summary>
        /// Memoized recursive Fibonacci implementation using Dictionary cache.
        /// Time complexity: O(n) on first call, O(1) on repeated calls.
        /// </summary>
        private long FibonacciMemoized(int n)
        {
            if (n < 0) throw new ArgumentException("N не может быть отрицательным.");
            if (n == 0) return 0;
            if (n == 1) return 1;

            if (_cache.TryGetValue(n, out long cached))
            {
                return cached;
            }

            long result = FibonacciMemoized(n - 1) + FibonacciMemoized(n - 2);
            _cache[n] = result;
            return result;
        }
    }
}