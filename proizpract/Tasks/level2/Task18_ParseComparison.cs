using proizpract.Common;
using System;
using System.Linq;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2" , 18)]
    internal class Task18_ParseComparison : ITask
    {
        public string Title => "TryParse vs try/catch ";

        public string Description => "Сравнение двух способов обработки некорректного ввода числа.";

        public string[] Requirements => new string[]
        {
            "Способ 1: int.TryParse — вернуть bool + значение через out",
            "Способ 2: int.Parse внутри try/catch — ловить FormatException",
            "Оба оформить отдельными методами",
            "Продемонстрировать на корректном и некорректном вводе"
        };

        public void Run()
        {
            string[] testCases = { "123", "abc", "1" };
            Console.Write("Ведите число для тестирования парсинга:\t");

            string test = Console.ReadLine();
            testCases[2] = test;

            Console.WriteLine("=== Способ 1: TryParse ===");
            foreach (string input in testCases)
            {
                int? result = ParseWithTryParse(input);
                Console.WriteLine($"\"{input}\" → {(result.HasValue ? result.Value.ToString() : "не удалось")}");
            }

            Console.WriteLine("\n=== Способ 2: Parse с try/catch ===");
            foreach (string input in testCases)
            {
                int? result = ParseWithTryCatch(input);
                Console.WriteLine($"\"{input}\" → {(result.HasValue ? result.Value.ToString() : "не удалось")}");
            }

        }

        /// <summary>
        /// Parses a string into a number using int.TryParse.
        /// </summary>
        private int? ParseWithTryParse(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Parses a string into a number using int.Parse, with exception handling
        /// </summary>
        private int? ParseWithTryCatch(string input)
        {
            try
            {
                return int.Parse(input);
            }
            catch (FormatException)
            {
                return null;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
    }
}