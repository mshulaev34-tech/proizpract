using proizpract.Common;
using proizpract.Models;
using System;
using System.Collections.Generic;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 24)]
    internal class Task24_AnimalHierarchy : ITask
    {
        public string Title => "ООП: Animal → Dog/Cat/Bird";

        public string Description => "Построение иерархии животных с демонстрацией полиморфизма.";

        public string[] Requirements => new string[]
        {
            "Абстрактный класс Animal с Name, MakeSound(), ToString()",
            "Классы Dog, Cat, Bird — наследники",
            "Bird дополнительно имеет метод Fly()",
            "Полиморфизм: List<Animal> с вызовом MakeSound() в цикле"
        };

        public void Run()
        {
            List<Animal> animals = new List<Animal>
            {
                new Dog("Шарик"),
                new Cat("Мурка"),
                new Bird("Чижик"),
                new Dog("Бобик"),
                new Cat("Барсик")
            };

            Console.WriteLine("=== Полиморфный вызов MakeSound() ===");
            foreach (Animal animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }

            Console.WriteLine("\n=== Специальный метод Bird.Fly() ===");
            foreach (Animal animal in animals)
            {
                if (animal is Bird bird)
                {
                    Console.WriteLine(bird.Fly());
                }
            }

            Console.Write("\nВведите имя нового животного (Dog/Cat/Bird через пробел, например 'Dog Rex'): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(' ', 2.ToString()[0]);
                if (parts.Length == 2)
                {
                    Animal newAnimal = CreateAnimal(parts[0], parts[1]);
                    if (newAnimal != null)
                    {
                        animals.Add(newAnimal);
                        Console.WriteLine($"Добавлено: {newAnimal}");
                    }
                    else
                    {
                        Console.WriteLine("Неизвестный тип животного.");
                    }
                }
                else
                {
                    Console.WriteLine("Формат: Тип Имя (например, 'Cat Murka')");
                }
            }
        }

        /// <summary>
        /// Factory method that creates an Animal by its type name.
        /// </summary>
        private Animal CreateAnimal(string typeName, string name)
        {
            switch (typeName.ToLower())
            {
                case "dog": return new Dog(name);
                case "cat": return new Cat(name);
                case "bird": return new Bird(name);
                default: return null;
            }
        }
    }
}