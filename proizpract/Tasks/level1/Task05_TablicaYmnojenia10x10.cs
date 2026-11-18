using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",5)]
    internal class Task05_TablicaYmnojenia10x10 : ITask
    {
        public string Title => "TablicaYmnojenia10x10";

        public string Description => "**Что сделать:**определить, чётное ли введённое число.";

        public string[] Requirements => new[] {"Требования:",
        "Использовать вложенные циклыfor.",
        "Выровнять столбцы.",
        "Заголовки строк/столбцов — не обязательны, но приветствуются.",
        "**Готово, если:**таблица ровная, все 100 значений на месте."};
        public void Run()
        {
            Console.WriteLine("Таблица умножения 10x10:");
            Console.WriteLine("------------------------------------------------");
            for (int a = 1; a <= 10; a++)
            {
                for (int b = 1; b <= 10; b++)
                {
                    int result = a * b;
                    Console.Write("{0, 4} ", result);
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------------------------------------"); 
        }
    }
}
