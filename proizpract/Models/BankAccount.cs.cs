using System;

namespace proizpract.Models
{
    /// <summary>
    /// Exception thrown when a withdrawal amount exceeds the available balance.
    /// </summary>
    public class InsufficientFundsException : Exception
    {
        public decimal Balance { get; }
        public decimal WithdrawAmount { get; }

        public InsufficientFundsException(decimal balance, decimal withdrawAmount)
            : base($"Недостаточно средств. Баланс: {balance:F2}, запрошено: {withdrawAmount:F2}")
        {
            Balance = balance;
            WithdrawAmount = withdrawAmount;
        }

        public InsufficientFundsException(string message) : base(message) { }

        public InsufficientFundsException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Represents a bank account with deposit and withdrawal operations.
    /// </summary>
    public class BankAccount
    {
        private decimal _balance;

        public string Owner { get; }
        public string AccountNumber { get; }

        public decimal Balance => _balance;

        public BankAccount(string owner, string accountNumber, decimal initialBalance = 0)
        {
            if (string.IsNullOrWhiteSpace(owner)) throw new ArgumentException("Владелец не может быть пустым.");
            if (string.IsNullOrWhiteSpace(accountNumber)) throw new ArgumentException("Номер счёта не может быть пустым.");
            if (initialBalance < 0) throw new ArgumentOutOfRangeException(nameof(initialBalance), "Начальный баланс не может быть отрицательным.");

            Owner = owner;
            AccountNumber = accountNumber;
            _balance = initialBalance;
        }

        /// <summary>
        /// Deposits the specified amount into the account.
        /// </summary>
        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Сумма пополнения должна быть положительной.");
            _balance += amount;
        }

        /// <summary>
        /// Withdraws the specified amount from the account.
        /// Throws InsufficientFundsException if the amount exceeds the balance.
        /// </summary>
        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Сумма снятия должна быть положительной.");
            if (amount > _balance) throw new InsufficientFundsException(_balance, amount);
            _balance -= amount;
        }

        public override string ToString()
        {
            return $"Счёт {AccountNumber} ({Owner}): {_balance:F2} руб.";
        }
    }
}