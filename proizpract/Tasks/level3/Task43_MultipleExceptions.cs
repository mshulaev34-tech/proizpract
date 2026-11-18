using proizpract.Common;
using System;

namespace proizpract.Tasks.Level3
{
    /// <summary>
    /// Custom exception for demonstration purposes.
    /// </summary>
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception inner) : base(message, inner) { }
    }

    [Common.TaskInfoAttribute("Level-3", 43)]
    internal class Task43_MultipleExceptions : ITask
    {
        public string Title => "Множественные типы исключений, порядок catch";

        public string Description => "Демонстрация порядка обработки исключений: от частного к общему.";

        public string[] Requirements => new string[]
        {
            "Метод, который может бросить FormatException, DivideByZeroException или CustomException",
            "Несколько catch-блоков в правильном порядке (от частного к общему)",
            "Продемонстрировать все три сценария",
            "В конце — finally с выводом «завершено»",
            "Объяснить, почему порядок catch важен"
        };

        public void Run()
        {
            Console.WriteLine("=== Сценарий 1: деление на ноль (DivideByZeroException) ===");
            SafeExecute("10", "0");

            Console.WriteLine("\n=== Сценарий 2: некорректный ввод (FormatException) ===");
            SafeExecute("abc", "5");

            Console.WriteLine("\n=== Сценарий 3: отрицательный делитель (CustomException) ===");
            SafeExecute("10", "-5");

            Console.WriteLine("\n=== Сценарий 4: успешное выполнение ===");
            SafeExecute("100", "4");

            Console.Write("\nВведите два числа через пробел (например '100 5'): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(' ');
                if (parts.Length == 2)
                {
                    SafeExecute(parts[0], parts[1]);
                }
                else
                {
                    Console.WriteLine("Нужно ввести ровно два числа.");
                }
            }
        }

        /// <summary>
        /// Safely executes the risky method and catches exceptions in the correct order.
        /// </summary>
        private void SafeExecute(string aStr, string bStr)
        {
            try
            {
                int result = RiskyMethod(aStr, bStr);
                Console.WriteLine($"Результат: {result}");
            }
            // IMPORTANT: catch blocks must be ordered from most specific to most general.
            // If we put 'catch (Exception)' first, it would catch EVERYTHING and the
            // subsequent catch blocks would become unreachable (compiler error).
            // The CLR checks catch blocks top-to-bottom and uses the FIRST matching one.
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"[DivideByZeroException] Нельзя делить на ноль: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"[FormatException] Некорректный формат числа: {ex.Message}");
            }
            catch (CustomException ex)
            {
                Console.WriteLine($"[CustomException] Бизнес-правило нарушено: {ex.Message}");
            }
            catch (Exception ex)
            {
                // The most general catch — acts as a safety net for anything unexpected.
                Console.WriteLine($"[Exception] Неожиданная ошибка: {ex.GetType().Name}: {ex.Message}");
            }
            finally
            {
                // finally ALWAYS executes — whether an exception occurred or not,
                // and even if there was a 'return' inside try/catch.
                // It is used for cleanup: closing files, releasing resources, etc.
                Console.WriteLine("[finally] Блок finally выполнен — операция завершена.");
            }
        }

        /// <summary>
        /// A method that can throw different exceptions depending on the input.
        /// </summary>
        private int RiskyMethod(string aStr, string bStr)
        {
            // int.Parse throws FormatException if the string is not a valid integer.
            int a = int.Parse(aStr);
            int b = int.Parse(bStr);

            // Custom business rule: negative divisor is not allowed.
            if (b < 0)
            {
                throw new CustomException($"Делитель не может быть отрицательным (получено {b}).");
            }

            // This throws DivideByZeroException if b == 0.
            return a / b;
        }
    }
}