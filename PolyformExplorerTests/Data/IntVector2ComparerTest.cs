using NUnit.Framework;

namespace PolyformExplorer.Data.Tests
{
    internal class IntVector2ComparerTest
    {
        private readonly IntVector2Comparer comparer = new();

        [TestCase(1, 1, 1, 1, ExpectedResult = 0)]
        [TestCase(1, 1, 2, 0, ExpectedResult = -1)]
        [TestCase(1, 1, 0, 2, ExpectedResult = 1)]
        [TestCase(1, 1, 1, 2, ExpectedResult = -1)]
        [TestCase(1, 1, 1, 0, ExpectedResult = 1)]
        public int Comparer_sorts_bottom_to_top_then_left_to_right(int x1, int y1, int x2, int y2)
        {
            IntVector2 vec1 = new(x1, y1);
            IntVector2 vec2 = new(x2, y2);

            return comparer.Compare(vec1, vec2);
        }
    }
}