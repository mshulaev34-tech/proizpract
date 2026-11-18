using proizpract.Common;
using System;
using System.Threading.Tasks;

namespace proizpract.Tasks.Level3
{
    [Common.TaskInfoAttribute("Level-3", 40)]
    internal class Task40_AsyncDownload : ITask
    {
        public string Title => "async/await + обработка исключений";

        public string Description => "Асинхронный метод с эмуляцией скачивания и обработкой ошибок.";

        public string[] Requirements => new string[]
        {
            "async Task<string> DownloadAsync(string url) — внутри Task.Delay(1000)",
            "Имитировать ошибку: если url пустой — бросить ArgumentException",
            "В Main вызвать через await внутри try/catch",
            "Продемонстрировать успешный и ошибочный вызов"
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
            Console.WriteLine("=== Успешный вызов ===");
            await TryDownload("https://example.com/data.json");

            Console.WriteLine("\n=== Ошибочный вызов (пустой URL) ===");
            await TryDownload("");

            Console.WriteLine("\n=== Ошибочный вызов (null URL) ===");
            await TryDownload(null);

            Console.Write("\nВведите свой URL для скачивания (или оставьте пустым для ошибки): ");
            string userInput = Console.ReadLine();
            await TryDownload(userInput);
        }

        /// <summary>
        /// Attempts to download from the specified URL and handles any exceptions.
        /// </summary>
        private async Task TryDownload(string url)
        {
            try
            {
                Console.WriteLine($"Пытаемся скачать: \"{url ?? "<null>"}\"...");
                string result = await DownloadAsync(url);
                Console.WriteLine($"[Успех] Получено: {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[Поймано ArgumentException]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Поймано {ex.GetType().Name}]: {ex.Message}");
            }
        }

        /// <summary>
        /// Simulates downloading content from a URL with a 1-second delay.
        /// Throws ArgumentException if the URL is null or empty.
        /// </summary>
        private async Task<string> DownloadAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL не может быть пустым или null.", nameof(url));
            }

            // Emulate network delay
            await Task.Delay(1000);

            return $"Данные с {url} (размер: {url.Length * 1024} байт)";
        }
    }
}