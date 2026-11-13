using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",12)]
    internal class Task12_Palindrom
    {
        public string Title => "Палиндром";

        public string Description => "Проверить, является ли строка палиндромом (без учёта регистра и пробелов)";

        public string[] Requirements => new string[]
        {
            "Игнорировать регистр",
            "Игнорировать пробелы и знаки пунктуации",
            "Не использовать LINQ Reverse",
            "Пустая строка и строка из одного символа — палиндром"
        };

        public void Run()
        {
            Console.WriteLine($"\"А роза упала на лапу Азора\" -> {IsPalindrome("А роза упала на лапу Азора")}");
            Console.WriteLine($"\"hello\" -> {IsPalindrome("hello")}");
            Console.WriteLine($"\"\" -> {IsPalindrome("")}");
            Console.WriteLine($"\"a\" -> {IsPalindrome("a")}");
            Console.WriteLine($"\"A man a plan a canal Panama\" -> {IsPalindrome("A man a plan a canal Panama")}");
            Console.WriteLine($"\"Was it a car or a cat I saw?\" -> {IsPalindrome("Was it a car or a cat I saw?")}");
        }

        private bool IsPalindrome(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;


            StringBuilder cleaned = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetterOrDigit(c))
                {
                    cleaned.Append(char.ToLower(c));
                }
            }

            string cleanString = cleaned.ToString();


            if (cleanString.Length <= 1)
                return true;

            int left = 0;
            int right = cleanString.Length - 1;

            while (left < right)
            {
                if (cleanString[left] != cleanString[right])
                    return false;

                left++;
                right--;
            }

            return true;
        }
    }
}
