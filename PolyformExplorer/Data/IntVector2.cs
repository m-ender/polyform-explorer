using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PolyformExplorer.Data
{
    [DebuggerDisplay("({X}, {Y})")]
    internal record IntVector2
    {
        public int X { get; init; }
        public int Y { get; init; }

        public IntVector2()
            : this(0, 0) { }

        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static IntVector2 Zero => new IntVector2(0, 0);
        public static IntVector2 One => new IntVector2(1, 1);
        public static IntVector2 Right => new IntVector2(1, 0);
        public static IntVector2 Up => new IntVector2(0, 1);
        public static IntVector2 Left => new IntVector2(-1, 0);
        public static IntVector2 Down => new IntVector2(0, -1);

        public void Deconstruct(out int x, out int y) 
            => (x, y) = (X, Y);

        public int SquaredMagnitude => X * X + Y * Y;

        public float Magnitude => MathF.Sqrt(SquaredMagnitude);

        public static IntVector2 operator +(IntVector2 vectorA, IntVector2 vectorB)
            => new IntVector2(vectorA.X + vectorB.X, vectorA.Y + vectorB.Y);

        public static IntVector2 operator -(IntVector2 vectorA, IntVector2 vectorB)
            => new IntVector2(vectorA.X - vectorB.X, vectorA.Y - vectorB.Y);

        public static IntVector2 operator *(IntVector2 vectorA, IntVector2 vectorB)
            => new IntVector2(vectorA.X * vectorB.X, vectorA.Y * vectorB.Y);

        public static IntVector2 operator /(IntVector2 vectorA, IntVector2 vectorB)
            => new IntVector2(vectorA.X / vectorB.X, vectorA.Y / vectorB.Y);

        public static IntVector2 operator *(IntVector2 vector, int scalar)
            => new IntVector2(vector.X * scalar, vector.Y * scalar);

        public static IntVector2 operator *(int scalar, IntVector2 vector)
            => vector * scalar;

        public static IntVector2 operator /(IntVector2 vector, int scalar)
            => new IntVector2(vector.X / scalar, vector.Y / scalar);

        public static IntVector2 operator /(int scalar, IntVector2 vector)
            => new IntVector2(scalar / vector.X, scalar / vector.Y);

        public int Dot(IntVector2 other)
            => X * other.X + Y * other.Y;

        public int ManhattanDistance(IntVector2 other)
            => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

        public IEnumerable<IntVector2> GetNeighbors()
        {
            return new List<IntVector2>()
            {
                this + Right,
                this + Up,
                this + Left,
                this + Down,
            };
        }

        public override string ToString()
            => $"({X}, {Y})";
    }
}