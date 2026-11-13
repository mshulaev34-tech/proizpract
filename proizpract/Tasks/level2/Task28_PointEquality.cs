using proizpract.Common;
using proizpract.Models;
using System;
using System.Collections.Generic;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 28)]
    internal class Task28_PointEquality : ITask
    {
        public string Title => "Equals / GetHashCode для Point";

        public string Description => "Переопределение Equals и GetHashCode для класса Point.";

        public string[] Requirements => new string[]
        {
            "Переопределить Equals(object) и GetHashCode()",
            "Реализовать IEquatable<Point>",
            "Проверить равенство двух точек с одинаковыми координатами",
            "Проверить в HashSet<Point>: дубликаты не увеличивают Count"
        };

        public void Run()
        {
            Point p1 = new Point(3, 5);
            Point p2 = new Point(3, 5);
            Point p3 = new Point(3, 7);
            Point p4 = p1;

            Console.WriteLine("=== Проверка Equals ===");
            Console.WriteLine($"p1 {p1} == p2 {p2} : {p1.Equals(p2)} (ожидается True)");
            Console.WriteLine($"p1 {p1} == p3 {p3} : {p1.Equals(p3)} (ожидается False)");
            Console.WriteLine($"p1 == p2 (оператор): {p1 == p2}");
            Console.WriteLine($"p1 != p3 (оператор): {p1 != p3}");
            Console.WriteLine($"ReferenceEquals(p1, p4): {ReferenceEquals(p1, p4)}");

            Console.WriteLine("\n=== Проверка GetHashCode ===");
            Console.WriteLine($"p1.GetHashCode() = {p1.GetHashCode()}");
            Console.WriteLine($"p2.GetHashCode() = {p2.GetHashCode()}");
            Console.WriteLine($"p3.GetHashCode() = {p3.GetHashCode()}");
            Console.WriteLine($"Hash одинаков для равных точек: {p1.GetHashCode() == p2.GetHashCode()}");

            Console.WriteLine("\n=== Проверка HashSet<Point> ===");
            HashSet<Point> set = new HashSet<Point>();
            set.Add(p1);
            set.Add(p2); // Dublicate - no added
            set.Add(p3);
            Console.WriteLine($"Добавлены: {p1}, {p2} (дубликат), {p3}");
            Console.WriteLine($"Count в HashSet: {set.Count} (ожидается 2)");

            Console.Write("\nВведите координаты новой точки (например '3 5'): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
                {
                    Point userPoint = new Point(x, y);
                    bool alreadyExists = set.Contains(userPoint);
                    set.Add(userPoint);
                    Console.WriteLine($"Точка {userPoint}: {(alreadyExists ? "уже была в множестве" : "добавлена")}. Новый Count: {set.Count}");
                }
                else
                {
                    Console.WriteLine("Некорректный формат.");
                }
            }
        }
    }
}