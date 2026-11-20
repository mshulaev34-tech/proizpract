using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",6)]
    internal class Task06_SummaChiselChisla : ITask
    {
        public string Title => "SummaChiselChisla";

        public string Description => "**Что сделать:**найти сумму цифр введённого числа (например, 1234 → 10).";

        public string[] Requirements => new[] { "Требования:",
        "\tСпособ 1 (строка): число →ToString()→ пройти по символам →int.Parseкаждой цифры.",
        "\tСпособ 2 (арифметика): цикл while (n > 0) { sum += n % 10; n /= 10; }.",
        "\tОба способа оформить отдельными методами.",
        "\tОтрицательные числа — брать модуль.",
        "\t**Готово, если:**оба метода дают одинаковый результат на 3+ разных числах, включая отрицательное." };

        public void Run()
        {
            Console.Write("Введите число: ");
            int number = int.Parse(Console.ReadLine());

            Console.WriteLine($"Способ 1 (строка): {MethodOne(number)}");
            Console.WriteLine($"Способ 2 (арифметика): {MethodTwo(number)}");
        }

        private int MethodOne(int i)
        {
            int sum = 0;
            string str = Math.Abs(i).ToString();
            foreach (char c in str)
            {
                sum += int.Parse(c.ToString());
            }
            return sum;
        }

        private int MethodTwo(int i)
        {
            int sum = 0;
            int n = Math.Abs(i);
            while (n > 0)
            {
                sum += n % 10;
                n /= 10;
            }
            return sum;
        }
    }
}
