using proizpract.Common;
using proizpract.Models;
using proizpract.Tests;
using System;

namespace proizpract.Tasks.Level3
{
    [Common.TaskInfoAttribute("Level-3", 45)]
    internal class Task45_Library : ITask
    {
        public string Title => "Мини-проект «Библиотека» + юнит-тесты";

        public string Description => "Классы библиотеки с выдачей/возвратом книг и 7 юнит-тестов.";

        public string[] Requirements => new string[]
        {
            "Классы: Book, Reader, Library",
            "Логика: выдача книги читателю, возврат, лимит (не больше 3 книг на читателя)",
            "Обработка: книгу уже выдали, лимит превышен — бросать исключения",
            "Написать 5+ юнит-тестов на ключевые сценарии",
            "Все тесты должны проходить"
        };

        public void Run()
        {
            Console.WriteLine("=== Запуск юнит-тестов ===\n");
            LibraryTests.RunAll();

            Console.WriteLine("\n=== Демонстрация работы библиотеки ===\n");
            Library library = new Library();

            library.AddBook(new Book("Война и мир", "Л. Толстой"));
            library.AddBook(new Book("Анна Каренина", "Л. Толстой"));
            library.AddBook(new Book("Преступление и наказание", "Ф. Достоевский"));
            library.AddBook(new Book("Идиот", "Ф. Достоевский"));
            library.AddBook(new Book("Мастер и Маргарита", "М. Булгаков"));

            library.AddReader(new Reader("Иван"));
            library.AddReader(new Reader("Мария"));

            Console.WriteLine("Книги в библиотеке:");
            foreach (var book in library.GetAllBooks())
            {
                Console.WriteLine($"  {book}");
            }

            Console.WriteLine("\nИван берёт 3 книги:");
            library.IssueBook("Война и мир", "Иван");
            library.IssueBook("Анна Каренина", "Иван");
            library.IssueBook("Преступление и наказание", "Иван");

            Console.WriteLine("\nПопытка Ивана взять 4-ю книгу (должна быть ошибка):");
            TryAction(() => library.IssueBook("Идиот", "Иван"));

            Console.WriteLine("\nМария пытается взять уже выданную книгу (должна быть ошибка):");
            TryAction(() => library.IssueBook("Война и мир", "Мария"));

            Console.WriteLine("\nИван возвращает одну книгу:");
            library.ReturnBook("Война и мир", "Иван");
            Console.WriteLine("  Успешно.");

            Console.WriteLine("\nМария берёт освобождённую книгу:");
            library.IssueBook("Война и мир", "Мария");
            Console.WriteLine("  Успешно.");

            Console.WriteLine("\nИтоговое состояние книг:");
            foreach (var book in library.GetAllBooks())
            {
                Console.WriteLine($"  {book}");
            }
        }

        /// <summary>
        /// Executes an action and prints any exception that occurs.
        /// </summary>
        private void TryAction(Action action)
        {
            try
            {
                action();
                Console.WriteLine("  [Успех]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [Ошибка] {ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}