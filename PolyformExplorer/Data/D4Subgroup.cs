namespace PolyformExplorer.Data
{
    internal enum D4Subgroup
    {
        Identity,
        D1AcrossHorizontal,
        D1AcrossVertical,
        D1AcrossMainDiagonal,
        D1AcrossAntiDiagonal,
        C2,
        D2Orthogonal,
        D2Diagonal,
        C4,
        D4,
    };

    internal static class D4SubgroupExtensions
    {
        public static bool IsSubgroupOf(this D4Subgroup group, D4Subgroup other)
            => group == D4Subgroup.Identity
            || other == D4Subgroup.D4
            || group == other
            || (group == D4Subgroup.D1AcrossHorizontal || group == D4Subgroup.D1AcrossVertical) && other == D4Subgroup.D2Orthogonal
            || (group == D4Subgroup.D1AcrossMainDiagonal || group == D4Subgroup.D1AcrossAntiDiagonal) && other == D4Subgroup.D2Diagonal
            || group == D4Subgroup.C2 && (other == D4Subgroup.D2Orthogonal || other == D4Subgroup.D2Diagonal || other == D4Subgroup.C4);
    }
}