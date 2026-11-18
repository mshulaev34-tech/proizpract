using proizpract.Common;
using proizpract.Models;
using System;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 30)]
    internal class Task30_BankAccount : ITask
    {
        public string Title => "BankAccount + свой Exception";

        public string Description => "Реализация банковского счёта с пользовательским исключением.";

        public string[] Requirements => new string[]
        {
            "Класс BankAccount с приватным балансом",
            "Методы Deposit(decimal), Withdraw(decimal)",
            "Свой класс InsufficientFundsException : Exception",
            "Withdraw бросает исключение, если сумма больше баланса",
            "Продемонстрировать успешное снятие и попытку снять больше баланса"
        };

        public void Run()
        {
            BankAccount account = new BankAccount("Иван Петров", "ACC-001", 1000m);
            Console.WriteLine($"Создан: {account}\n");

            Console.WriteLine("=== Успешные операции ===");
            account.Deposit(500m);
            Console.WriteLine($"После пополнения на 500: {account}");

            account.Withdraw(200m);
            Console.WriteLine($"После снятия 200: {account}");

            Console.WriteLine("\n=== Попытка снять больше баланса ===");
            try
            {
                decimal bigAmount = account.Balance + 500m;
                Console.WriteLine($"Пытаемся снять {bigAmount:F2} при балансе {account.Balance:F2}...");
                account.Withdraw(bigAmount);
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"[Поймано] {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine($"Баланс после неудачной операции: {account.Balance:F2} (не изменился)");
            }

            Console.WriteLine("\n=== Попытка снять отрицательную сумму ===");
            try
            {
                account.Withdraw(-100m);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"[Поймано] {ex.GetType().Name}: {ex.Message}");
            }

            Console.Write("\nВведите сумму для снятия: ");
            string input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal userAmount))
            {
                try
                {
                    account.Withdraw(userAmount);
                    Console.WriteLine($"Успешно снято. {account}");
                }
                catch (InsufficientFundsException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }
    }
}