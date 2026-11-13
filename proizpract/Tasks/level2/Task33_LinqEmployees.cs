using proizpract.Common;
using proizpract.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 33)]
    internal class Task33_LinqEmployees : ITask
    {
        public string Title => "LINQ: сотрудники (top-3, avg по отделу, фильтр)";

        public string Description => "LINQ-запросы по списку сотрудников: сортировка, группировка, фильтрация.";

        public string[] Requirements => new string[]
        {
            "Класс Employee: Name, Age, Salary, Department",
            "Список из 10+ сотрудников",
            "топ-3 по зарплате (убывание)",
            "средняя зарплата по каждому отделу (GroupBy + Average)",
            "сотрудники старше 30 (Where)"
        };

        public void Run()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee("Иванов", 28, 75000, "IT"),
                new Employee("Петров", 35, 120000, "IT"),
                new Employee("Сидорова", 42, 95000, "HR"),
                new Employee("Кузнецов", 31, 110000, "IT"),
                new Employee("Смирнова", 29, 65000, "Sales"),
                new Employee("Попов", 45, 150000, "Management"),
                new Employee("Васильева", 38, 88000, "HR"),
                new Employee("Новиков", 26, 70000, "Sales"),
                new Employee("Фёдоров", 33, 130000, "IT"),
                new Employee("Морозова", 40, 105000, "HR"),
                new Employee("Волков", 27, 72000, "Sales"),
                new Employee("Алексеев", 50, 200000, "Management")
            };

            Console.WriteLine($"Всего сотрудников: {employees.Count}\n");

            Console.WriteLine("=== 1. Топ-3 по зарплате (убывание) ===");
            var top3 = employees
                .OrderByDescending(e => e.Salary)
                .Take(3);
            foreach (var emp in top3)
            {
                Console.WriteLine($"  {emp}");
            }

            Console.WriteLine("\n=== 2. Средняя зарплата по отделам ===");
            var avgByDept = employees
                .GroupBy(e => e.Department)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count(),
                    Average = g.Average(e => e.Salary)
                });
            foreach (var item in avgByDept)
            {
                Console.WriteLine($"  {item.Department,-12}: {item.Average,10:F0} руб. (сотрудников: {item.Count})");
            }

            Console.WriteLine("\n=== 3. Сотрудники старше 30 лет ===");
            var over30 = employees.Where(e => e.Age > 30);
            foreach (var emp in over30)
            {
                Console.WriteLine($"  {emp}");
            }
            Console.WriteLine($"Всего старше 30: {over30.Count()}");

            Console.Write("\nВведите минимальный возраст для фильтрации: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int minAge))
            {
                var filtered = employees.Where(e => e.Age >= minAge).OrderBy(e => e.Age);
                Console.WriteLine($"\nСотрудники старше {minAge}:");
                foreach (var emp in filtered)
                {
                    Console.WriteLine($"  {emp}");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }
    }
}