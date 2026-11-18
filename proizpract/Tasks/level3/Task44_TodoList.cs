using proizpract.Common;
using proizpract.Models;
using System;
using System.IO;

namespace proizpract.Tasks.Level3
{
    [Common.TaskInfoAttribute("Level-3", 44)]
    internal class Task44_TodoList : ITask
    {
        public string Title => "Мини-проект «Todo List» (классы + JSON)";

        public string Description => "Консольное приложение для списка задач с сохранением в JSON.";

        public string[] Requirements => new string[]
        {
            "Классы: TaskItem, TaskRepository, TaskService",
            "CRUD-операции: добавить, вывести все, отметить выполненной, удалить",
            "Сохранение/загрузка через System.Text.Json (файл tasks.json)",
            "Меню с выбором действий",
            "Обработка ошибок"
        };

        public void Run()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "tasks.json");
            TaskRepository repository = new TaskRepository(filePath);
            TaskService service = new TaskService(repository);

            Console.WriteLine($"Файл хранения: {filePath}\n");

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== Todo List ===");
                Console.WriteLine("1. Показать все задачи");
                Console.WriteLine("2. Добавить задачу");
                Console.WriteLine("3. Отметить задачу как выполненную");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("5. Выйти");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowAll(service);
                            break;
                        case "2":
                            AddTask(service);
                            break;
                        case "3":
                            MarkDone(service);
                            break;
                        case "4":
                            DeleteTask(service);
                            break;
                        case "5":
                            running = false;
                            Console.WriteLine("Выход из приложения.");
                            break;
                        default:
                            Console.WriteLine("Неизвестная команда.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Ошибка] {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Displays all tasks in the list.
        /// </summary>
        private void ShowAll(TaskService service)
        {
            var tasks = service.GetAll();
            if (tasks.Count == 0)
            {
                Console.WriteLine("Список задач пуст.");
                return;
            }
            Console.WriteLine($"\nВсего задач: {tasks.Count}");
            foreach (var task in tasks)
            {
                Console.WriteLine($"  {task}");
            }
        }

        /// <summary>
        /// Prompts the user for a title and adds a new task.
        /// </summary>
        private void AddTask(TaskService service)
        {
            Console.Write("Введите название задачи: ");
            string title = Console.ReadLine();
            var task = service.Add(title);
            Console.WriteLine($"Добавлена задача: {task}");
        }

        /// <summary>
        /// Prompts the user for an ID and marks the task as done.
        /// </summary>
        private void MarkDone(TaskService service)
        {
            Console.Write("Введите ID задачи: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (service.MarkAsDone(id))
                {
                    Console.WriteLine($"Задача #{id} отмечена как выполненная.");
                }
                else
                {
                    Console.WriteLine($"Задача с ID {id} не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ID.");
            }
        }

        /// <summary>
        /// Prompts the user for an ID and deletes the task.
        /// </summary>
        private void DeleteTask(TaskService service)
        {
            Console.Write("Введите ID задачи: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (service.Delete(id))
                {
                    Console.WriteLine($"Задача #{id} удалена.");
                }
                else
                {
                    Console.WriteLine($"Задача с ID {id} не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ID.");
            }
        }
    }
}