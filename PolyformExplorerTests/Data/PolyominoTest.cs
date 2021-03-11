using NUnit.Framework;
using System;
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

        [Test]
        public void Test_string_based_constructor()
        {
            string polyominoString = @"
                ##
                 ## #
                  ###
            ";

            Polyomino expectedPolyomino = new Polyomino(new List<IntVector2>()
            {
                new(0, 2),
                new(1, 2),
                new(1, 1),
                new(2, 1),
                new(2, 0),
                new(3, 0),
                new(4, 0),
                new(4, 1),
            });

            Assert.That(new Polyomino(polyominoString), Is.EqualTo(expectedPolyomino));
        }

        [Test]
        public void Polyomino_cells_must_be_orthogonally_contiguous()
        {
            List<IntVector2> discontiguousCells = new()
            {
                new(1, 0),
                new(0, 1),
            };

            Assert.That(
                () => new Polyomino(discontiguousCells),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void String_based_constructor_enforces_contiguous_cells()
        {
            string discontiguousString = @"
                ##
                  #
            ";

            Assert.That(
                () => new Polyomino(discontiguousString),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Polyominoes_implement_deep_value_equality()
        {
            Polyomino jTetromino1 = new(@"
                 #
                 #
                ##
            ");

            Polyomino jTetromino2 = new(@"

                     #
                     #
                    ##
            ");

            Polyomino lTetromino = new(@"
                 #
                 #
                 ##
            ");

            Assert.That(jTetromino1, Is.EqualTo(jTetromino2));
            Assert.That(jTetromino1, Is.Not.EqualTo(lTetromino));
            Assert.That(jTetromino2, Is.Not.EqualTo(lTetromino));
        }

        [Test]
        public void Hash_code_corresponds_to_deep_value()
        {
            Polyomino jTetromino1 = new(@"
                 #
                 #
                ##
            ");

            Polyomino jTetromino2 = new(@"

                     #
                     #
                    ##
            ");

            Polyomino lTetromino = new(@"
                 #
                 #
                 ##
            ");

            Assert.That(jTetromino1.GetHashCode(), Is.EqualTo(jTetromino2.GetHashCode()));
            Assert.That(jTetromino1.GetHashCode(), Is.Not.EqualTo(lTetromino.GetHashCode()));
            Assert.That(jTetromino2.GetHashCode(), Is.Not.EqualTo(lTetromino.GetHashCode()));
        }

        [Test]
        public void Test_clockwise_rotation()
        {
            Polyomino jTetromino = new(@"
                 #
                 #
                ##
            ");

            Polyomino expectedTetromino = new(@"
                #
                ###
            ");

            Assert.That(jTetromino.RotateCW(), Is.EqualTo(expectedTetromino));
        }

        [Test]
        public void Test_counterclockwise_rotation()
        {
            Polyomino jTetromino = new(@"
                 #
                 #
                ##
            ");

            Polyomino expectedTetromino = new(@"
                ###
                  #
            ");

            Assert.That(jTetromino.RotateCCW(), Is.EqualTo(expectedTetromino));
        }

        [Test]
        public void Test_180_degree_rotation()
        {
            Polyomino jTetromino = new(@"
                 #
                 #
                ##
            ");

            Polyomino expectedTetromino = new(@"
                ##
                #
                #
            ");

            Assert.That(jTetromino.Rotate180(), Is.EqualTo(expectedTetromino));
        }

        [Test]
        public void Test_horizontal_reflection()
        {
            Polyomino jTetromino = new(@"
                 #
                 #
                ##
            ");

            Polyomino expectedTetromino = new(@"
                #
                #
                ##
            ");

            Assert.That(jTetromino.ReflectHorizontally(), Is.EqualTo(expectedTetromino));
        }

        [Test]
        public void Test_vertical_reflection()
        {
            Polyomino jTetromino = new(@"
                 #
                 #
                ##
            ");

            Polyomino expectedTetromino = new(@"
                ##
                 #
                 #
            ");

            Assert.That(jTetromino.ReflectVertically(), Is.EqualTo(expectedTetromino));
        }

        [Test]
        public void Test_main_diagonal_reflection()
        {
            Polyomino jTetromino = new(@"
                 #
                 #
                ##
            ");

            Polyomino expectedTetromino = new(@"
                ###
                #
            ");

            Assert.That(jTetromino.ReflectAlongMainDiagonal(), Is.EqualTo(expectedTetromino));
        }

        [Test]
        public void Test_anti_diagonal_reflection()
        {
            Polyomino jTetromino = new(@"
                 #
                 #
                ##
            ");

            Polyomino expectedTetromino = new(@"
                  #
                ###
            ");

            Assert.That(jTetromino.ReflectAlongAntiDiagonal(), Is.EqualTo(expectedTetromino));
        }
    }
}
