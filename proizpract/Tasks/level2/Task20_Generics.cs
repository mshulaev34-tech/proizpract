using proizpract.Common;
using proizpract.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level2
{
    [Common.TaskInfoAttribute("Level-2" , 20)]
    internal class Task20_Generics : ITask
    {
        public string Title => "Обобщения: Swap<T> и Max<T> ";

        public string Description => "Реализация и демонстрация обобщённых методов Swap и Max с ограничением IComparable<T>.";

        public string[] Requirements => new string[]
        {
            "Swap<T>(ref T a, ref T b) — меняет значения местами",
            "Max<T>(T a, T b) where T : IComparable<T> — возвращает большее",
            "Продемонстрировать на int, string, double",
            "Swap — без ограничений, Max — с ограничением"
        };
        public void Run()
        {
            Console.WriteLine("=== Демонстрация на int ===");
            int a = 5, b = 10;
            Console.WriteLine($"До Swap: a = {a}, b = {b}");
            GenericUtils.Swap(ref a, ref b);
            Console.WriteLine($"После Swap: a = {a}, b = {b}");
            Console.WriteLine($"Max({a}, {b}) = {GenericUtils.Max(a, b)}");

            Console.WriteLine("\n=== Демонстрация на string ===");
            string s1 = "Apple", s2 = "Banana";
            Console.WriteLine($"До Swap: s1 = {s1}, s2 = {s2}");
            GenericUtils.Swap(ref s1, ref s2);
            Console.WriteLine($"После Swap: s1 = {s1}, s2 = {s2}");
            Console.WriteLine($"Max(\"{s1}\", \"{s2}\") = {GenericUtils.Max(s1, s2)}");

            Console.WriteLine("\n=== Демонстрация на double ===");
            double d1 = 3.14, d2 = 2.71;
            Console.WriteLine($"До Swap: d1 = {d1}, d2 = {d2}");
            GenericUtils.Swap(ref d1, ref d2);
            Console.WriteLine($"После Swap: d1 = {d1}, d2 = {d2}");
            Console.WriteLine($"Max({d1}, {d2}) = {GenericUtils.Max(d1, d2)}");

            Console.WriteLine("\n=== Динамический ввод ===");
            Console.Write("Введите первое число (double): ");
            string input1 = Console.ReadLine();
            Console.Write("Введите второе число (double): ");
            string input2 = Console.ReadLine();
            if (double.TryParse(input1, out double val1) && double.TryParse(input2, out double val2))
            {
                Console.WriteLine($"Вы ввели: {val1} и {val2}");
                GenericUtils.Swap(ref val1, ref val2);
                Console.WriteLine($"После Swap: {val1} и {val2}");
                Console.WriteLine($"Max: {GenericUtils.Max(val1, val2)}");
            }
            else
            {
                Console.WriteLine("Некорректный ввод чисел.");
            }
        }
    }
}
