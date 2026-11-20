using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",8)]
    internal class Task08_FizzBuzz
    {
        public string Title => "FizzBuzz";
        public string Description => "числа кратные 3 выводят Fizz  числа кратные 5 выводят Buzz числа кратные 15 выводят FizzBuzz";
        public string[] Requirements => new[] { "Кратно 3 → Fizz.",
            "\tКратно 5 → Buzz.",
            "\tКратно 15 → FizzBuzz.",
            "\tИначе — само число.",
            "\tПорядок проверок: сначала 15, потом 3 и 5."};

        public void Run()
        {
            Console.Write("Введите число: ");
            string line = Console.ReadLine();
            int result = 0;
            bool isInt = int.TryParse(line, out result);
            string textResult = "";
            if (isInt)
            {
                if (result % 15 == 0)
                {
                    textResult = "FizzBuzz";
                    Console.WriteLine(textResult);
                    return;
                }
                if (result % 3 == 0)
                {
                    textResult = "Fizz";
                    Console.WriteLine(textResult);
                    return;
                }
                if (result % 5 == 0)
                {
                    textResult = "Buzz";
                    Console.WriteLine(textResult);
                    return;
                }
                textResult = line;
            }
            else
            {
                textResult = "ВВедено не коректное число";
            }
            Console.WriteLine(textResult);

        }
    }
}
