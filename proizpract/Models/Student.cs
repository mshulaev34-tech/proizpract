using System;
using System.Collections.Generic;

namespace proizpract.Models
{
    /// <summary>
    /// Represents a student with a name and average grade.
    /// Implements IComparable to allow default sorting by grade (descending).
    /// </summary>
    public class Student : IComparable<Student>
    {
        public string Name { get; }
        public double AverageGrade { get; }

        public Student(string name, double averageGrade)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя не может быть пустым.");
            if (averageGrade < 0 || averageGrade > 5) throw new ArgumentException("Балл должен быть в диапазоне 0-5.");
            Name = name;
            AverageGrade = averageGrade;
        }

        /// <summary>
        /// Default comparison: sorts students by average grade in descending order.
        /// </summary>
        public int CompareTo(Student other)
        {
            if (other == null) return 1;
            // Descending order: higher grade comes first
            return other.AverageGrade.CompareTo(AverageGrade);
        }

        public override string ToString()
        {
            return $"{Name} — средний балл: {AverageGrade:F2}";
        }
    }

    /// <summary>
    /// Comparer that sorts students alphabetically by name.
    /// </summary>
    public class ByNameComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}