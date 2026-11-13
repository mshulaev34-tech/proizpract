using proizpract.Common;
using proizpract.Enums;
using System;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2" , 21)]
    internal class Task21_DayOfWeekEnum : ITask
    {
        public string Title => "enum дни недели ";

        public string Description => "Описание дней недели через enum и определение типа дня.";

        public string[] Requirements => new string[]
        {
            "enum DayOfWeek { Monday, ..., Sunday }",
            "Метод IsWeekend(DayOfWeek day) — true для субботы и воскресенья",
            "Метод GetRussianName(DayOfWeek day) — возвращает название на русском",
            "Продемонстрировать на всех 7 днях"
        };
        public void Run()
        {
            foreach (CustomDayOfWeek day in Enum.GetValues(typeof(CustomDayOfWeek)))
            {
                string russianName = GetRussianName(day);
                string dayType = IsWeekend(day) ? "Выходной" : "Рабочий";
                Console.WriteLine($"{russianName} — {dayType}");
            }

            Console.Write("\nВведите номер дня недели (1-7, где 1=Monday): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int dayNumber) && dayNumber >= 1 && dayNumber <= 7)
            {
                CustomDayOfWeek selectedDay = (CustomDayOfWeek)(dayNumber - 1);
                Console.WriteLine($"Выбрано: {GetRussianName(selectedDay)}. Это {(IsWeekend(selectedDay) ? "выходной" : "рабочий")} день.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до 7.");
            }
        }

        /// <summary>
        /// Determines if the specified day is a weekend.
        /// </summary>
        private bool IsWeekend(CustomDayOfWeek day)
        {
            return day == CustomDayOfWeek.Saturday || day == CustomDayOfWeek.Sunday;
        }

        /// <summary>
        /// Returns the Russian name of the specified day.
        /// </summary>
        private string GetRussianName(CustomDayOfWeek day)
        {
            switch (day) 
            {
                case CustomDayOfWeek.Monday :
                    return "Понедельник";
                case CustomDayOfWeek.Tuesday:
                    return "Вторник";
                case CustomDayOfWeek.Wednesday:
                    return "Среда";
                case CustomDayOfWeek.Thursday:
                    return "Четверг";
                case CustomDayOfWeek.Friday:
                    return "Пятница";
                case CustomDayOfWeek.Saturday:
                    return "Суббота";
                case CustomDayOfWeek.Sunday:
                    return "Воскресенье";
                default:
                    return "Неизвестно";
            }
        }
    }
}