using NUnit.Framework;
using System.Collections.Generic;

namespace PolyformExplorer.Data.Tests
{
    internal class IntVector2Test
    {
        [Test]
        public void Default_constructor_returns_origin()
        {
            IntVector2 vector = new();

            Assert.That(vector.X, Is.Zero);
            Assert.That(vector.Y, Is.Zero);
        }

        [Test]
        public void Constructor_sets_x_and_y_components()
        {
            const int x = -2;
            const int y = 4;

            IntVector2 vector = new(x, y);

            Assert.That(vector.X, Is.EqualTo(x));
            Assert.That(vector.Y, Is.EqualTo(y));
        }

        [Test]
        public void Vectors_implement_value_equality()
        {
            IntVector2 vectorA1 = new(-2, 4);
            IntVector2 vectorA2 = new(-2, 4);
            IntVector2 vectorB = new(-1, 4);
            IntVector2 vectorC = new(-2, 3);

            Assert.That(vectorA1, Is.EqualTo(vectorA2));
            Assert.That(vectorA1, Is.Not.EqualTo(vectorB));
            Assert.That(vectorA1, Is.Not.EqualTo(vectorC));
            Assert.That(vectorB, Is.Not.EqualTo(vectorC));
        }

        [Test]
        public void Test_static_vector_initializers()
        {
            Assert.That(IntVector2.Zero, Is.EqualTo(new IntVector2(0, 0)));
            Assert.That(IntVector2.One, Is.EqualTo(new IntVector2(1, 1)));
            Assert.That(IntVector2.Right, Is.EqualTo(new IntVector2(1, 0)));
            Assert.That(IntVector2.Up, Is.EqualTo(new IntVector2(0, 1)));
            Assert.That(IntVector2.Left, Is.EqualTo(new IntVector2(-1, 0)));
            Assert.That(IntVector2.Down, Is.EqualTo(new IntVector2(0, -1)));
        }

        [Test]
        public void Test_deconstruction()
        {
            IntVector2 vector = new(-2, 4);
            (int x, int y) = vector;

            Assert.That(x, Is.EqualTo(vector.X));
            Assert.That(y, Is.EqualTo(vector.Y));
        }

        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(2, 1, ExpectedResult = 5)]
        [TestCase(-3, 4, ExpectedResult = 25)]
        public int Test_squared_magnitude(int x, int y)
        {
            return new IntVector2(x, y).SquaredMagnitude;
        }

        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(2, 1, ExpectedResult = 2.2360679775f)] // Sqrt(5)
        [TestCase(-3, 4, ExpectedResult = 5)]
        public float Test_magnitude(int x, int y)
        {
            return new IntVector2(x, y).Magnitude;
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4)]
        [TestCase(1, -2, -3, 4)]
        [TestCase(-1, 2, 3, -4)]
        public void Test_addition(int x1, int x2, int y1, int y2)
        {
            IntVector2 vectorA = new(x1, y1);
            IntVector2 vectorB = new(x2, y2);

            IntVector2 expected = new(x1 + x2, y1 + y2);

            Assert.That(vectorA + vectorB, Is.EqualTo(expected));
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4)]
        [TestCase(1, -2, -3, 4)]
        [TestCase(-1, 2, 3, -4)]
        public void Test_subtraction(int x1, int x2, int y1, int y2)
        {
            IntVector2 vectorA = new(x1, y1);
            IntVector2 vectorB = new(x2, y2);

            IntVector2 expected = new(x1 - x2, y1 - y2);

            Assert.That(vectorA - vectorB, Is.EqualTo(expected));
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4)]
        [TestCase(1, -2, -3, 4)]
        [TestCase(-1, 2, 3, -4)]
        public void Test_multiplication(int x1, int x2, int y1, int y2)
        {
            IntVector2 vectorA = new(x1, y1);
            IntVector2 vectorB = new(x2, y2);

            IntVector2 expected = new(x1 * x2, y1 * y2);

            Assert.That(vectorA * vectorB, Is.EqualTo(expected));
        }

        [TestCase(0, 1, 0, -2)]
        [TestCase(1, 2, 3, 4)]
        [TestCase(1, -2, -3, 4)]
        [TestCase(-1, 2, 3, -4)]
        [TestCase(4, 3, 2, 1)]
        [TestCase(4, -3, -2, 1)]
        [TestCase(-4, 3, 2, -1)]
        public void Test_division(int x1, int x2, int y1, int y2)
        {
            IntVector2 vectorA = new(x1, y1);
            IntVector2 vectorB = new(x2, y2);

            IntVector2 expected = new(x1 / x2, y1 / y2);

            Assert.That(vectorA / vectorB, Is.EqualTo(expected));
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 2, 3)]
        [TestCase(1, -2, -3)]
        [TestCase(-1, 2, 3)]
        public void Test_scalar_multiplication(int x, int y, int s)
        {
            IntVector2 vectorA = new(x, y);

            IntVector2 expected = new(x * s, y * s);

            Assert.That(vectorA * s, Is.EqualTo(expected));
            Assert.That(s * vectorA, Is.EqualTo(expected));
        }

        [TestCase(1, 2, 3)]
        [TestCase(1, -2, -3)]
        [TestCase(-1, 2, 3)]
        [TestCase(3, 2, 1)]
        [TestCase(3, -2, -1)]
        [TestCase(-3, 2, 1)]
        public void Test_scalar_division(int x, int y, int s)
        {
            IntVector2 vectorA = new(x, y);

            IntVector2 expectedA = new(x / s, y / s);
            IntVector2 expectedB = new(s / x, s / y);

            Assert.That(vectorA / s, Is.EqualTo(expectedA));
            Assert.That(s / vectorA, Is.EqualTo(expectedB));
        }

        [TestCase(0, 0, 0, 0, ExpectedResult = 0)]
        [TestCase(0, 1, 2, 0, ExpectedResult = 0)]
        [TestCase(1, 2, 3, 4, ExpectedResult = 11)]
        [TestCase(1, -2, -3, 4, ExpectedResult = -11)]
        [TestCase(-1, -2, -3, 4, ExpectedResult = -5)]
        public int Test_dot_product(int x1, int y1, int x2, int y2)
        {
            IntVector2 vectorA = new(x1, y1);
            IntVector2 vectorB = new(x2, y2);

            return vectorA.Dot(vectorB);
        }

        [TestCase(0, 0, 0, 0, ExpectedResult = 0)]
        [TestCase(0, 1, 2, 0, ExpectedResult = 3)]
        [TestCase(1, 2, 3, 4, ExpectedResult = 4)]
        [TestCase(1, -2, -3, 4, ExpectedResult = 10)]
        [TestCase(-1, -2, -3, 4, ExpectedResult = 8)]
        public int Test_Manhattan_distance(int x1, int y1, int x2, int y2)
        {
            IntVector2 vectorA = new(x1, y1);
            IntVector2 vectorB = new(x2, y2);

            return vectorA.ManhattanDistance(vectorB);
        }

        [Test]
        public void GetNeighbors_returns_orthogonally_adjacent_vectors()
        {
            IntVector2 vector = new(3, 4);

            List<IntVector2> expectedNeighbors = new()
            {
                new(4, 4),
                new(3, 5),
                new(2, 4),
                new(3, 3),
            };

            Assert.That(vector.GetNeighbors(), Is.EquivalentTo(expectedNeighbors));
        }
    }
}