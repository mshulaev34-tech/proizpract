using proizpract.Common;
using System;
using System.IO;

namespace proizpract.Tasks.Level2
{
    /// <summary>
    /// A simple file writer that implements IDisposable to properly release the underlying stream.
    /// </summary>
    public class FileWriter : IDisposable
    {
        private StreamWriter _writer;
        private bool _disposed;

        public string FilePath { get; }

        public FileWriter(string filePath)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _writer = new StreamWriter(filePath, append: false);
        }

        /// <summary>
        /// Writes a line of text to the file.
        /// </summary>
        public void WriteLine(string text)
        {
            ThrowIfDisposed();
            _writer.WriteLine(text);
        }

        /// <summary>
        /// Flushes the underlying buffer to the file.
        /// </summary>
        public void Flush()
        {
            ThrowIfDisposed();
            _writer.Flush();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(FileWriter));
        }

        /// <summary>
        /// Releases all resources used by the FileWriter.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Core disposal logic. Closes the underlying StreamWriter.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _writer?.Dispose();
                _writer = null;
            }
            _disposed = true;
        }

        ~FileWriter()
        {
            Dispose(false);
        }
    }

    [Common.TaskInfoAttribute("Level-2", 31)]
    internal class Task31_DisposableFileWriter : ITask
    {
        public string Title => "IDisposable + using";

        public string Description => "Реализация IDisposable для класса FileWriter и демонстрация using.";

        public string[] Requirements => new string[]
        {
            "Класс FileWriter с полем StreamWriter",
            "Реализовать Dispose() (закрыть StreamWriter)",
            "Продемонстрировать использование через using (блок)",
            "Дополнительно: продемонстрировать using-декларацию (C# 8+, using var)"
        };

        public void Run()
        {
            string tempDir = Path.GetTempPath();
            string file1 = Path.Combine(tempDir, "task31_block1.txt");
            string file2 = Path.Combine(tempDir, "task31_block2.txt");

            Console.WriteLine("=== Вариант 1: Классический using-блок ===");
            using (FileWriter writer1 = new FileWriter(file1))
            {
                writer1.WriteLine("Строка 1 — записана через using-блок");
                writer1.WriteLine("Строка 2 — записана через using-Block");
                writer1.Flush();
                Console.WriteLine($"Записано в: {file1}");
            } 
            Console.WriteLine("Блок using завершён — ресурсы освобождены.\n");

            Console.WriteLine("=== Вариант 2: Ещё один классический using-блок ===");
            using (FileWriter writer2 = new FileWriter(file2))
            {
                writer2.WriteLine("Строка A — записана через классический using");
                writer2.WriteLine("Строка B — записана через классический using");
                writer2.Flush();
                Console.WriteLine($"Записано в: {file2}");
            } 
            Console.WriteLine("Блок using завершён — ресурсы освобождены.\n");

            Console.WriteLine("=== Проверка ObjectDisposedException ===");
            FileWriter writer3 = new FileWriter(Path.Combine(tempDir, "task31_disposed.txt"));
            writer3.Dispose(); 
            try
            {
                writer3.WriteLine("Это не должно сработать");
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"[Поймано] {ex.GetType().Name}: {ex.Message}");
            }

            Console.WriteLine("\n=== Содержимое файлов ===");
            Console.WriteLine($"--- {Path.GetFileName(file1)} ---");

            Console.WriteLine(File.ReadAllText(file1));

            Console.WriteLine($"--- {Path.GetFileName(file2)} ---");

            Console.WriteLine(File.ReadAllText(file2));


            File.Delete(file1);
            File.Delete(file2);
            File.Delete(Path.Combine(tempDir, "task31_disposed.txt"));
            Console.WriteLine("\nВременные файлы удалены.");
        }
    }
}