using proizpract.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace proizpract.Tasks.Level3
{
    [Common.TaskInfoAttribute("Level-3", 42)]
    internal class Task42_CancellationToken : ITask
    {
        public string Title => "CancellationToken — отмена через 2 сек";

        public string Description => "Реализация долгой операции с поддержкой отмены через CancellationToken.";

        public string[] Requirements => new string[]
        {
            "async Task LongOperationAsync(CancellationToken token) — цикл с Task.Delay(500, token) 10 раз",
            "В Main создать CancellationTokenSource, запустить операцию, отменить через 2 секунды",
            "Обработать OperationCanceledException",
            "Вывести «операция отменена»"
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
            Console.WriteLine("=== Демонстрация отмены операции ===\n");

            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                Console.WriteLine("Запускаем долгую операцию (10 итераций по 500 мс)...");
                Console.WriteLine("Отмена запланирована через 2000 мс.\n");

                Task operationTask = LongOperationAsync(cts.Token);

                cts.CancelAfter(2000);

                try
                {
                    await operationTask;
                    Console.WriteLine("Операция завершена успешно.");
                }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine($"\n[Поймано OperationCanceledException]: {ex.Message}");
                    Console.WriteLine("Операция отменена.");
                }
            }

            Console.Write("\nВведите время отмены в мс (или Enter для 2000): ");
            string input = Console.ReadLine();
            int cancelAfterMs = 2000;

            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int userMs) && userMs > 0)
            {
                cancelAfterMs = userMs;
            }

            Console.WriteLine($"\n=== Повторный тест с отменой через {cancelAfterMs} мс ===");
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                Task operationTask = LongOperationAsync(cts.Token);
                cts.CancelAfter(cancelAfterMs);

                try
                {
                    await operationTask;
                    Console.WriteLine("Операция завершена успешно.");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Операция отменена.");
                }
            }
        }

        /// <summary>
        /// Simulates a long-running operation that checks for cancellation on each iteration.
        /// Throws OperationCanceledException if the token is cancelled.
        /// </summary>
        private async Task LongOperationAsync(CancellationToken token)
        {
            for (int i = 1; i <= 10; i++)
            {
                token.ThrowIfCancellationRequested();

                Console.WriteLine($"Итерация {i}/10...");

                await Task.Delay(500, token);
            }

            Console.WriteLine("Все 10 итераций завершены.");
        }
    }
}