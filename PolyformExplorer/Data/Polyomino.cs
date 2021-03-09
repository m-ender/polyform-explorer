using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace PolyformExplorer.Data
{
    internal record Polyomino
    {
        public enum Symmetry
        {
            None,
            MirrorOrthogonal,
            MirrorDiagonal,
            C2,
            D2Orthogonal,
            D2Diagonal,
            C4,
            D4,
        };

        private readonly ImmutableHashSet<IntVector2> cells;

        public int Order => cells.Count;

        public Polyomino()
        {
            IntVector2 origin = new IntVector2(0, 0);
            cells = ImmutableHashSet.Create(origin);
        }

        public Polyomino(IEnumerable<IntVector2> cells)
        {
            IEnumerable<IntVector2> normalisedCells = Normalize(cells);
            this.cells = ImmutableHashSet.CreateRange(normalisedCells);
        }

        private IEnumerable<IntVector2> Normalize(IEnumerable<IntVector2> cells)
        {
            int minX = cells.Min(cell => cell.X);
            int minY = cells.Min(cell => cell.Y);

            IntVector2 origin = new(minX, minY);

            return cells.Select(cell => cell - origin);
        }

        public bool Contains(IntVector2 pos)
            => cells.Contains(pos);
    }
}