using NUnit.Framework;
using PolyformExplorer.Data;
using System.Collections.Generic;
using System.Linq;

namespace PolyformExplorer.Tests
{
    internal class PolyominoGeneratorTest
    {
        [Test]
        public void Returns_a_single_monomino()
        {
            PolyominoGenerator generator = new();

            IEnumerable<Polyomino> monominoes = generator.GetPolyominoesOfOrder(1);

            Assert.That(monominoes.Count(), Is.EqualTo(1));
            Assert.That(monominoes.First(), Is.EqualTo(new Polyomino()));
        }

        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 5)]
        [TestCase(5, 12)]
        public void Returns_correct_number_of_free_polyominoes(int order, int expectedCount)
        {
            PolyominoGenerator generator = new();

            IEnumerable<Polyomino> polyominoes = generator.GetPolyominoesOfOrder(order);

            Assert.That(polyominoes.Count(), Is.EqualTo(expectedCount));
            Assert.That(polyominoes.All(p => p.Order == order), Is.True);
        }
    }
}