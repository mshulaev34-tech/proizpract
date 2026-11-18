using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace proizpract.Tasks.Level3
{
    [Common.TaskInfoAttribute("Level-3", 41)]
    internal class Task41_TaskWhenAll : ITask
    {
        public string Title => "Task.WhenAll — параллельно 10 задач";

        public string Description => "Сравнение последовательного и параллельного выполнения 10 задач.";

        public string[] Requirements => new string[]
        {
            "Метод Task<int> WorkAsync(int id) — Task.Delay(500), возвращает id",
            "Последовательный запуск 10 задач в цикле с await",
            "Параллельный запуск через Task.WhenAll",
            "Замерить время обоих вариантов через Stopwatch",
            "Вывести оба времени и объяснить разницу"
        };

        public void Run()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Async entry point. Wrapped because ITask.Run() is synchronous.
        /// </summary>
        private async Task RunAsync()
        {
            const int TaskCount = 10;
            const int DelayMs = 500;

            Console.WriteLine($"Количество задач: {TaskCount}");
            Console.WriteLine($"Задержка каждой задачи: {DelayMs} мс\n");

            // === Execution ===
            Console.WriteLine("=== Последовательное выполнение ===");
            Stopwatch sw = Stopwatch.StartNew();
            List<int> sequentialResults = new List<int>();

            for (int i = 1; i <= TaskCount; i++)
            {
                int result = await WorkAsync(i);
                sequentialResults.Add(result);
            }

            sw.Stop();
            long sequentialTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"Результаты: [{string.Join(", ", sequentialResults)}]");
            Console.WriteLine($"Время: {sequentialTime} мс\n");

            // === Paralel run ===
            Console.WriteLine("=== Параллельное выполнение (Task.WhenAll) ===");
            sw.Restart();

            List<Task<int>> tasks = new List<Task<int>>();
            for (int i = 1; i <= TaskCount; i++)
            {
                tasks.Add(WorkAsync(i));
            }

            int[] parallelResults = await Task.WhenAll(tasks);
            sw.Stop();
            long parallelTime = sw.ElapsedMilliseconds;

            Console.WriteLine($"Результаты: [{string.Join(", ", parallelResults)}]");
            Console.WriteLine($"Время: {parallelTime} мс\n");

            // === equals ===
            Console.WriteLine("=== Сравнение ===");
            Console.WriteLine($"Последовательно: {sequentialTime} мс");
            Console.WriteLine($"Параллельно:     {parallelTime} мс");
            Console.WriteLine($"Разница:         {sequentialTime - parallelTime} мс");

            if (parallelTime > 0)
            {
                double speedup = (double)sequentialTime / parallelTime;
                Console.WriteLine($"Ускорение:         ~{speedup:F1}x");
            }

            Console.WriteLine("\n=== Объяснение ===");
            Console.WriteLine("Последовательное выполнение:");
            Console.WriteLine("  - Каждая задача ждёт завершения предыдущей.");
            Console.WriteLine("  - Общее время ≈ N * Delay = 10 * 500 = 5000 мс.");
            Console.WriteLine();
            Console.WriteLine("Параллельное выполнение (Task.WhenAll):");
            Console.WriteLine("  - Все задачи запускаются одновременно.");
            Console.WriteLine("  - Общее время ≈ Delay = 500 мс (все работают параллельно).");
            Console.WriteLine("  - Task.WhenAll ждёт завершения ВСЕХ задач.");
            Console.WriteLine();
            Console.WriteLine("Вывод: параллельное выполнение в ~10 раз быстрее для I/O-операций.");

            Console.Write("\nВведите количество задач для повторного теста (или Enter для 10): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int userCount) && userCount > 0)
            {
                await RunCustomTest(userCount, DelayMs);
            }
        }

        /// <summary>
        /// Simulates an async operation with a delay. Returns the task ID.
        /// </summary>
        private async Task<int> WorkAsync(int id)
        {
            await Task.Delay(500);
            return id;
        }

        /// <summary>
        /// Runs the benchmark with a custom number of tasks.
        /// </summary>
        private async Task RunCustomTest(int taskCount, int delayMs)
        {
            Console.WriteLine($"\n=== Повторный тест ({taskCount} задач) ===");

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 1; i <= taskCount; i++)
            {
                await WorkAsync(i);
            }
            sw.Stop();
            long seqTime = sw.ElapsedMilliseconds;

            sw.Restart();
            List<Task<int>> tasks = new List<Task<int>>();
            for (int i = 1; i <= taskCount; i++)
            {
                tasks.Add(WorkAsync(i));
            }
            await Task.WhenAll(tasks);
            sw.Stop();
            long parTime = sw.ElapsedMilliseconds;

            Console.WriteLine($"Последовательно: {seqTime} мс");
            Console.WriteLine($"Параллельно:     {parTime} мс");
            Console.WriteLine($"Ускорение:       ~{(double)seqTime / parTime:F1}x");
        }
    }
}