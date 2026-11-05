using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1")]
    internal class Task01_HelloWorld : ITask
    {
        public string Title => "Hello World";

        public string Description => "**Что сделать:**вывести приветствие и текущую дату.";

        public string[] Requirements => new[] { "ИспользоватьConsole.WriteLine.\r\nДату получить черезDateTime.Now, отформатировать какdd.MM.yyyy.\r\nВывести две строки: приветствие и дату." };

        public void Run()
        {
            
                Console.WriteLine("Hello World!");
                Console.WriteLine(DateTime.Now.ToString("dd.MM.yyyy"));
           
        }
    }
}
