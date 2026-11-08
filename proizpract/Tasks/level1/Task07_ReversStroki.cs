using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1")]
    internal class Task07_ReversStroki : ITask
    {
        public string Title => "ReversStroki";

        public string Description => "**Что сделать:**перевернуть введённую строку.";

        public string[] Requirements => new[]{"Требования:",
        "Не использоватьArray.Reverseи LINQ.",
        "Реализовать через циклforот конца к началу (или черезchar[] и swap).",
        "Пустая строка и строка из одного символа — обрабатываются корректно.",
        "Готово, если:**«abc» → «cba», «a» → «a», «» → «».;"};
        public void Run()
        {
            string ReverseString(string text)
            {
                if (string.IsNullOrEmpty(text))
                    return text ?? string.Empty;
                char[] result = new char[text.Length];
                for (int a = 0; a < text.Length; a++)
                {
                    result[a] = text[text.Length - 1 - a];
                }
                return new string(result);
            }
            Console.Write("Введите строку:");
            string input = Console.ReadLine();

            if (input == null)
            {
                input = string.Empty;
            }
            string reversed = ReverseString(input);
            Console.WriteLine($"Исходная строка:\"{input}\"");
            Console.WriteLine($"Перевёрнутая:\"{reversed}\"");
        }
    }
}
