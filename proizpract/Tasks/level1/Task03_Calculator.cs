using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",3)]
    internal class Task03_Calculator : ITask
    {
        public string Title => "Calculator";

        public string Description => "Описание:Принимает два числа и оператор (+, -, *, /),выводит результат.Обработать деление на ноль.";

        public string[] Requirements => new[] {"\tТребования:Использовать switch для оператора", 
            "\tНе допускать падения при делении на ноль"};

        public void Run()
        {
            Console.Write("Ввод первого числа: ");
            if (!double.TryParse(Console.ReadLine(), out double a))
            {
                Console.WriteLine("Ошибка первое число введено неверно");
                return;
            }
            Console.Write("Ввод второго числа: ");
            if (!double.TryParse(Console.ReadLine(), out double b))
            {
                Console.WriteLine("Ошибка второе число введено неверно");
                return;
            }
            Console.Write("Ввести оператор (+ - * /): ");
            string op = Console.ReadLine()?.Trim();
            switch (op)
            {
                case "+":
                    Console.WriteLine($"{a} + {b} = {a + b}");
                    break;
                case "-":
                    Console.WriteLine($"{a} - {b} = {a - b}");
                    break;
                case "*":
                    Console.WriteLine($"{a} * {b} = {a * b}");
                    break;
                case "/":
                    if (b == 0)
                    {
                        Console.WriteLine("Ошибка нельзя делить на ноль.");
                    }
                    else
                    {
                        Console.WriteLine($"{a} / {b} = {a / b}");
                    }
                    break; 
            }
        }
    }
}
