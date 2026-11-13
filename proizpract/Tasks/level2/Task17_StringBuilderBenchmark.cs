using proizpract.Common;
using System;
using System.Diagnostics;
using System.Text;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2" , 17)]
    internal class Task17_StringBuilderBenchmark : ITask
    {
        public string Title => "StringBuilder vs конкатенация (замер производительности) ";

        public string Description => "Сравнение производительности конкатенации через + и StringBuilder.";

        public string[] Requirements => new string[]
        {
            "Собрать строку из 10 000 (или 100 000) чисел двумя способами",
            "Замерить время каждого через Stopwatch",
            "Вывести оба времени и разницу",
            "Объяснить, почему StringBuilder быстрее"
        };

        public void Run()
        {
            int count = 100000;

            Console.WriteLine($"Подождите...\n");
            Console.WriteLine($"Тестирование с {count} числами:\n");

            Stopwatch sw = Stopwatch.StartNew();
            string result1 = ConcatenateWithPlus(count);
            sw.Stop();
            long timePlus = sw.ElapsedMilliseconds;

            sw.Restart();
            string result2 = ConcatenateWithStringBuilder(count);
            sw.Stop();
            long timeBuilder = sw.ElapsedMilliseconds;

            Console.WriteLine($"Конкатенация через +: {timePlus} мс");
            Console.WriteLine($"StringBuilder: {timeBuilder} мс");
            Console.WriteLine($"Разница: {timePlus - timeBuilder} мс ({(double)timePlus / (timeBuilder + 1):F2}x быстрее)\n");

            Console.WriteLine($"Строки равны: {result1 == result2}");
            Console.WriteLine($"Длина строки: {result1.Length} символов\n");

            Console.WriteLine("Почему StringBuilder быстрее:");
            Console.WriteLine("1. String в C# неизменяемая (immutable). Каждая конкатенация через + создает новый объект string в памяти.");
            Console.WriteLine("2. При 100 000 итерациях создается 100 000 промежуточных строк, которые нагружают GC.");
            Console.WriteLine("3. StringBuilder использует изменяемый буфер (char[]), который расширяется по мере необходимости.");
            Console.WriteLine("4. StringBuilder выделяет память один раз (или несколько раз при расширении), а не на каждой итерации.");
        }

        /// <summary>
        /// Constructs a string from numbers by concatenating them using the + operator.
        /// </summary>
        private string ConcatenateWithPlus(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                result += i.ToString();
            }
            return result;
        }

        /// <summary>
        /// Constructs a string from numbers using StringBuilder.
        /// </summary>
        private string ConcatenateWithStringBuilder(int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(i);
            }
            return sb.ToString();
        }
    }
}