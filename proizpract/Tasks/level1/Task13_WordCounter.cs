using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1")]
    internal class Task13_WordCounter
    {
        public string Title => "Счётчик вхождений слов в тексте";

        public string Description => "Посчитать, сколько раз каждое слово встречается в тексте, с использованием Dictionary.";

        public string[] Requirements => new string[]
        {
            "Использовать Dictionary<string, int>",
            "Разбить текст на слова по пробелам и знакам пунктуации",
            "Привести слова к нижнему регистру",
            "Вывести пары «слово → количество», отсортированные по убыванию количества"
        };

        public void Run()
        {
            string text = "Мама мыла раму, а рама была белой. Белая рама блестела на солнце.";
            var counts = CountWords(text);

            Console.WriteLine("Результат подсчёта слов:");
            foreach (var kvp in counts)
            {
                Console.WriteLine($"{kvp.Key} → {kvp.Value}");
            }
        }

        private List<KeyValuePair<string, int>> CountWords(string text)
        {
            var dictionary = new Dictionary<string, int>();
            string[] words = Regex.Split(text.ToLower(), @"\W+");

            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    if (dictionary.ContainsKey(word))
                    {
                        dictionary[word]++;
                    }
                    else
                    {
                        dictionary[word] = 1;
                    }
                }
            }

            return dictionary.OrderByDescending(kv => kv.Value).ToList();
        }
    }
}
