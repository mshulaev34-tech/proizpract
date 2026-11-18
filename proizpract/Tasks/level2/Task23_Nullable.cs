using proizpract.Common;
using System;
using System.Collections.Generic;

namespace proizpract.Tasks.Level2
{
    /// <summary>
    /// Represents a person with optional (nullable) fields for demonstration purposes.
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string MiddleName { get; set; }

        public override string ToString()
        {
            return $"{Name} {MiddleName}".Trim();
        }
    }

    [Common.TaskInfoAttribute("Level-2", 23)]
    internal class Task23_Nullable : ITask
    {
        public string Title => "Nullable-типы, ??, ?.";

        public string Description => "Демонстрация работы с nullable-типами и операторами ??, ?., HasValue/Value.";

        public string[] Requirements => new string[]
        {
            "Метод int? FindValue(string key) — возвращает null, если ключа нет",
            "Использовать ?? для значения по умолчанию",
            "Использовать ?. для безопасного доступа",
            "Использовать HasValue / Value с проверкой",
            "Продемонстрировать на найденном и ненайденном ключе"
        };

        private readonly Dictionary<string, int> _scores = new Dictionary<string, int>
        {
            { "Alice", 95 },
            { "Bob", 78 },
            { "Charlie", 88 }
        };

        public void Run()
        {
            Console.WriteLine("=== 1. FindValue + HasValue/Value ===");
            PrintScore("Alice");
            PrintScore("Unknown");

            Console.WriteLine("\n=== 2. Оператор ?? (значение по умолчанию) ===");
            int aliceScore = FindValue("Alice") ?? 0;
            int unknownScore = FindValue("Unknown") ?? 0;
            Console.WriteLine($"Alice (с ??)   = {aliceScore}");
            Console.WriteLine($"Unknown (с ??) = {unknownScore}");

            Console.WriteLine("\n=== 3. Оператор ?. (безопасный доступ) ===");
            Person personWithAge = new Person { Name = "Ivan", MiddleName = "Ivanovich", Age = 30 };
            Person personWithoutAge = new Person { Name = "Petr", MiddleName = null };
            Person nullPerson = null;

            Console.WriteLine($"personWithAge.Age    = {personWithAge?.Age}");
            Console.WriteLine($"personWithoutAge.Age = {personWithoutAge?.Age}");
            Console.WriteLine($"nullPerson?.Age      = {nullPerson?.Age}");
            Console.WriteLine($"nullPerson?.ToString() = {nullPerson?.ToString() ?? "null"}");

            Console.Write("\nВведите имя для поиска в словаре: ");
            string userInput = Console.ReadLine();
            int userScore = FindValue(userInput) ?? -1;
            Console.WriteLine($"Результат поиска \"{userInput}\": {userScore}");
        }

        /// <summary>
        /// Finds a value by key in the internal dictionary. Returns null if the key is not found.
        /// </summary>
        private int? FindValue(string key)
        {
            if (_scores.TryGetValue(key, out int value))
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// Demonstrates HasValue / Value usage with explicit null-check.
        /// </summary>
        private void PrintScore(string name)
        {
            int? score = FindValue(name);
            if (score.HasValue)
            {
                Console.WriteLine($"{name}: {score.Value}");
            }
            else
            {
                Console.WriteLine($"{name}: ключ не найден (null)");
            }
        }
    }
}