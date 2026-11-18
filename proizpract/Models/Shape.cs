using proizpract.Common.proizpract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Models
{
    /// <summary>
    /// Represents a circle.
    /// </summary>
    public class Circle : IShape
    {
        public double Radius { get; }
        public string Description => $"Круг (R={Radius})";

        public Circle(double radius)
        {
            if (radius <= 0) throw new ArgumentException("Радиус должен быть положительным.");
            Radius = radius;
        }

        public double Area() => Math.PI * Radius * Radius;
        public double Perimeter() => 2 * Math.PI * Radius;

        public override string ToString() => $"{Description}: S={Area():F2}, P={Perimeter():F2}";
    }

    /// <summary>
    /// Represents a rectangle.
    /// </summary>
    public class Rectangle : IShape
    {
        public double Width { get; }
        public double Height { get; }
        public string Description => $"Прямоугольник ({Width}×{Height})";

        public Rectangle(double width, double height)
        {
            if (width <= 0 || height <= 0) throw new ArgumentException("Стороны должны быть положительными.");
            Width = width;
            Height = height;
        }

        public double Area() => Width * Height;
        public double Perimeter() => 2 * (Width + Height);

        public override string ToString() => $"{Description}: S={Area():F2}, P={Perimeter():F2}";
    }

    /// <summary>
    /// Represents a triangle defined by three sides.
    /// </summary>
    public class Triangle : IShape
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }
        public string Description => $"Треугольник ({A}, {B}, {C})";

        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0) throw new ArgumentException("Стороны должны быть положительными.");
            if (a + b <= c || a + c <= b || b + c <= a) throw new ArgumentException("Неверные стороны треугольника.");
            A = a; B = b; C = c;
        }

        public double Area()
        {
            double p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        public double Perimeter() => A + B + C;

        public override string ToString() => $"{Description}: S={Area():F2}, P={Perimeter():F2}";
    }
}
