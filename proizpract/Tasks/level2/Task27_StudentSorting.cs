using proizpract.Common;
using proizpract.Models;
using System;
using System.Collections.Generic;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 27)]
    internal class Task27_StudentSorting : ITask
    {
        public string Title => "IComparable / IComparer<T> для Student";

        public string Description => "Сортировка студентов по среднему баллу и по имени.";

        public string[] Requirements => new string[]
        {
            "Класс Student: Name, AverageGrade",
            "IComparable<Student> — сортировка по среднему баллу (по убыванию)",
            "IComparer<Student> ByNameComparer — сортировка по имени",
            "Продемонстрировать обе сортировки на List<Student>"
        };

        public void Run()
        {
            List<Student> students = new List<Student>
            {
                new Student("Иван", 4.5),
                new Student("Анна", 4.8),
                new Student("Борис", 3.9),
                new Student("Елена", 4.8),
                new Student("Дмитрий", 4.2)
            };

            Console.WriteLine("=== Исходный список ===");
            PrintStudents(students);

            // sort by deffault - IComparable<Student>
            students.Sort();
            Console.WriteLine("\n=== После Sort() — по убыванию среднего балла (IComparable) ===");
            PrintStudents(students);

            // sort by name — ByNameComparer
            students.Sort(new ByNameComparer());
            Console.WriteLine("\n=== После Sort(ByNameComparer) — по имени ===");
            PrintStudents(students);

            Console.Write("\nВведите имя и балл нового студента (например 'Олег 4.6'): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(' ');
                if (parts.Length == 2 && double.TryParse(parts[1], out double grade))
                {
                    try
                    {
                        students.Add(new Student(parts[0], grade));
                        students.Sort();
                        Console.WriteLine("\n=== Обновлённый список (по баллу) ===");
                        PrintStudents(students);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный формат.");
                }
            }
        }

        /// <summary>
        /// Prints all students in the list.
        /// </summary>
        private void PrintStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"  {student}");
            }
        }
    }
}