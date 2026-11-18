using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",2)]
    internal class Task02_Primitives : ITask
    {
        public string Title => "\tPrimitives";

        public string Description => "\t**Что сделать:**объявить переменные всех примитивных типов и вывести их значения и типы.";

        public string[] Requirements => new[] {"\tДля каждой переменной вывести значение иGetType().Name.\r\ndecimal— с суффиксомm,float— сf."};

        public void Run()
        {
            int age = 19;
            long population = 1_000_000_000L;
            double pi = 3.14159265358979;
            float price = 19.99f;          
            decimal money = 1234.56m;        
            bool isActive = true;
            char letter = 'A';
            string name = "Максим";
            byte b = 255;
            sbyte sb = -100;
            short sh = -30000;
            ushort ush = 65000;
            uint ui = 4_000_000_000;
            ulong ul = 18_000_000_000_000_000_000UL;
            Console.WriteLine($"age        = {age}        ({age.GetType().Name})");
            Console.WriteLine($"population = {population} ({population.GetType().Name})");
            Console.WriteLine($"pi         = {pi}         ({pi.GetType().Name})");
            Console.WriteLine($"price      = {price}      ({price.GetType().Name})");
            Console.WriteLine($"money      = {money}      ({money.GetType().Name})");
            Console.WriteLine($"isActive   = {isActive}   ({isActive.GetType().Name})");
            Console.WriteLine($"letter     = {letter}     ({letter.GetType().Name})");
            Console.WriteLine($"name       = {name}       ({name.GetType().Name})");
            Console.WriteLine($"b          = {b}          ({b.GetType().Name})");
            Console.WriteLine($"sb         = {sb}         ({sb.GetType().Name})");
            Console.WriteLine($"sh         = {sh}         ({sh.GetType().Name})");
            Console.WriteLine($"ush        = {ush}        ({ush.GetType().Name})");
            Console.WriteLine($"ui         = {ui}         ({ui.GetType().Name})");
            Console.WriteLine($"ul         = {ul}         ({ul.GetType().Name})");
        }
    }
}
