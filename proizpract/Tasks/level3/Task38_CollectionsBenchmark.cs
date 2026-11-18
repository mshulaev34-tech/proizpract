using proizpract.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace proizpract.Tasks.Level3
{
    [Common.TaskInfoAttribute("Level-3", 38)]
    internal class Task38_CollectionsBenchmark : ITask
    {
        public string Title => "List<T> vs LinkedList<T> (бенчмарк)";

        public string Description => "Сравнение производительности вставки в середину List<T> и LinkedList<T>.";

        public string[] Requirements => new string[]
        {
            "Создать List<int> и LinkedList<int> по 100 000 элементов",
            "Вставить 1000 элементов в середину каждой коллекции",
            "Замерить время через Stopwatch",
            "Вывести оба времени и объяснить результат"
        };

        public void Run()
        {
            const int InitialSize = 100_000;
            const int InsertCount = 1_000;

            Console.WriteLine($"Начальный размер коллекций: {InitialSize:N0}");
            Console.WriteLine($"Количество вставок: {InsertCount:N0}\n");

            // === Test List<T> ===
            List<int> list = new List<int>(InitialSize);
            for (int i = 0; i < InitialSize; i++)
            {
                list.Add(i);
            }

            Stopwatch sw = Stopwatch.StartNew();
            int listMiddle = list.Count / 2;
            for (int i = 0; i < InsertCount; i++)
            {
                list.Insert(listMiddle + i, -1);
            }
            sw.Stop();
            long listTime = sw.ElapsedMilliseconds;

            // === Test LinkedList<T> ===
            LinkedList<int> linkedList = new LinkedList<int>();
            for (int i = 0; i < InitialSize; i++)
            {
                linkedList.AddLast(i);
            }

            sw.Restart();

            LinkedListNode<int> middleNode = linkedList.First;
            for (int i = 0; i < InitialSize / 2; i++)
            {
                middleNode = middleNode.Next;
            }

            for (int i = 0; i < InsertCount; i++)
            {
                linkedList.AddAfter(middleNode, -1);
            }
            sw.Stop();
            long linkedListTime = sw.ElapsedMilliseconds;

            Console.WriteLine("=== Результаты бенчмарка ===");
            Console.WriteLine($"List<T> вставка в середину:     {listTime} мс");
            Console.WriteLine($"LinkedList<T> вставка в середину: {linkedListTime} мс");
            Console.WriteLine($"Разница: {Math.Abs(listTime - linkedListTime)} мс");

            if (listTime > 0 && linkedListTime > 0)
            {
                double ratio = (double)listTime / linkedListTime;
                Console.WriteLine($"Соотношение: {ratio:F2}x");
            }

            Console.WriteLine("\n=== Объяснение результатов ===");
            Console.WriteLine("List<T>:");
            Console.WriteLine("  - Вставка в середину требует сдвига ВСЕХ элементов правее позиции вставки.");
            Console.WriteLine("  - Сложность: O(n) на каждую вставку, итого O(n * k), где k — число вставок.");
            Console.WriteLine("  - Но доступ по индексу — O(1), кэши CPU работают эффективно.");
            Console.WriteLine();
            Console.WriteLine("LinkedList<T>:");
            Console.WriteLine("  - Вставка после найденного узла — O(1), сдвигать ничего не нужно.");
            Console.WriteLine("  - НО: поиск узла по индексу — O(n), что и показано в бенчмарке.");
            Console.WriteLine("  - Проигрыш в кэш-локальности: элементы разбросаны по памяти.");
            Console.WriteLine();
            Console.WriteLine("Вывод: LinkedList быстрее на вставке/удалении, если УЖЕ есть ссылка на узел.");
            Console.WriteLine("Но на практике List часто выигрывает за счёт кэш-эффективности и простоты.");

            Console.Write("\nВведите количество вставок для повторного теста (или Enter для 1000): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int userCount) && userCount > 0)
            {
                RunCustomTest(InitialSize, userCount);
            }
        }

        /// <summary>
        /// Runs the benchmark again with a custom number of insertions.
        /// </summary>
        private void RunCustomTest(int initialSize, int insertCount)
        {
            List<int> list = new List<int>(initialSize);
            for (int i = 0; i < initialSize; i++) list.Add(i);

            LinkedList<int> linkedList = new LinkedList<int>();
            for (int i = 0; i < initialSize; i++) linkedList.AddLast(i);

            Stopwatch sw = Stopwatch.StartNew();
            int middle = list.Count / 2;
            for (int i = 0; i < insertCount; i++) list.Insert(middle + i, -1);
            sw.Stop();
            long listTime = sw.ElapsedMilliseconds;

            sw.Restart();
            LinkedListNode<int> node = linkedList.First;
            for (int i = 0; i < initialSize / 2; i++) node = node.Next;
            for (int i = 0; i < insertCount; i++) linkedList.AddAfter(node, -1);
            sw.Stop();
            long linkedTime = sw.ElapsedMilliseconds;

            Console.WriteLine($"\nПовторный тест ({insertCount} вставок):");
            Console.WriteLine($"  List<T>:       {listTime} мс");
            Console.WriteLine($"  LinkedList<T>: {linkedTime} мс");
        }
    }
}