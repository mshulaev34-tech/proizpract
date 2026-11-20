using System;

namespace proizpract.Models
{
    /// <summary>
    /// Represents a temperature value with validation and conversion between Celsius and Fahrenheit.
    /// </summary>
    public class Temperature
    {
        private const double AbsoluteZero = -273.15;

        private double _celsius;

        /// <summary>
        /// Temperature in Celsius. Cannot be lower than absolute zero (-273.15°C).
        /// </summary>
        public double Celsius
        {
            get => _celsius;
            set
            {
                if (value < AbsoluteZero)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        $"Температура не может быть ниже абсолютного нуля ({AbsoluteZero}°C).");
                }
                _celsius = value;
            }
        }

        /// <summary>
        /// Temperature in Fahrenheit, computed from Celsius. Read-only.
        /// </summary>
        public double Fahrenheit => Celsius * 9.0 / 5.0 + 32;

        public Temperature(double celsius)
        {
            Celsius = celsius;
        }

        public override string ToString()
        {
            return $"{Celsius:F2}°C / {Fahrenheit:F2}°F";
        }
    }
}