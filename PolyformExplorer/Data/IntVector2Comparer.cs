using System.Collections.Generic;

namespace PolyformExplorer.Data
{
    internal class IntVector2Comparer : Comparer<IntVector2>
    {
        public override int Compare(IntVector2? vec1, IntVector2? vec2)
        {
            if (vec1 is null)
                return vec2 is null ? 0 : -1;

            if (vec2 is null)
                return 1;

            if (vec1.X != vec2.X)
                return vec1.X - vec2.X;

            return vec1.Y - vec2.Y;
        }
    }
}