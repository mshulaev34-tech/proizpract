using System;

namespace proizpract.Models
{


    /// <summary>
    /// Strategy that applies no discount.
    /// </summary>
    public class NoDiscount : Common.IDiscountStrategy
    {
        public string Description => "Без скидки (0%)";
        public decimal Apply(decimal price) => price;
    }

    /// <summary>
    /// Strategy that applies a 10% VIP discount.
    /// </summary>
    public class VipDiscount : Common.IDiscountStrategy
    {
        public string Description => "VIP скидка (10%)";
        public decimal Apply(decimal price) => price * 0.90m;
    }

    /// <summary>
    /// Strategy that applies a 20% promotional discount.
    /// </summary>
    public class PromoDiscount : Common.IDiscountStrategy
    {
        public string Description => "Промо скидка (20%)";
        public decimal Apply(decimal price) => price * 0.80m;
    }

    /// <summary>
    /// Calculates the final price using a discount strategy.
    /// The calculator itself does not know how discounts are applied —
    /// it delegates this to the injected strategy.
    /// </summary>
    public class PriceCalculator
    {
        private readonly Common.IDiscountStrategy _strategy;

        public PriceCalculator(Common.IDiscountStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        /// <summary>
        /// Calculates the final price after applying the current strategy.
        /// </summary>
        public decimal Calculate(decimal basePrice)
        {
            if (basePrice < 0) throw new ArgumentException("Цена не может быть отрицательной.");
            return _strategy.Apply(basePrice);
        }

        /// <summary>
        /// Returns a description of the currently used strategy.
        /// </summary>
        public string StrategyDescription => _strategy.Description;
    }
}