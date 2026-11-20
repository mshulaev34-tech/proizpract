using System;

namespace proizpract.Models
{
    /// <summary>
    /// Abstract base class for all animals.
    /// </summary>
    public abstract class Animal
    {
        public string Name { get; protected set; }

        protected Animal(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Produces the characteristic sound of the animal.
        /// </summary>
        public abstract string MakeSound();

        public override string ToString()
        {
            return $"{GetType().Name} по имени \"{Name}\" говорит: {MakeSound()}";
        }
    }

    /// <summary>
    /// Represents a dog.
    /// </summary>
    public class Dog : Animal
    {
        public Dog(string name) : base(name) { }

        public override string MakeSound() => "Гав!";
    }

    /// <summary>
    /// Represents a cat.
    /// </summary>
    public class Cat : Animal
    {
        public Cat(string name) : base(name) { }

        public override string MakeSound() => "Мяу!";
    }

    /// <summary>
    /// Represents a bird with the ability to fly.
    /// </summary>
    public class Bird : Animal
    {
        public Bird(string name) : base(name) { }

        public override string MakeSound() => "Чик-чирик!";

        /// <summary>
        /// Makes the bird fly.
        /// </summary>
        public string Fly() => $"{Name} взлетает в небо!";
    }
}