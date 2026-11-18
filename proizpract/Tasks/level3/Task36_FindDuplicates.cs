using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proizpract.Tasks.Level3
{
    [Common.TaskInfoAttribute("Level-3", 36)]
    internal class Task36_FindDuplicates : ITask
    {
        public string Title => "Дубликаты в List (HashSet + GroupBy)";

        public string Description => "Поиск дубликатов в списке чисел двумя способами: через HashSet и LINQ GroupBy.";

        public string[] Requirements => new string[]
        {
            "Способ 1: через HashSet<int> — при добавлении проверять Contains",
            "Способ 2: через LINQ GroupBy(x => x).Where(g => g.Count() > 1)",
            "Оба способа оформить отдельными методами",
            "Продемонстрировать на списке с повторяющимися значениями"
        };

        public void Run()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 2, 5, 1, 6, 7, 3, 8, 9, 5, 10 };

            Console.WriteLine("Исходный список:");
            Console.WriteLine($"[{string.Join(", ", numbers)}]\n");

            List<int> duplicatesByHashSet = FindDuplicatesByHashSet(numbers);
            List<int> duplicatesByLinq = FindDuplicatesByLinq(numbers);

            Console.WriteLine("=== Способ 1: HashSet ===");
            Console.WriteLine($"Дубликаты: [{string.Join(", ", duplicatesByHashSet)}]");

            Console.WriteLine("\n=== Способ 2: LINQ GroupBy ===");
            Console.WriteLine($"Дубликаты: [{string.Join(", ", duplicatesByLinq)}]");

            Console.WriteLine($"\nРезультаты совпадают: {AreSetsEqual(duplicatesByHashSet, duplicatesByLinq)}");

            Console.Write("\nВведите числа через пробел для поиска дубликатов: ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                List<int> userNumbers = ParseNumbers(input);
                if (userNumbers != null)
                {
                    var userDuplicates = FindDuplicatesByLinq(userNumbers);
                    Console.WriteLine($"Дубликаты в вашем списке: [{string.Join(", ", userDuplicates)}]");
                }
            }
        }

        /// <summary>
        /// Finds duplicate values using HashSet. When adding an element,
        /// checks if it already exists via Contains.
        /// </summary>
        private List<int> FindDuplicatesByHashSet(IEnumerable<int> source)
        {
            HashSet<int> seen = new HashSet<int>();
            HashSet<int> duplicates = new HashSet<int>();

            foreach (int number in source)
            {
                if (seen.Contains(number))
                {
                    duplicates.Add(number);
                }
                else
                {
                    seen.Add(number);
                }
            }

            return duplicates.OrderBy(x => x).ToList();
        }

        /// <summary>
        /// Finds duplicate values using LINQ GroupBy.
        /// Groups elements by value and selects groups with count greater than 1.
        /// </summary>
        private List<int> FindDuplicatesByLinq(IEnumerable<int> source)
        {
            return source
                .GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .OrderBy(x => x)
                .ToList();
        }

        /// <summary>
        /// Compares two lists as sets (ignoring order and duplicates).
        /// </summary>
        private bool AreSetsEqual(List<int> a, List<int> b)
        {
            return new HashSet<int>(a).SetEquals(b);
        }

        /// <summary>
        /// Parses a space-separated string of integers into a list.
        /// Returns null if parsing fails.
        /// </summary>
        private List<int> ParseNumbers(string input)
        {
            string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> result = new List<int>();
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int number))
                {
                    result.Add(number);
                }
                else
                {
                    Console.WriteLine($"Не удалось распарсить \"{part}\" как число.");
                    return null;
                }
            }
            return result;
        }
    }
}