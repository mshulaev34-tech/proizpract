using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",4)]
    internal class Task04_ChetnoeChislo : ITask
    {
        public string Title => "ChetnoeChislo";

        public string Description => "**Что сделать:**определить, чётное ли введённое число.";

        public string[] Requirements => new[] { "Способ 1: через% 2 == 0.",
            "Способ 2: через тернарный оператор?",
            "Оба способа реализовать отдельными методами.",
            "Вывести результат обоих способов." };

        public void Run()
        {
            {
                bool IsEvenModulo(int n)
                {
                    return n % 2 == 0;
                }
                bool IsEvenTernary(int n)
                {
                    return (n % 2 == 0) ? true : false;
                }
                Console.Write("Ввод целого числа: ");

                if (!int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.WriteLine("Ошибка нужно ввести целое число.");
                    return;
                }
                bool result1 = IsEvenModulo(number);
                bool result2 = IsEvenTernary(number);
                Console.WriteLine($"Способ 1 (% 2 == 0): {(result1 ? "чётное" : "нечётное")}");
                Console.WriteLine($"Способ 2 (тернарный): {(result2 ? "чётное" : "нечётное")}");
            }
        }
    }
}
