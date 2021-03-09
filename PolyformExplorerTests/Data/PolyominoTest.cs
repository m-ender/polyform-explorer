using NUnit.Framework;
using System.Collections.Generic;

namespace PolyformExplorer.Data.Tests
{
    internal class PolyominoTest
    {   
        [Test]
        public void Default_constructor_returns_monomino()
        {
            Polyomino polyomino = new();
            IntVector2 origin = new(0, 0);

            Assert.That(polyomino.Order, Is.EqualTo(1));
            Assert.That(polyomino.Contains(origin));
        }

        [Test]
        public void Constructor_uses_custom_coordinates()
        {
            List<IntVector2> cells = new()
            {
                new(1, 0),
                new(0, 1),
                new(1, 1),
            };

            Polyomino polyomino = new(cells);

            Assert.That(polyomino.Order, Is.EqualTo(3));
            foreach (IntVector2 cell in cells)
                Assert.That(polyomino.Contains(cell));
        }

        [Test]
        public void Constructor_normalizes_position_of_cells()
        {
            List<IntVector2> cells = new()
            {
                new(3, -5),
                new(2, -4),
                new(3, -4),
            };

            List<IntVector2> expectedCells = new()
            {
                new(1, 0),
                new(0, 1),
                new(1, 1),
            };

            Polyomino polyomino = new(cells);

            Assert.That(polyomino.Order, Is.EqualTo(3));
            foreach (IntVector2 cell in expectedCells)
                Assert.That(polyomino.Contains(cell));
        }
    }
}
