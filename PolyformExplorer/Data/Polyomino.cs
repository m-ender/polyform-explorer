using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace PolyformExplorer.Data
{
    internal sealed record Polyomino : IEquatable<Polyomino>
    {
        public enum Symmetry
        {
            None,
            MirrorHorizontal,
            MirrorVertical,
            MirrorMainDiagonal,
            MirrorAntiDiagonal,
            C2,
            D2Orthogonal,
            D2Diagonal,
            C4,
            D4,
        };

        private readonly ImmutableSortedSet<IntVector2> cells;

        public int Order => cells.Count;

        public Polyomino()
        {
            IntVector2 origin = new(0, 0);
            cells = ImmutableSortedSet.Create(new IntVector2Comparer(), origin);
        }

        public Polyomino(IEnumerable<IntVector2> cells)
        {
            if (!AreCellsContiguous(cells))
                throw new ArgumentException("Cells must be orthogonally contiguous", nameof(cells));

            IEnumerable<IntVector2> normalizedCells = Normalize(cells);
            this.cells = ImmutableSortedSet.CreateRange(new IntVector2Comparer(), normalizedCells);
        }

        // Convenience constructor, e.g. for testing. Takes a multiline representation
        // of the polyomino, using '#' to indicate placement of cells. Example for
        // T tetromino:
        //
        // ###
        //  #
        public Polyomino(string stringRepresentation)
            : this (StringToCells(stringRepresentation))
        { }

        private static IEnumerable<IntVector2> StringToCells(string stringRepresentation)
        {
            string[] lines = stringRepresentation.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            List<IntVector2> cells = new();

            for (int y = 0; y < lines.Length; ++y)
            {
                string line = lines[y];
                for (int x = 0; x < line.Length; ++x)
                    if (line[x] == '#')
                        cells.Add(new IntVector2(x, -y));
            }

            return cells;
        }

        // Implemented via BFT
        private static bool AreCellsContiguous(IEnumerable<IntVector2> cells)
        {
            HashSet<IntVector2> remainingCells = new(cells);
            Queue<IntVector2> cellQueue = new();

            IntVector2 firstCell = remainingCells.First();
            remainingCells.Remove(firstCell);
            cellQueue.Enqueue(firstCell);

            while (cellQueue.Count > 0)
            {
                IntVector2 cell = cellQueue.Dequeue();
                foreach (IntVector2 neighbor in cell.GetNeighbors())
                    if (remainingCells.Remove(neighbor))
                        cellQueue.Enqueue(neighbor);
            }

            return remainingCells.Count == 0;
        }

        private static IEnumerable<IntVector2> Normalize(IEnumerable<IntVector2> cells)
        {
            int minX = cells.Min(cell => cell.X);
            int minY = cells.Min(cell => cell.Y);

            IntVector2 origin = new(minX, minY);

            return cells.Select(cell => cell - origin);
        }

        public bool Contains(IntVector2 pos)
            => cells.Contains(pos);

        public bool Equals(Polyomino? other)
            => other is not null && cells.SetEquals(other.cells);

        public override int GetHashCode()
        {
            int hash = 13;
            hash = hash * 7 + cells.Count.GetHashCode();

            foreach (IntVector2 cell in cells)
                hash = hash * 7 + cell.GetHashCode();

            return hash;
        }

        public Polyomino RotateCW() 
            => new Polyomino(cells.Select(c => new IntVector2(c.Y, -c.X)));

        public Polyomino RotateCCW()
            => new Polyomino(cells.Select(c => new IntVector2(-c.Y, c.X)));

        public Polyomino Rotate180()
            => new Polyomino(cells.Select(c => -c));

        public Polyomino ReflectHorizontally()
            => new Polyomino(cells.Select(c => c with { X = -c.X }));

        public Polyomino ReflectVertically()
            => new Polyomino(cells.Select(c => c with { Y = -c.Y }));

        public Polyomino ReflectAlongMainDiagonal()
            => new Polyomino(cells.Select(c => new IntVector2(c.Y, c.X)));

        public Polyomino ReflectAlongAntiDiagonal()
            => new Polyomino(cells.Select(c => new IntVector2(-c.Y, -c.X)));
    }
}