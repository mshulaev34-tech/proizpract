using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 34)]
    internal class Task34_LinqWordGrouping : ITask
    {
        public string Title => "LINQ: группировка слов по первой букве";

        public string Description => "Группировка слов по первой букве с сортировкой внутри групп.";

        public string[] Requirements => new string[]
        {
            "Взять список слов (10+)",
            "Сгруппировать через LINQ GroupBy(w => w[0])",
            "Отсортировать группы по первой букве",
            "Внутри группы — отсортировать слова",
            "Вывести «Буква: слово1, слово2, ...»"
        };

        public void Run()
        {
            List<string> words = new List<string>
            {
                "яблоко", "арбуз", "банан", "вишня", "апельсин",
                "груша", "банан", "виноград", "арбуз", "дыня",
                "слива", "яблоко", "мандарин", "гранат"
            };

            Console.WriteLine("=== Исходные слова ===");
            Console.WriteLine(string.Join(", ", words));

            Console.WriteLine("\n=== Группировка по первой букве ===");
            var grouped = words
                .GroupBy(w => char.ToUpper(w[0]))
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Letter = g.Key,
                    Words = g.Distinct().OrderBy(w => w).ToList()
                });

            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Letter}: {string.Join(", ", group.Words)}");
            }

            Console.Write("\nВведите свои слова через пробел (или Enter для пропуска): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                var userWords = input
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => w.Length > 0)
                    .ToList();

                if (userWords.Count > 0)
                {
                    var userGrouped = userWords
                        .GroupBy(w => char.ToUpper(w[0]))
                        .OrderBy(g => g.Key)
                        .Select(g => new
                        {
                            Letter = g.Key,
                            Words = g.Distinct().OrderBy(w => w).ToList()
                        });

                    Console.WriteLine("\n=== Ваши слова, сгруппированные ===");
                    foreach (var group in userGrouped)
                    {
                        Console.WriteLine($"{group.Letter}: {string.Join(", ", group.Words)}");
                    }
                }
            }
        }
    }
}