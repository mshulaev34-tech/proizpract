using proizpract.Common;
using proizpract.Common.proizpract.Models;
using proizpract.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 25)]
    internal class Task25_ShapeSorting : ITask
    {
        public string Title => "Интерфейс IShape + сортировка фигур";

        public string Description => "Реализация интерфейса фигур и сортировка по площади.";

        public string[] Requirements => new string[]
        {
            "IShape с методами Area() и Perimeter()",
            "Классы Circle, Rectangle, Triangle — реализуют IShape",
            "Создать List<IShape> из 5+ фигур",
            "Отсортировать по площади",
            "Вывести фигуры до и после сортировки"
        };

        public void Run()
        {
            List<IShape> shapes = new List<IShape>
            {
                new Circle(5),
                new Rectangle(4, 6),
                new Triangle(3, 4, 5),
                new Circle(2),
                new Rectangle(10, 3),
                new Triangle(7, 8, 9)
            };

            Console.WriteLine("=== Фигуры ДО сортировки ===");
            PrintShapes(shapes);

            shapes.Sort((a, b) => a.Area().CompareTo(b.Area()));

            Console.WriteLine("\n=== Фигуры ПОСЛЕ сортировки (по возрастанию площади) ===");
            PrintShapes(shapes);

            Console.Write("\nДобавить ещё одну фигуру? (circle/rectangle/triangle или Enter для пропуска): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                IShape newShape = CreateShapeByType(input);
                if (newShape != null)
                {
                    shapes.Add(newShape);
                    shapes.Sort((a, b) => a.Area().CompareTo(b.Area()));
                    Console.WriteLine("\n=== Обновлённый список ===");
                    PrintShapes(shapes);
                }
                else
                {
                    Console.WriteLine("Неизвестный тип фигуры.");
                }
            }
        }

        /// <summary>
        /// Prints all shapes in the list with their descriptions.
        /// </summary>
        private void PrintShapes(List<IShape> shapes)
        {
            foreach (IShape shape in shapes)
            {
                Console.WriteLine($"  {shape}");
            }
        }

        /// <summary>
        /// Creates a shape of the specified type with default parameters.
        /// </summary>
        private IShape CreateShapeByType(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "circle": return new Circle(3);
                case "rectangle": return new Rectangle(5, 5);
                case "triangle": return new Triangle(5, 5, 5);
                default: return null;
            }
        }
    }
}