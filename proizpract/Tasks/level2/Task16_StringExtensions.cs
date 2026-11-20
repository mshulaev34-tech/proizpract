using proizpract.Common;
using proizpract.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level2
{
    [Common.TaskInfoAttribute("Level-2" , 16)]
    internal class Task16_StringExtensions : ITask
    {
        public string Title => "Методы расширения для string";

        public string Description => "Реализация и демонстрация extension-методов для типа string.";

        public string[] Requirements => new string[]
        {
            "Реализовать в статическом классе StringExtensions",
            "Методы: IsPalindrome(), WordCount(), ReverseString()",
            "Методы должны быть public static с this string первым параметром",
            "Продемонстрировать вызов на 2–3 строках"
        };
        public void Run()
        {
            string text1 = "А роза упала на лапу Азора";
            string text2 = "Hello, World! This is a test.";
            string text3 = "12321";

            Console.WriteLine($"Текст: \"{text1}\"");
            Console.WriteLine($"IsPalindrome: {text1.IsPalindrome()}");
            Console.WriteLine($"WordCount: {text1.WordCount()}");
            Console.WriteLine($"ReverseString: {text1.ReverseString()}");
            Console.WriteLine();

            Console.WriteLine($"Текст: \"{text2}\"");
            Console.WriteLine($"IsPalindrome: {text2.IsPalindrome()}");
            Console.WriteLine($"WordCount: {text2.WordCount()}");
            Console.WriteLine($"ReverseString: {text2.ReverseString()}");
            Console.WriteLine();

            Console.WriteLine($"Текст: \"{text3}\"");
            Console.WriteLine($"IsPalindrome: {text3.IsPalindrome()}");
            Console.WriteLine($"WordCount: {text3.WordCount()}");
            Console.WriteLine($"ReverseString: {text3.ReverseString()}");
        }
    }
}
