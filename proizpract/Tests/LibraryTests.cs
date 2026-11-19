using proizpract.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace proizpract.Tests
{
    /// <summary>
    /// A simple test harness that mimics a unit-test framework.
    /// In a real project, these would be xUnit/NUnit tests in a separate test project.
    /// </summary>
    public static class LibraryTests
    {
        private static int _passed;
        private static int _failed;
        private static readonly List<string> _failedNames = new List<string>();

        /// <summary>
        /// Runs all tests and prints a summary.
        /// </summary>
        public static void RunAll()
        {
            _passed = 0;
            _failed = 0;
            _failedNames.Clear();

            Run(nameof(Can_Issue_Available_Book), Can_Issue_Available_Book);
            Run(nameof(Cannot_Issue_Already_Borrowed_Book), Cannot_Issue_Already_Borrowed_Book);
            Run(nameof(Cannot_Exceed_Limit_Of_3_Books), Cannot_Exceed_Limit_Of_3_Books);
            Run(nameof(Can_Return_Borrowed_Book), Can_Return_Borrowed_Book);
            Run(nameof(Cannot_Return_NonBorrowed_Book), Cannot_Return_NonBorrowed_Book);
            Run(nameof(Cannot_Issue_Nonexistent_Book), Cannot_Issue_Nonexistent_Book);
            Run(nameof(Cannot_Issue_To_Nonexistent_Reader), Cannot_Issue_To_Nonexistent_Reader);

            Console.WriteLine($"\n=== Итоги: {_passed} пройдено, {_failed} провалено ===");
            if (_failed > 0)
            {
                Console.WriteLine("Проваленные тесты:");
                foreach (string name in _failedNames)
                {
                    Console.WriteLine($"  ✗ {name}");
                }
            }
        }

        private static void Run(string name, Action test)
        {
            try
            {
                test();
                Console.WriteLine($"  ✓ {name}");
                _passed++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ {name}: {ex.Message}");
                _failed++;
                _failedNames.Add(name);
            }
        }

        private static void AssertTrue(bool condition, string message = "Ожидалось true")
        {
            if (!condition) throw new Exception(message);
        }

        private static void AssertFalse(bool condition, string message = "Ожидалось false")
        {
            if (condition) throw new Exception(message);
        }

        private static void AssertThrows<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
                throw new Exception($"Ожидалось исключение {typeof(TException).Name}, но его не было.");
            }
            catch (TException)
            {
                // Expected
            }
        }

        // === Tests ===

        private static void Can_Issue_Available_Book()
        {
            var lib = new Library();
            var book = new Book("Война и мир", "Толстой");
            var reader = new Reader("Иван");
            lib.AddBook(book);
            lib.AddReader(reader);

            lib.IssueBook("Война и мир", "Иван");

            AssertFalse(book.IsAvailable, "Книга должна стать недоступной");
            AssertTrue(reader.BorrowedBooks.Count == 1, "Читатель должен иметь 1 книгу");
        }

        private static void Cannot_Issue_Already_Borrowed_Book()
        {
            var lib = new Library();
            var book = new Book("Война и мир", "Толстой");
            var reader1 = new Reader("Иван");
            var reader2 = new Reader("Пётр");
            lib.AddBook(book);
            lib.AddReader(reader1);
            lib.AddReader(reader2);

            lib.IssueBook("Война и мир", "Иван");
            AssertThrows<InvalidOperationException>(() => lib.IssueBook("Война и мир", "Пётр"));
        }

        private static void Cannot_Exceed_Limit_Of_3_Books()
        {
            var lib = new Library();
            var reader = new Reader("Иван");
            lib.AddReader(reader);
            for (int i = 1; i <= 3; i++)
            {
                var book = new Book($"Книга {i}", "Автор");
                lib.AddBook(book);
                lib.IssueBook($"Книга {i}", "Иван");
            }

            var extraBook = new Book("Книга 4", "Автор");
            lib.AddBook(extraBook);
            AssertThrows<InvalidOperationException>(() => lib.IssueBook("Книга 4", "Иван"));
        }

        private static void Can_Return_Borrowed_Book()
        {
            var lib = new Library();
            var book = new Book("Война и мир", "Толстой");
            var reader = new Reader("Иван");
            lib.AddBook(book);
            lib.AddReader(reader);
            lib.IssueBook("Война и мир", "Иван");
            lib.ReturnBook("Война и мир", "Иван");

            AssertTrue(book.IsAvailable, "Книга должна снова стать доступной");
            AssertTrue(reader.BorrowedBooks.Count == 0, "У читателя не должно остаться книг");
        }

        private static void Cannot_Return_NonBorrowed_Book()
        {
            var lib = new Library();
            var book = new Book("Война и мир", "Толстой");
            var reader = new Reader("Иван");
            lib.AddBook(book);
            lib.AddReader(reader);

            AssertThrows<InvalidOperationException>(() => lib.ReturnBook("Война и мир", "Иван"));
        }

        private static void Cannot_Issue_Nonexistent_Book()
        {
            var lib = new Library();
            var reader = new Reader("Иван");
            lib.AddReader(reader);

            AssertThrows<KeyNotFoundException>(() => lib.IssueBook("Несуществующая", "Иван"));
        }

        private static void Cannot_Issue_To_Nonexistent_Reader()
        {
            var lib = new Library();
            var book = new Book("Война и мир", "Толстой");
            lib.AddBook(book);

            AssertThrows<KeyNotFoundException>(() => lib.IssueBook("Война и мир", "Несуществующий"));
        }
    }
}