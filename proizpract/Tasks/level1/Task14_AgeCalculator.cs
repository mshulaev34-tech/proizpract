using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",14)]
    internal class Task14_AgeCalculator
    {
        public string Title => "Возраст по дате рождения";

        public string Description => "Вычислить точный возраст в годах, месяцах и днях по дате рождения.";

        public string[] Requirements => new string[]
        {
            "Ввод даты через DateTime.TryParse",
            "Учесть, что день рождения в этом году мог ещё не наступить",
            "Вывести «X лет, Y месяцев, Z дней»",
            "Некорректная дата — сообщение об ошибке, не падать"
        };

        public void Run()
        {
            CalculateAndPrint("15.05.1990");
            CalculateAndPrint("31.12.2020");
            CalculateAndPrint(DateTime.Today.ToString("dd.MM.yyyy"));
            CalculateAndPrint("неверная дата");
            CalculateAndPrint("29.02.2023");
        }

        /// <summary>
        /// Parses the date and calculates the age, outputting the result to the console.
        /// </summary>
        private void CalculateAndPrint(string input)
        {
            Console.WriteLine($"Ввод: {input}");
            if (DateTime.TryParse(input, out DateTime birthDate))
            {
                if (birthDate > DateTime.Today)
                {
                    Console.WriteLine("Ошибка: дата рождения не может быть в будущем.");
                    return;
                }

                int years = DateTime.Today.Year - birthDate.Year;
                int months = DateTime.Today.Month - birthDate.Month;
                int days = DateTime.Today.Day - birthDate.Day;

                if (days < 0)
                {
                    months--;
                    DateTime previousMonth = DateTime.Today.AddMonths(-1);
                    days += DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);
                }

                if (months < 0)
                {
                    years--;
                    months += 12;
                }

                Console.WriteLine($"Результат: {years} {GetPlural(years, "год", "года", "лет")}, {months} {GetPlural(months, "месяц", "месяца", "месяцев")}, {days} {GetPlural(days, "день", "дня", "дней")}");
            }
            else
            {
                Console.WriteLine("Ошибка: некорректный формат даты.");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Returns the correct form of a word based on the number, for the Russian language.
        /// </summary>
        private string GetPlural(int number, string one, string few, string many)
        {
            int abs = Math.Abs(number) % 100;
            int lastDigit = abs % 10;

            if (abs > 10 && abs < 20) return many;
            if (lastDigit > 1 && lastDigit < 5) return few;
            if (lastDigit == 1) return one;
            return many;
        }

    }
}
