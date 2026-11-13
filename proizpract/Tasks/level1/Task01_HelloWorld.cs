using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1", 1)]
    internal class Task01_HelloWorld : ITask
    {
        public string Title => "Hello World";

        public string Description => "**Что сделать:**вывести приветствие и текущую дату.";

        public string[] Requirements => new[] {"\tИспользоватьConsole.WriteLine.",
            "\tДату получить черезDateTime.Now",
            "\tотформатировать какdd.MM.yyyy.",
            "\tВывести две строки: приветствие и дату." };

        public void Run()
        {
                Console.WriteLine("Hello World!");
                Console.WriteLine(DateTime.Now.ToString("dd.MM.yyyy"));
        }
    }
}
