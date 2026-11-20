using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Models
{
    public class Point : IEquatable<Point>
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Determines whether the specified point is equal to the current point.
        /// </summary>
        public bool Equals(Point other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current point.
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        /// <summary>
        /// Returns a hash code based on the point coordinates.
        /// </summary>
        public override int GetHashCode()
        {
            // Classic hash combination — works in all .NET versions
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + X.GetHashCode();
                hash = hash * 31 + Y.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Point left, Point right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
