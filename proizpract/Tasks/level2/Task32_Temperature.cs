using proizpract.Common;
using proizpract.Models;
using System;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 32)]
    internal class Task32_Temperature : ITask
    {
        public string Title => "Свойства с валидацией (Temperature)";

        public string Description => "Класс Temperature с валидацией в set и вычисляемым Fahrenheit.";

        public string[] Requirements => new string[]
        {
            "Свойство Celsius с проверкой в set: не ниже −273.15",
            "При некорректном значении — ArgumentOutOfRangeException",
            "Вычисляемое свойство Fahrenheit (только get)",
            "Продемонстрировать корректное и некорректное присваивание"
        };

        public void Run()
        {
            Temperature temp = new Temperature(25);
            Console.WriteLine($"Создано: {temp}");

            Console.WriteLine("\n=== Корректные присваивания ===");
            temp.Celsius = 0;
            Console.WriteLine($"0°C (точка замерзания воды): {temp}");

            temp.Celsius = 100;
            Console.WriteLine($"100°C (точка кипения воды): {temp}");

            temp.Celsius = -273.15;
            Console.WriteLine($"-273.15°C (абсолютный ноль): {temp}");

            Console.WriteLine("\n=== Некорректные присваивания ===");
            TrySetTemperature(temp, -300);
            TrySetTemperature(temp, -273.16);

            Console.Write("\nВведите температуру в °C: ");
            string input = Console.ReadLine();
            if (double.TryParse(input, out double userValue))
            {
                try
                {
                    temp.Celsius = userValue;
                    Console.WriteLine($"Принято: {temp}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }

        /// <summary>
        /// Attempts to set the temperature and prints the result or error.
        /// </summary>
        private void TrySetTemperature(Temperature temp, double value)
        {
            try
            {
                temp.Celsius = value;
                Console.WriteLine($"{value}°C принято: {temp}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"[Поймано] {value}°C → {ex.ParamName}: {ex.Message}");
            }
        }
    }
}