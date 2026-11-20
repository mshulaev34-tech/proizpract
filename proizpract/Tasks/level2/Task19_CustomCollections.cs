using proizpract.Common;
using proizpract.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level2
{
    [Common.TaskInfoAttribute("Level-2" , 19)]
    internal class Task19_CustomCollections : ITask
    {
        public string Title => "Свой стек и очередь на массиве ";

        public string Description => "Реализация MyStack<T> и MyQueue<T> на массиве с автоматическим расширением.";

        public string[] Requirements => new string[]
        {
            "Стек: Push, Pop, Peek, Count, IsEmpty",
            "Очередь: Enqueue, Dequeue, Peek, Count, IsEmpty",
            "При переполнении — расширять внутренний массив (удвоение)",
            "Pop/Dequeue на пустой коллекции — бросать InvalidOperationException"
        };
        public void Run()
        {
            Console.WriteLine("=== Тестирование MyStack<int> ===");
            MyStack<int> stack = new MyStack<int>(2);
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            Console.WriteLine($"Peek: {stack.Peek()}, Count: {stack.Count}");
            Console.WriteLine($"Pop: {stack.Pop()}, Count: {stack.Count}");
            Console.WriteLine($"Pop: {stack.Pop()}, Count: {stack.Count}");
            Console.WriteLine($"Pop: {stack.Pop()}, Count: {stack.Count}");

            try
            {
                stack.Pop();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ожидаемая ошибка: {ex.Message}");
            }
            Console.WriteLine("\n=== Тестирование MyQueue<string> ===");
            MyQueue<string> queue = new MyQueue<string>(2);
            queue.Enqueue("A");
            queue.Enqueue("B");
            queue.Enqueue("C");
            Console.WriteLine($"Peek: {queue.Peek()}, Count: {queue.Count}");
            Console.WriteLine($"Dequeue: {queue.Dequeue()}, Count: {queue.Count}");
            Console.WriteLine($"Dequeue: {queue.Dequeue()}, Count: {queue.Count}");
            Console.WriteLine($"Dequeue: {queue.Dequeue()}, Count: {queue.Count}");

            try
            {
                queue.Dequeue();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ожидаемая ошибка: {ex.Message}");
            }

            Console.WriteLine("\n=== Динамический ввод ===");
            Console.Write("Введите число для добавления в стек: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int userValue))
            {
                stack.Push(userValue);
                Console.WriteLine($"Добавлено {userValue}. Новый размер стека: {stack.Count}");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }
    }
}
