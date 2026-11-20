using proizpract.Common;
using proizpract.Models;
using System;

namespace proizpract.Tasks.Level2
{
    [Common.TaskInfoAttribute("Level-2", 26)]
    internal class Task26_DiscountStrategy : ITask
    {
        public string Title => "Паттерн «Стратегия» (скидки)";

        public string Description => "Реализация калькулятора скидок через паттерн «Стратегия».";

        public string[] Requirements => new string[]
        {
            "Интерфейс IDiscountStrategy с методом Apply(decimal price)",
            "Реализации: NoDiscount, VipDiscount (10%), PromoDiscount (20%)",
            "Класс PriceCalculator, принимающий стратегию в конструкторе",
            "Продемонстрировать расчёт цены с каждой стратегией"
        };


        public void Run()
        {
            decimal basePrice = 1000m;
            Console.WriteLine($"Базовая цена: {basePrice:F2} руб.\n");

            IDiscountStrategy[] strategies = new IDiscountStrategy[]
            {
                new NoDiscount(),
                new VipDiscount(),
                new PromoDiscount()
            };

            Console.WriteLine("=== Расчёт цены с разными стратегиями ===");
            foreach (var strategy in strategies)
            {
                var calculator = new PriceCalculator(strategy);
                decimal finalPrice = calculator.Calculate(basePrice);
                Console.WriteLine($"{calculator.StrategyDescription}: {finalPrice:F2} руб.");
            }

            Console.Write("\nВведите свою базовую цену: ");
            string input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal userPrice) && userPrice >= 0)
            {
                var vipCalc = new PriceCalculator(new VipDiscount());
                Console.WriteLine($"VIP-цена для {userPrice:F2}: {vipCalc.Calculate(userPrice):F2} руб.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }
    }
}